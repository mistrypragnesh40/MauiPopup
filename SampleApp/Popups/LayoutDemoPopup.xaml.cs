using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class LayoutDemoPopup : BasePopupPage
{
    public LayoutDemoPopup(string position)
    {
        InitializeComponent();
        PositionLabel.Text = $"Position: {position}";
        ForegroundColor = Colors.White;
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
