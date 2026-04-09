using CoreGraphics;
using Foundation;
using MauiPopup.Services;
using MauiPopup.Views;
using Microsoft.Maui.Platform;
using UIKit;

namespace MauiPopup.Platforms.MacCatalyst
{
    internal class MacCatalystPopupService : IPopupPlatformService
    {
        private readonly Dictionary<BasePopupPage, (UIView View, NSObject Observer)> _overlays = new();

        public bool IsSupported => true;

        public async Task ShowPopupAsync(BasePopupPage page)
        {
            var mauiContext = PageResolver.GetMauiContext();
            if (mauiContext == null)
                throw new InvalidOperationException("MauiContext is not available.");

            var window = GetKeyWindow();
            if (window == null)
                throw new InvalidOperationException("Key window is not available.");

            var platformView = page.ToPlatform(mauiContext);

            platformView.Frame = window.Bounds;
            platformView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            var observer = NSNotificationCenter.DefaultCenter.AddObserver(
                UIWindow.DidBecomeVisibleNotification,
                _ => UpdateFrame(platformView, window));

            _overlays[page] = (platformView, observer);

            var duration = page.AnimationDuration / 1000.0;

            // Set initial state
            switch (page.Animation)
            {
                case PopupAnimation.Fade:
                    platformView.Alpha = 0f;
                    break;
                case PopupAnimation.Scale:
                    platformView.Alpha = 0f;
                    platformView.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);
                    break;
                case PopupAnimation.SlideUp:
                    platformView.Alpha = 0f;
                    platformView.Transform = CGAffineTransform.MakeTranslation(0, window.Bounds.Height);
                    break;
                case PopupAnimation.SlideDown:
                    platformView.Alpha = 0f;
                    platformView.Transform = CGAffineTransform.MakeTranslation(0, -window.Bounds.Height);
                    break;
            }

            window.AddSubview(platformView);

            if (page.Animation == PopupAnimation.None)
                return;

            await UIView.AnimateAsync(duration, () =>
            {
                platformView.Alpha = 1f;
                platformView.Transform = CGAffineTransform.MakeIdentity();
            });
        }

        public async Task DismissPopupAsync(BasePopupPage page)
        {
            if (!_overlays.TryGetValue(page, out var entry))
                return;

            _overlays.Remove(page);
            NSNotificationCenter.DefaultCenter.RemoveObserver(entry.Observer);

            if (page.Animation == PopupAnimation.None)
            {
                entry.View.RemoveFromSuperview();
                page.Handler?.DisconnectHandler();
                return;
            }

            var duration = page.AnimationDuration * 0.75 / 1000.0;
            var window = entry.View.Window;

            await UIView.AnimateAsync(duration, () =>
            {
                entry.View.Alpha = 0f;
                entry.View.Transform = page.Animation switch
                {
                    PopupAnimation.Scale => CGAffineTransform.MakeScale(0.8f, 0.8f),
                    PopupAnimation.SlideUp => CGAffineTransform.MakeTranslation(0, window?.Bounds.Height ?? 800),
                    PopupAnimation.SlideDown => CGAffineTransform.MakeTranslation(0, -(window?.Bounds.Height ?? 800)),
                    _ => CGAffineTransform.MakeIdentity(),
                };
            });

            entry.View.RemoveFromSuperview();
            page.Handler?.DisconnectHandler();
        }

        private static void UpdateFrame(UIView view, UIWindow window)
        {
            view.Frame = window.Bounds;
        }

        private static UIWindow GetKeyWindow()
        {
            var scenes = UIApplication.SharedApplication.ConnectedScenes;
            foreach (var scene in scenes)
            {
                if (scene is UIWindowScene windowScene)
                {
                    foreach (var window in windowScene.Windows)
                    {
                        if (window.IsKeyWindow)
                            return window;
                    }
                }
            }

#pragma warning disable CA1422
            return UIApplication.SharedApplication.KeyWindow;
#pragma warning restore CA1422
        }
    }
}
