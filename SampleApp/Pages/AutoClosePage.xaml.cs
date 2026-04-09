using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class AutoClosePage : ContentPage
{
    public AutoClosePage()
    {
        InitializeComponent();
    }

    private async void OnAutoClose3sClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new AutoClosePopup(TimeSpan.FromSeconds(3)));
    }

    private async void OnAutoClose5sClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new AutoClosePopup(TimeSpan.FromSeconds(5)));
    }
}
