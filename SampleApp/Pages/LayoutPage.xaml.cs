using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class LayoutPage : ContentPage
{
    public LayoutPage()
    {
        InitializeComponent();
    }

    private async void OnCenterClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Center")
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnTopClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Top")
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 50, 0, 0),
            Animation = PopupAnimation.SlideDown
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnBottomClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Bottom")
        {
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, 50),
            Animation = PopupAnimation.SlideUp
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnLeftClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Left")
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start,
            Margin = new Thickness(20, 0, 0, 0)
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnRightClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Right")
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 20, 0)
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnTopLeftClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Top-Left")
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Start,
            Margin = new Thickness(20, 50, 0, 0)
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnBottomRightClicked(object sender, EventArgs e)
    {
        var popup = new LayoutDemoPopup("Bottom-Right")
        {
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 20, 50)
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnBottomSheetClicked(object sender, EventArgs e)
    {
        var popup = new PositionPopup
        {
            Animation = PopupAnimation.SlideUp
        };
        await PopupAction.DisplayPopup(popup);
    }
}
