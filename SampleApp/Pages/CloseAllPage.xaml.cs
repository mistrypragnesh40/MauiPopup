using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class CloseAllPage : ContentPage
{
    public CloseAllPage()
    {
        InitializeComponent();
    }

    private async void OnCloseAllClicked(object sender, EventArgs e)
    {
        // Open 3 stacked popups
        _ = PopupAction.DisplayPopup(new StackedPopup(1));
        await Task.Delay(400);
        _ = PopupAction.DisplayPopup(new StackedPopup(2));
        await Task.Delay(400);
        _ = PopupAction.DisplayPopup(new StackedPopup(3));
        await Task.Delay(1500);

        // Close them all at once
        await PopupAction.CloseAllPopups();
    }
}
