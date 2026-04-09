using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class ContentPaddingPage : ContentPage
{
    public ContentPaddingPage()
    {
        InitializeComponent();
    }

    private async void OnSmallClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new ContentPaddingPopup(new Thickness(10)));
    }

    private async void OnLargeClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new ContentPaddingPopup(new Thickness(30)));
    }

    private async void OnAsymmetricClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new ContentPaddingPopup(new Thickness(10, 30, 10, 30)));
    }
}
