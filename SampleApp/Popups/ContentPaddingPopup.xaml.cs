using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class ContentPaddingPopup : BasePopupPage
{
    public ContentPaddingPopup(Thickness padding)
    {
        InitializeComponent();
        ContentPadding = padding;
        CornerRadius = new CornerRadius(16);
        ForegroundColor = Colors.White;
        PaddingLabel.Text = $"ContentPadding: {padding.Left}, {padding.Top}, {padding.Right}, {padding.Bottom}";
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
