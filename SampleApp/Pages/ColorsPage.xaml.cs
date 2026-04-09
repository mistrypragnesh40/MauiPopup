using MauiPopup;
using SampleApp.Popups;

namespace SampleApp.Pages;

public partial class ColorsPage : ContentPage
{
    public ColorsPage()
    {
        InitializeComponent();
    }

    // ---- ForegroundColor demos ----

    private async void OnFgWhiteClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("ForegroundColor: White", "Default light theme look")
        {
            ForegroundColor = Colors.White
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnFgLightBlueClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("ForegroundColor: LightBlue", "Popup content area is light blue")
        {
            ForegroundColor = Color.FromArgb("#E3F2FD")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnFgYellowClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("ForegroundColor: LightYellow", "Popup content area is light yellow")
        {
            ForegroundColor = Color.FromArgb("#FFF9C4")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnFgCoralClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("ForegroundColor: LightCoral", "Popup content area is light coral")
        {
            ForegroundColor = Color.FromArgb("#FFCDD2")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnFgTransparentClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("ForegroundColor: Transparent", "No card background — content floats on overlay")
        {
            ForegroundColor = Colors.Transparent
        };
        await PopupAction.DisplayPopup(popup);
    }

    // ---- OverlayColor demos ----

    private async void OnOverlayDefaultClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("OverlayColor: Default", "50% black overlay (#80000000)")
        {
            ForegroundColor = Colors.White
        };
        // Default overlay, no change needed
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnOverlayDarkClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("OverlayColor: Dark", "80% black overlay — much darker")
        {
            ForegroundColor = Colors.White,
            OverlayColor = Color.FromArgb("#CC000000")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnOverlayLightClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("OverlayColor: Light", "20% black overlay — barely visible")
        {
            ForegroundColor = Colors.White,
            OverlayColor = Color.FromArgb("#33000000")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnOverlayBlueClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("OverlayColor: Blue Tint", "Semi-transparent blue overlay")
        {
            ForegroundColor = Colors.White,
            OverlayColor = Color.FromArgb("#801565C0")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnOverlayRedClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("OverlayColor: Red Tint", "Semi-transparent red overlay")
        {
            ForegroundColor = Colors.White,
            OverlayColor = Color.FromArgb("#80C62828")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnOverlayNoneClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("OverlayColor: Transparent", "No overlay — page is fully visible behind")
        {
            ForegroundColor = Colors.White,
            OverlayColor = Colors.Transparent
        };
        await PopupAction.DisplayPopup(popup);
    }

    // ---- Combined theme demos ----

    private async void OnDarkThemeClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("Dark Theme", "Dark card on dark overlay", Colors.White)
        {
            ForegroundColor = Color.FromArgb("#1E1E1E"),
            OverlayColor = Color.FromArgb("#CC000000")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnSuccessThemeClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("Success Theme", "Green card on green-tinted overlay", Colors.White)
        {
            ForegroundColor = Color.FromArgb("#2E7D32"),
            OverlayColor = Color.FromArgb("#4D1B5E20")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnWarningThemeClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("Warning Theme", "Orange card on warm overlay", Colors.Black)
        {
            ForegroundColor = Color.FromArgb("#FF9800"),
            OverlayColor = Color.FromArgb("#4DE65100")
        };
        await PopupAction.DisplayPopup(popup);
    }

    private async void OnFrostedGlassClicked(object sender, EventArgs e)
    {
        var popup = new ColorDemoPopup("Frosted Glass", "Semi-transparent white on light overlay")
        {
            ForegroundColor = Color.FromArgb("#E0FFFFFF"),
            OverlayColor = Color.FromArgb("#40FFFFFF"),
            Animation = PopupAnimation.Scale
        };
        await PopupAction.DisplayPopup(popup);
    }
}
