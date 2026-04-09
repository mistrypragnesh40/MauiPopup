using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class ConfirmPopup : BasePopupPage
{
    public ConfirmPopup()
    {
        InitializeComponent();
        IsCloseOnBackgroundClick = false;
    }

    private async void OnYesClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup(new ConfirmResult { IsConfirmed = true });
    }

    private async void OnNoClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup(new ConfirmResult { IsConfirmed = false });
    }
}

public class ConfirmResult
{
    public bool IsConfirmed { get; set; }
}
