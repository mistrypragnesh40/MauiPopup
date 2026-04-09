using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class PositionPopup : BasePopupPage
{
    public PositionPopup()
    {
        InitializeComponent();
        VerticalOptions = LayoutOptions.End;
        HorizontalOptions = LayoutOptions.Fill;
        Margin = new Thickness(0, 0, 0, 0);
        ForegroundColor = Colors.White;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
