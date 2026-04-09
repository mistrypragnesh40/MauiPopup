using MauiPopup.Views;

namespace MauiPopup.Services
{
    internal class FallbackPopupService : IPopupPlatformService
    {
        public bool IsSupported => true;

        public async Task ShowPopupAsync(BasePopupPage page)
        {
            var currentPage = PageResolver.GetCurrentPage();
            if (currentPage != null)
                await currentPage.Navigation.PushModalAsync(page);
        }

        public async Task DismissPopupAsync(BasePopupPage page)
        {
            var currentPage = PageResolver.GetCurrentPage();
            if (currentPage != null && currentPage.Navigation.ModalStack.Count > 0)
                await currentPage.Navigation.PopModalAsync();
        }
    }
}
