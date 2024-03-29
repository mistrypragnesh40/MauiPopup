using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiPopup.Views;

public partial class BasePopupPage : ContentPage
{
    public TaskCompletionSource<object> returnResultTask = new();
    public BasePopupPage()
    {
        InitializeComponent();
        var current_theme = Application.Current.RequestedTheme;
        this.BackgroundColor = Color.FromArgb("#80000000");
        if (current_theme == AppTheme.Light)
            ForegroundColor = Colors.White;
        else
            ForegroundColor = Color.FromArgb("#ff282828");
    }


    public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(
   propertyName: nameof(ForegroundColor),
   returnType: typeof(Color),
   declaringType: typeof(BasePopupPage),
   defaultValue: Colors.Transparent,
   defaultBindingMode: BindingMode.OneWay);

    public Color ForegroundColor
    {
        get => (Color)GetValue(ForegroundColorProperty);
        set { SetValue(ForegroundColorProperty, value); }
    }

    public bool IsCloseOnBackgroundClick { get; set; } = true;

    public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create(
         propertyName: nameof(VerticalOptions),
         returnType: typeof(LayoutOptions),
         declaringType: typeof(BasePopupPage),
         defaultValue: LayoutOptions.Center,
         defaultBindingMode: BindingMode.OneWay);

    public LayoutOptions VerticalOptions
    {
        get => (LayoutOptions)GetValue(VerticalOptionsProperty);
        set { SetValue(VerticalOptionsProperty, value); }
    }

    public static readonly BindableProperty HorizontalOptionsProperty = BindableProperty.Create(
        propertyName: nameof(HorizontalOptions),
        returnType: typeof(LayoutOptions),
        declaringType: typeof(BasePopupPage),
        defaultValue: LayoutOptions.Center,
        defaultBindingMode: BindingMode.OneWay);

    public LayoutOptions HorizontalOptions
    {
        get => (LayoutOptions)GetValue(HorizontalOptionsProperty);
        set { SetValue(HorizontalOptionsProperty, value); }
    }

    public static readonly BindableProperty MarginProperty = BindableProperty.Create(
        propertyName: nameof(Margin),
        returnType: typeof(Thickness),
        declaringType: typeof(BasePopupPage),
        defaultBindingMode: BindingMode.OneWay);

    public Thickness Margin
    {
        get => (Thickness)GetValue(MarginProperty);
        set { SetValue(MarginProperty, value); }
    }
    public ICommand PopModelCommand => new Command(async () =>
    {
        if (IsCloseOnBackgroundClick)
            await PopupAction.ClosePopup();
    });


    protected override bool OnBackButtonPressed()
    {
        if (!IsCloseOnBackgroundClick)
            return true;

        return base.OnBackButtonPressed();
    }

}
