using MauiPopup;
using SampleApp.Popups;

namespace SampleApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Test 1: Simple popup with background dismiss
    private async void OnSimplePopupClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new SimplePopup());
    }

    // Test 2: Return a string value
    private async void OnReturnStringClicked(object sender, EventArgs e)
    {
        string result = await PopupAction.DisplayPopup(new ReturnStringPopup());
        StringResultLabel.Text = string.IsNullOrEmpty(result)
            ? "Result: (cancelled or empty)"
            : $"Result: \"{result}\"";
    }

    // Test 3: Return a typed object
    private async void OnReturnObjectClicked(object sender, EventArgs e)
    {
        var result = await PopupAction.DisplayPopup<UserInfo>(new ReturnObjectPopup());
        ObjectResultLabel.Text = result != null
            ? $"Result: Name={result.Name}, Age={result.Age}"
            : "Result: (null)";
    }

    // Test 4: Positioned popup (bottom sheet)
    private async void OnPositionPopupClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new PositionPopup());
    }

    // Test 5: Stacked popups
    private async void OnStackedPopupClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new StackedPopup(1));
    }

    // Test 6: Return bool (confirm dialog)
    private async void OnConfirmPopupClicked(object sender, EventArgs e)
    {
        var result = await PopupAction.DisplayPopup<ConfirmResult>(new ConfirmPopup());
        BoolResultLabel.Text = result != null
            ? $"Result: Confirmed = {result.IsConfirmed}"
            : "Result: (null)";
    }

    // Test 7: Animation types
    private async void OnFadeAnimationClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.Fade };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnScaleAnimationClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.Scale };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnSlideUpAnimationClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.SlideUp };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnSlideDownAnimationClicked(object sender, EventArgs e)
    {
        var popup = new SimplePopup { Animation = PopupAnimation.SlideDown };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnNoAnimationClicked(object sender, EventArgs e)
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
}
