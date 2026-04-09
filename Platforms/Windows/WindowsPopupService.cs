using MauiPopup.Services;
using MauiPopup.Views;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using WinPopup = Microsoft.UI.Xaml.Controls.Primitives.Popup;

namespace MauiPopup.Platforms.Windows
{
    internal class WindowsPopupService : IPopupPlatformService
    {
        private readonly Dictionary<BasePopupPage, PopupEntry> _popups = new();

        public bool IsSupported => true;

        public async Task ShowPopupAsync(BasePopupPage page)
        {
            var mauiContext = PageResolver.GetMauiContext();
            if (mauiContext == null)
                throw new InvalidOperationException("MauiContext is not available.");

            var platformView = page.ToPlatform(mauiContext);

            var xamlRoot = GetXamlRoot();
            if (xamlRoot == null)
                throw new InvalidOperationException("XamlRoot is not available.");

            var popup = new WinPopup
            {
                XamlRoot = xamlRoot,
                IsLightDismissEnabled = false,
                Child = platformView
            };

            var windowSize = xamlRoot.Size;
            platformView.Width = windowSize.Width;
            platformView.Height = windowSize.Height;

            var window = GetCurrentWindow();
            if (window != null)
                window.SizeChanged += OnWindowSizeChanged;

            var entry = new PopupEntry
            {
                Popup = popup,
                PlatformView = platformView,
                Page = page,
                Window = window
            };
            _popups[page] = entry;

            var duration = (int)page.AnimationDuration;

            // Set initial state and open
            switch (page.Animation)
            {
                case PopupAnimation.Fade:
                    platformView.Opacity = 0;
                    popup.IsOpen = true;
                    CreateFadeAnimation(platformView, 0, 1, duration).Begin();
                    break;

                case PopupAnimation.Scale:
                    platformView.Opacity = 0;
                    platformView.RenderTransform = new ScaleTransform { ScaleX = 0.8, ScaleY = 0.8 };
                    platformView.RenderTransformOrigin = new global::Windows.Foundation.Point(0.5, 0.5);
                    popup.IsOpen = true;
                    var scaleShow = new Storyboard();
                    scaleShow.Children.Add(CreateDoubleAnim(platformView, "Opacity", 0, 1, duration));
                    scaleShow.Children.Add(CreateDoubleAnim(platformView, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)", 0.8, 1, duration));
                    scaleShow.Children.Add(CreateDoubleAnim(platformView, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)", 0.8, 1, duration));
                    scaleShow.Begin();
                    break;

                case PopupAnimation.SlideUp:
                    platformView.Opacity = 0;
                    platformView.RenderTransform = new TranslateTransform { Y = 300 };
                    popup.IsOpen = true;
                    var slideUpShow = new Storyboard();
                    slideUpShow.Children.Add(CreateDoubleAnim(platformView, "Opacity", 0, 1, duration));
                    slideUpShow.Children.Add(CreateDoubleAnim(platformView, "(UIElement.RenderTransform).(TranslateTransform.Y)", 300, 0, duration));
                    slideUpShow.Begin();
                    break;

                case PopupAnimation.SlideDown:
                    platformView.Opacity = 0;
                    platformView.RenderTransform = new TranslateTransform { Y = -300 };
                    popup.IsOpen = true;
                    var slideDownShow = new Storyboard();
                    slideDownShow.Children.Add(CreateDoubleAnim(platformView, "Opacity", 0, 1, duration));
                    slideDownShow.Children.Add(CreateDoubleAnim(platformView, "(UIElement.RenderTransform).(TranslateTransform.Y)", -300, 0, duration));
                    slideDownShow.Begin();
                    break;

                default: // None
                    popup.IsOpen = true;
                    break;
            }

            await Task.CompletedTask;
        }

        public async Task DismissPopupAsync(BasePopupPage page)
        {
            if (!_popups.TryGetValue(page, out var entry))
                return;

            _popups.Remove(page);

            if (entry.Window != null)
                entry.Window.SizeChanged -= OnWindowSizeChanged;

            if (page.Animation == PopupAnimation.None)
            {
                CloseAndCleanup(entry, page);
                return;
            }

            var tcs = new TaskCompletionSource<bool>();
            var duration = (int)(page.AnimationDuration * 0.75);

            Storyboard storyboard;

            switch (page.Animation)
            {
                case PopupAnimation.Scale:
                    entry.PlatformView.RenderTransform = new ScaleTransform { ScaleX = 1, ScaleY = 1 };
                    entry.PlatformView.RenderTransformOrigin = new global::Windows.Foundation.Point(0.5, 0.5);
                    storyboard = new Storyboard();
                    storyboard.Children.Add(CreateDoubleAnim(entry.PlatformView, "Opacity", 1, 0, duration));
                    storyboard.Children.Add(CreateDoubleAnim(entry.PlatformView, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)", 1, 0.8, duration));
                    storyboard.Children.Add(CreateDoubleAnim(entry.PlatformView, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)", 1, 0.8, duration));
                    break;

                case PopupAnimation.SlideUp:
                    entry.PlatformView.RenderTransform = new TranslateTransform { Y = 0 };
                    storyboard = new Storyboard();
                    storyboard.Children.Add(CreateDoubleAnim(entry.PlatformView, "Opacity", 1, 0, duration));
                    storyboard.Children.Add(CreateDoubleAnim(entry.PlatformView, "(UIElement.RenderTransform).(TranslateTransform.Y)", 0, 300, duration));
                    break;

                case PopupAnimation.SlideDown:
                    entry.PlatformView.RenderTransform = new TranslateTransform { Y = 0 };
                    storyboard = new Storyboard();
                    storyboard.Children.Add(CreateDoubleAnim(entry.PlatformView, "Opacity", 1, 0, duration));
                    storyboard.Children.Add(CreateDoubleAnim(entry.PlatformView, "(UIElement.RenderTransform).(TranslateTransform.Y)", 0, -300, duration));
                    break;

                default: // Fade
                    storyboard = CreateFadeAnimation(entry.PlatformView, 1, 0, duration);
                    break;
            }

            storyboard.Completed += (s, e) =>
            {
                CloseAndCleanup(entry, page);
                tcs.TrySetResult(true);
            };
            storyboard.Begin();

            await tcs.Task;
        }

        private static void CloseAndCleanup(PopupEntry entry, BasePopupPage page)
        {
            entry.Popup.IsOpen = false;
            entry.Popup.Child = null;
            page.Handler?.DisconnectHandler();
        }

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            foreach (var entry in _popups.Values)
            {
                entry.PlatformView.Width = args.Size.Width;
                entry.PlatformView.Height = args.Size.Height;
            }
        }

        private static Storyboard CreateFadeAnimation(FrameworkElement target, double from, double to, int durationMs)
        {
            var storyboard = new Storyboard();
            storyboard.Children.Add(CreateDoubleAnim(target, "Opacity", from, to, durationMs));
            return storyboard;
        }

        private static DoubleAnimation CreateDoubleAnim(FrameworkElement target, string property, double from, double to, int durationMs)
        {
            var animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, property);

            return animation;
        }

        private static XamlRoot GetXamlRoot()
        {
            var platformView = PageResolver.GetCurrentPage()?.Handler?.PlatformView as FrameworkElement;
            return platformView?.XamlRoot;
        }

        private static Microsoft.UI.Xaml.Window GetCurrentWindow()
        {
            var mauiWindow = Microsoft.Maui.Controls.Application.Current?.Windows?.FirstOrDefault();
            return mauiWindow?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
        }

        private class PopupEntry
        {
            public WinPopup Popup { get; set; }
            public FrameworkElement PlatformView { get; set; }
            public BasePopupPage Page { get; set; }
            public Microsoft.UI.Xaml.Window Window { get; set; }
        }
    }
}
