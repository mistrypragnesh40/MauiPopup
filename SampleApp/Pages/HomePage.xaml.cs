namespace SampleApp.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnBasicClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new BasicPage());

    private async void OnAnimationsClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new AnimationPage());

    private async void OnLayoutsClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new LayoutPage());

    private async void OnAdvancedClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new AdvancedPage());

    private async void OnColorsClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new ColorsPage());

    private async void OnCornerRadiusClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new CornerRadiusPage());

    private async void OnContentPaddingClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new ContentPaddingPage());

    private async void OnAutoCloseClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new AutoClosePage());

    private async void OnCloseAllClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new CloseAllPage());

    private async void OnDismissedClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new DismissedEventPage());
}
