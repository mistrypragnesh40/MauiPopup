using MauiPopup.Views;

namespace MauiPopup
{
    // All the code in this file is included in all platforms.
    public class PopupAction
    {
        public static async Task DisplayPopup(BasePopupPage page)
        {
            if (Application.Current.MainPage != null)
                await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public static async Task ClosePopup()
        {
            if (Application.Current.MainPage != null)
                await Application.Current.MainPage.Navigation.PopModalAsync();
        }

    }
}