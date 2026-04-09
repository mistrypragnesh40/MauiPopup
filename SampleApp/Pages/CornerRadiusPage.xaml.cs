using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class CornerRadiusPage : ContentPage
{
    public CornerRadiusPage()
    {
        InitializeComponent();
    }

    private async void OnUniformClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new CornerRadiusPopup(new CornerRadius(20)));
    }

    private async void OnTopOnlyClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new CornerRadiusPopup(new CornerRadius(20, 20, 0, 0)));
    }

    private async void OnLargeClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new CornerRadiusPopup(new CornerRadius(40)));
    }
}
