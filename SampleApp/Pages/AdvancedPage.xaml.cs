using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class AdvancedPage : ContentPage
{
    public AdvancedPage()
    {
        InitializeComponent();
    }

    private async void OnFullPageClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new FullPagePopup());
    }

    private async void OnHalfTopClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new HalfPagePopup());
    }

    private async void OnHalfBottomClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new HalfBottomPopup());
    }

    private async void OnNotificationClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Notification")
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Fill,
            Margin = new Thickness(10, 50, 10, 0),
            ForegroundColor = Color.FromArgb("#4CAF50"),
            Animation = PopupAnimation.SlideDown,
            AnimationDuration = 250
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnSidePanelClicked(object sender, EventArgs e)
    {
        var popup = new SidePanelPopup();
        await PopupAction.DisplayPopup(popup);
    }
}
