using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class HalfBottomPopup : BasePopupPage
{
    public HalfBottomPopup()
    {
        InitializeComponent();
        VerticalOptions = LayoutOptions.End;
        HorizontalOptions = LayoutOptions.Fill;
        Margin = new Thickness(0);
        ForegroundColor = Colors.White;
        Animation = PopupAnimation.SlideUp;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
