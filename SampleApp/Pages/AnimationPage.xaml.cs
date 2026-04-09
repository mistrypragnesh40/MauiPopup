using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class AnimationPage : ContentPage
{
    public AnimationPage()
    {
        InitializeComponent();
    }

    private async void OnFadeClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.Fade };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnScaleClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.Scale };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnSlideUpClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.SlideUp };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnSlideDownClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.SlideDown };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnNoneClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.None };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnSlowFadeClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup
        {
            Animation = PopupAnimation.Fade,
            AnimationDuration = 400
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnAnimatedBottomSheetClicked(object sender, EventArgs e)
    {
        var popup = new PositionPopup
        {
            Animation = PopupAnimation.SlideUp
        };
        await PopupAction.DisplayPopup(popup);
    }
}
