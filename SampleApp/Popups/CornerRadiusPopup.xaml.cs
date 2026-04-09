using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class CornerRadiusPopup : BasePopupPage
{
    public CornerRadiusPopup(CornerRadius radius)
    {
        InitializeComponent();
        CornerRadius = radius;
        ForegroundColor = Colors.White;
        RadiusLabel.Text = $"CornerRadius: {radius.TopLeft}, {radius.TopRight}, {radius.BottomRight}, {radius.BottomLeft}";
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
