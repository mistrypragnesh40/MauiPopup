using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class AutoClosePopup : BasePopupPage
{
    public AutoClosePopup(TimeSpan duration)
    {
        InitializeComponent();
        AutoCloseAfter = duration;
        CornerRadius = new CornerRadius(16);
        ForegroundColor = Colors.White;
        TimerLabel.Text = $"Closing in {duration.TotalSeconds:0} seconds...";
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
