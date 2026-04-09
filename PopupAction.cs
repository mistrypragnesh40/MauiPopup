using MauiPopup.Services;
using MauiPopup.Views;

namespace MauiPopup
{
    public class PopupAction
    {
        /// <summary>
        /// Display a popup and return a typed result.
        /// </summary>
        public static async Task<T> DisplayPopup<T>(BasePopupPage page)
        {
            try
            {
                await PopupManager.ShowAsync(page);
                var result = await page.returnResultTask.Task;
                return result is T typed ? typed : default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Display a popup and return a string result.
        /// </summary>
        public static async Task<string> DisplayPopup(BasePopupPage page)
        {
            try
            {
                await PopupManager.ShowAsync(page);
                var result = await page.returnResultTask.Task;
                return result as string ?? "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Close the topmost popup with an optional return value.
        /// </summary>
        public static async Task ClosePopup(object returnValue = null)
        {
            try
            {
                await PopupManager.CloseCurrentAsync(returnValue);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Close all open popups at once, from top to bottom.
        /// </summary>
        public static async Task CloseAllPopups()
        {
            try
            {
                await PopupManager.CloseAllAsync();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Returns true if any popup is currently open.
        /// </summary>
        public static bool IsPopupOpen => PopupManager.IsPopupOpen;
    }
}
