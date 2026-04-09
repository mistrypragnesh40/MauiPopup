using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using MauiPopup.Services;
using MauiPopup.Views;
using Microsoft.Maui.Platform;

namespace MauiPopup.Platforms.Android
{
    internal class AndroidPopupService : IPopupPlatformService
    {
        private readonly Dictionary<BasePopupPage, Dialog> _dialogs = new();

        public bool IsSupported => true;

        public async Task ShowPopupAsync(BasePopupPage page)
        {
            var mauiContext = PageResolver.GetMauiContext();
            if (mauiContext == null)
                throw new InvalidOperationException("MauiContext is not available.");

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            if (activity == null)
                throw new InvalidOperationException("Current Activity is not available.");

            var platformView = page.ToPlatform(mauiContext);

            var dialog = new Dialog(activity);
            dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            dialog.SetCancelable(false);
            dialog.SetContentView(platformView);

            // Configure full-screen transparent window
            var window = dialog.Window;
            if (window != null)
            {
                window.SetBackgroundDrawable(new ColorDrawable(global::Android.Graphics.Color.Transparent));
                window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                window.SetDimAmount(0f);
                window.DecorView.SetPadding(0, 0, 0, 0);
            }

            dialog.SetOnKeyListener(new BackButtonKeyListener(page));
            _dialogs[page] = dialog;

            var duration = (long)page.AnimationDuration;

            // Set initial state and show
            switch (page.Animation)
            {
                case PopupAnimation.Fade:
                    platformView.Alpha = 0f;
                    dialog.Show();
                    platformView.Animate().Alpha(1f).SetDuration(duration).Start();
                    break;

                case PopupAnimation.Scale:
                    platformView.Alpha = 0f;
                    platformView.ScaleX = 0.8f;
                    platformView.ScaleY = 0.8f;
                    dialog.Show();
                    platformView.Animate()
                        .Alpha(1f).ScaleX(1f).ScaleY(1f)
                        .SetDuration(duration).Start();
                    break;

                case PopupAnimation.SlideUp:
                    platformView.Alpha = 0f;
                    platformView.TranslationY = 300f;
                    dialog.Show();
                    platformView.Animate()
                        .Alpha(1f).TranslationY(0f)
                        .SetDuration(duration).Start();
                    break;

                case PopupAnimation.SlideDown:
                    platformView.Alpha = 0f;
                    platformView.TranslationY = -300f;
                    dialog.Show();
                    platformView.Animate()
                        .Alpha(1f).TranslationY(0f)
                        .SetDuration(duration).Start();
                    break;

                default: // None
                    dialog.Show();
                    break;
            }

            await Task.CompletedTask;
        }

        public async Task DismissPopupAsync(BasePopupPage page)
        {
            if (!_dialogs.TryGetValue(page, out var dialog))
                return;

            _dialogs.Remove(page);

            var platformView = dialog.Window?.DecorView;
            if (platformView == null || page.Animation == PopupAnimation.None)
            {
                DismissAndCleanup(dialog, page);
                return;
            }

            var tcs = new TaskCompletionSource<bool>();
            var duration = (long)(page.AnimationDuration * 0.75);

            var animator = page.Animation switch
            {
                PopupAnimation.Scale => platformView.Animate()
                    .Alpha(0f).ScaleX(0.8f).ScaleY(0.8f).SetDuration(duration),

                PopupAnimation.SlideUp => platformView.Animate()
                    .Alpha(0f).TranslationY(300f).SetDuration(duration),

                PopupAnimation.SlideDown => platformView.Animate()
                    .Alpha(0f).TranslationY(-300f).SetDuration(duration),

                _ => platformView.Animate().Alpha(0f).SetDuration(duration), // Fade
            };

            animator.WithEndAction(new Java.Lang.Runnable(() =>
            {
                DismissAndCleanup(dialog, page);
                tcs.TrySetResult(true);
            })).Start();

            await tcs.Task;
        }

        private static void DismissAndCleanup(Dialog dialog, BasePopupPage page)
        {
            try
            {
                if (dialog.IsShowing)
                    dialog.Dismiss();
            }
            catch { }

            page.Handler?.DisconnectHandler();
        }

        private class BackButtonKeyListener : Java.Lang.Object, IDialogInterfaceOnKeyListener
        {
            private readonly BasePopupPage _page;

            public BackButtonKeyListener(BasePopupPage page)
            {
                _page = page;
            }

            public bool OnKey(IDialogInterface dialog, Keycode keyCode, KeyEvent e)
            {
                if (keyCode == Keycode.Back && e.Action == KeyEventActions.Up)
                {
                    if (_page.IsCloseOnBackgroundClick)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupAction.ClosePopup();
                        });
                        return true;
                    }
                    return true;
                }
                return false;
            }
        }
    }
}
