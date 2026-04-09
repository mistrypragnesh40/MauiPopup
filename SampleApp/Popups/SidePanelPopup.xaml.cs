using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class SidePanelPopup : BasePopupPage
{
    public SidePanelPopup()
    {
        InitializeComponent();
        VerticalOptions = LayoutOptions.Fill;
        HorizontalOptions = LayoutOptions.End;
        Margin = new Thickness(0);
        ForegroundColor = Colors.White;
        Animation = PopupAnimation.Fade;
        AnimationDuration = 200;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
