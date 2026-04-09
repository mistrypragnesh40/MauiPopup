using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class ColorDemoPopup : BasePopupPage
{
    private readonly Color _textColor;

    public ColorDemoPopup(string title, string description, Color? textColor = null)
    {
        InitializeComponent();

        _textColor = textColor ?? Colors.Black;

        TitleLabel.Text = title;
        TitleLabel.TextColor = _textColor;

        DescriptionLabel.Text = description;
        DescriptionLabel.TextColor = _textColor;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        FgLabel.Text = $"ForegroundColor: {ForegroundColor.ToArgbHex()}";
        OvLabel.Text = $"OverlayColor: {OverlayColor.ToArgbHex()}";
        FgLabel.TextColor = _textColor.WithAlpha(0.6f);
        OvLabel.TextColor = _textColor.WithAlpha(0.6f);
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
