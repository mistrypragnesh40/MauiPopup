using MauiPopup.Views;

namespace MauiPopup.Services
{
    internal interface IPopupPlatformService
    {
        bool IsSupported { get; }
        Task ShowPopupAsync(BasePopupPage page);
        Task DismissPopupAsync(BasePopupPage page);
    }
}
