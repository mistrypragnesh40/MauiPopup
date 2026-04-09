using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class BasicPage : ContentPage
{
    public BasicPage()
    {
        InitializeComponent();
    }

    private async void OnSimplePopupClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new SimplePopup());
    }

    private async void OnReturnStringClicked(object sender, EventArgs e)
    {
        string result = await PopupAction.DisplayPopup(new ReturnStringPopup());
        StringResultLabel.Text = string.IsNullOrEmpty(result)
            ? "Result: (cancelled or empty)"
            : $"Result: \"{result}\"";
    }

    private async void OnReturnObjectClicked(object sender, EventArgs e)
    {
        var result = await PopupAction.DisplayPopup<UserInfo>(new ReturnObjectPopup());
        ObjectResultLabel.Text = result != null
            ? $"Result: Name={result.Name}, Age={result.Age}"
            : "Result: (null)";
    }

    private async void OnConfirmPopupClicked(object sender, EventArgs e)
    {
        var result = await PopupAction.DisplayPopup<ConfirmResult>(new ConfirmPopup());
        BoolResultLabel.Text = result != null
            ? $"Result: Confirmed = {result.IsConfirmed}"
            : "Result: (null)";
    }

    private async void OnStackedPopupClicked(object sender, EventArgs e)
    {
        await PopupAction.DisplayPopup(new StackedPopup(1));
    }
}
