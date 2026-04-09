namespace MauiPopup.Services
{
    internal static class PageResolver
    {
        internal static Page GetCurrentPage()
        {
#pragma warning disable CS0618 // Suppress MainPage deprecation for backward compat
            return Microsoft.Maui.Controls.Application.Current?.MainPage
                ?? Microsoft.Maui.Controls.Application.Current?.Windows?.FirstOrDefault()?.Page;
#pragma warning restore CS0618
        }

        internal static IMauiContext GetMauiContext()
        {
            return GetCurrentPage()?.Handler?.MauiContext;
        }
    }
}
