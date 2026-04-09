using MauiPopup;
using MauiPopup.Views;

namespace SampleApp.Popups;

public partial class ReturnObjectPopup : BasePopupPage
{
    public ReturnObjectPopup()
    {
        InitializeComponent();
        IsCloseOnBackgroundClick = false;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var user = new UserInfo
        {
            Name = NameEntry.Text ?? "",
            Age = int.TryParse(AgeEntry.Text, out var age) ? age : 0
        };
        await PopupAction.ClosePopup(user);
    }
}

public class UserInfo
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}
