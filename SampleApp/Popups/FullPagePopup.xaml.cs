using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class FullPagePopup : BasePopupPage
{
    public FullPagePopup()
    {
        InitializeComponent();
        VerticalOptions = LayoutOptions.Fill;
        HorizontalOptions = LayoutOptions.Fill;
        Margin = new Thickness(0);
        ForegroundColor = Colors.White;
        IsCloseOnBackgroundClick = false;
        Animation = PopupAnimation.Scale;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
