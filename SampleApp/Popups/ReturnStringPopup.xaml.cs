using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class ReturnStringPopup : BasePopupPage
{
    public ReturnStringPopup()
    {
        InitializeComponent();
        IsCloseOnBackgroundClick = false;
    }

    private async void OnOkClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup(InputEntry.Text ?? "");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup("");
    }
}
