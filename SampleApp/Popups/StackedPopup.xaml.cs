using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class StackedPopup : BasePopupPage
{
    private readonly int _level;

    public StackedPopup(int level)
    {
        InitializeComponent();
        _level = level;
        TitleLabel.Text = $"Popup Level {level}";
        MessageLabel.Text = $"This is stacked popup #{level}. You can open more on top!";
        ForegroundColor = level switch
        {
            1 => Colors.White,
            2 => Colors.LightBlue,
            3 => Colors.LightGreen,
            _ => Colors.LightYellow
        };
    }

    private async void OnOpenAnotherClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new StackedPopup(_level + 1));
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
