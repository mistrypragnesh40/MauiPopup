using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class DismissedEventPage : ContentPage
{
    public DismissedEventPage()
    {
        InitializeComponent();
    }

    private async void OnDismissedEventClicked(object sender, EventArgs e)
    {
        var popup = new ReturnStringPopup();
        popup.Dismissed += (s, args) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var result = args.Result as string;
                DismissedResultLabel.Text = string.IsNullOrEmpty(result)
                    ? "Dismissed result: (cancelled or empty)"
                    : $"Dismissed result: \"{result}\"";
            });
        };
        await PopupAction.DisplayPopup(popup);
    }
}
