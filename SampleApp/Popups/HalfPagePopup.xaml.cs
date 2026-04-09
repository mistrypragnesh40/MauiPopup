using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class HalfPagePopup : BasePopupPage
{
    public HalfPagePopup()
    {
        InitializeComponent();
        VerticalOptions = LayoutOptions.Start;
        HorizontalOptions = LayoutOptions.Fill;
        Margin = new Thickness(0);
        ForegroundColor = Colors.White;
        IsCloseOnBackgroundClick = true;
        Animation = PopupAnimation.SlideDown;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
