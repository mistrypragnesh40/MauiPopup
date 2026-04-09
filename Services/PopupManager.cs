using System.Collections.Concurrent;
using MauiPopup.Views;

namespace MauiPopup.Services
{
    internal static class PopupManager
    {
        private static readonly ConcurrentStack<BasePopupPage> _popupStack = new();
        private static IPopupPlatformService _platformService;
        private static readonly Lazy<FallbackPopupService> _fallback = new();
        private static readonly object _lock = new();
        private static bool _useFallback;

        internal static bool IsPopupOpen => !_popupStack.IsEmpty;

        internal static IPopupPlatformService PlatformService
        {
            get
            {
                if (_platformService == null)
                {
                    lock (_lock)
                    {
                        _platformService ??= CreatePlatformService();
                    }
                }
                return _platformService;
            }
        }

        private static IPopupPlatformService CreatePlatformService()
        {
#if ANDROID
            return new MauiPopup.Platforms.Android.AndroidPopupService();
#elif IOS
            return new MauiPopup.Platforms.iOS.iOSPopupService();
#elif MACCATALYST
            return new MauiPopup.Platforms.MacCatalyst.MacCatalystPopupService();
#elif WINDOWS
            return new MauiPopup.Platforms.Windows.WindowsPopupService();
#else
            return new FallbackPopupService();
#endif
        }

        internal static async Task ShowAsync(BasePopupPage page)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    if (!_useFallback && PlatformService.IsSupported)
                    {
                        await PlatformService.ShowPopupAsync(page);
                        _popupStack.Push(page);
                        page.NotifyAppearing();
                        page.StartAutoCloseTimer();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[MauiPopup] Platform service failed, falling back to modal navigation: {ex.Message}");
                    _useFallback = true;
                }

                await _fallback.Value.ShowPopupAsync(page);
                _popupStack.Push(page);
                page.StartAutoCloseTimer();
            });
        }

        /// <summary>
        /// Atomically pops the current popup, sets the result, and dismisses it.
        /// This prevents race conditions between getting the popup and dismissing it.
        /// </summary>
        internal static async Task CloseCurrentAsync(object returnValue)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (!_popupStack.TryPop(out var page))
                    return;

                page.CancelAutoCloseTimer();
                page.returnResultTask.TrySetResult(returnValue);
                page.NotifyDisappearing();
                page.RaiseDismissed(returnValue);

                try
                {
                    if (!_useFallback && PlatformService.IsSupported)
                    {
                        await PlatformService.DismissPopupAsync(page);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[MauiPopup] Platform dismiss failed, falling back: {ex.Message}");
                    _useFallback = true;
                }

                await _fallback.Value.DismissPopupAsync(page);
            });
        }

        internal static async Task CloseAllAsync()
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                while (_popupStack.TryPop(out var page))
                {
                    page.CancelAutoCloseTimer();
                    page.returnResultTask.TrySetResult(null);
                    page.NotifyDisappearing();
                    page.RaiseDismissed(null);

                    try
                    {
                        if (!_useFallback && PlatformService.IsSupported)
                        {
                            await PlatformService.DismissPopupAsync(page);
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"[MauiPopup] Platform dismiss failed, falling back: {ex.Message}");
                        _useFallback = true;
                    }

                    await _fallback.Value.DismissPopupAsync(page);
                }
            });
        }

        internal static BasePopupPage CurrentPopup
        {
            get
            {
                _popupStack.TryPeek(out var page);
                return page;
            }
        }
    }
}
