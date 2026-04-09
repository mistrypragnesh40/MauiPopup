using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class SimplePopup : BasePopupPage
{
    public SimplePopup()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
