using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiPopup.Views;

public partial class BasePopupPage : ContentPage
{
    private ICommand _popModelCommand;
    private CancellationTokenSource _autoCloseCts;

    public TaskCompletionSource<object> returnResultTask = new();

    public BasePopupPage()
    {
        InitializeComponent();
        var current_theme = Microsoft.Maui.Controls.Application.Current.RequestedTheme;
        BackgroundColor = Colors.Transparent;
        if (current_theme == AppTheme.Light)
            ForegroundColor = Colors.White;
        else
            ForegroundColor = Color.FromArgb("#ff282828");
    }

    // ---- Color Properties ----

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

    public static readonly BindableProperty OverlayColorProperty = BindableProperty.Create(
        propertyName: nameof(OverlayColor),
        returnType: typeof(Color),
        declaringType: typeof(BasePopupPage),
        defaultValue: Color.FromArgb("#80000000"),
        defaultBindingMode: BindingMode.OneWay);

    public Color OverlayColor
    {
        get => (Color)GetValue(OverlayColorProperty);
        set { SetValue(OverlayColorProperty, value); }
    }

    // ---- Behavior Properties ----

    public static readonly BindableProperty IsCloseOnBackgroundClickProperty = BindableProperty.Create(
        propertyName: nameof(IsCloseOnBackgroundClick),
        returnType: typeof(bool),
        declaringType: typeof(BasePopupPage),
        defaultValue: true,
        defaultBindingMode: BindingMode.OneWay);

    public bool IsCloseOnBackgroundClick
    {
        get => (bool)GetValue(IsCloseOnBackgroundClickProperty);
        set { SetValue(IsCloseOnBackgroundClickProperty, value); }
    }

    // ---- Animation Properties ----

    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(
        propertyName: nameof(Animation),
        returnType: typeof(PopupAnimation),
        declaringType: typeof(BasePopupPage),
        defaultValue: PopupAnimation.Fade,
        defaultBindingMode: BindingMode.OneWay);

    public PopupAnimation Animation
    {
        get => (PopupAnimation)GetValue(AnimationProperty);
        set { SetValue(AnimationProperty, value); }
    }

    public static readonly BindableProperty AnimationDurationProperty = BindableProperty.Create(
        propertyName: nameof(AnimationDuration),
        returnType: typeof(uint),
        declaringType: typeof(BasePopupPage),
        defaultValue: (uint)200,
        defaultBindingMode: BindingMode.OneWay);

    public uint AnimationDuration
    {
        get => (uint)GetValue(AnimationDurationProperty);
        set { SetValue(AnimationDurationProperty, value); }
    }

    // ---- Layout Properties ----

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

    // ---- CornerRadius Property ----

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        propertyName: nameof(CornerRadius),
        returnType: typeof(CornerRadius),
        declaringType: typeof(BasePopupPage),
        defaultValue: new CornerRadius(0),
        defaultBindingMode: BindingMode.OneWay,
        propertyChanged: OnCornerRadiusChanged);

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Converts CornerRadius to a RoundRectangle StrokeShape for the Border
    public IShape PopupStrokeShape => new RoundRectangle
    {
        CornerRadius = CornerRadius
    };

    private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var page = (BasePopupPage)bindable;
        page.OnPropertyChanged(nameof(PopupStrokeShape));
    }

    // ---- ContentPadding Property ----

    public static readonly BindableProperty ContentPaddingProperty = BindableProperty.Create(
        propertyName: nameof(ContentPadding),
        returnType: typeof(Thickness),
        declaringType: typeof(BasePopupPage),
        defaultValue: new Thickness(0),
        defaultBindingMode: BindingMode.OneWay);

    public Thickness ContentPadding
    {
        get => (Thickness)GetValue(ContentPaddingProperty);
        set { SetValue(ContentPaddingProperty, value); }
    }

    // ---- AutoCloseAfter Property ----

    public static readonly BindableProperty AutoCloseAfterProperty = BindableProperty.Create(
        propertyName: nameof(AutoCloseAfter),
        returnType: typeof(TimeSpan?),
        declaringType: typeof(BasePopupPage),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public TimeSpan? AutoCloseAfter
    {
        get => (TimeSpan?)GetValue(AutoCloseAfterProperty);
        set { SetValue(AutoCloseAfterProperty, value); }
    }

    // ---- OnDismissed Event ----

    public event EventHandler<PopupDismissedEventArgs> Dismissed;

    protected virtual void OnDismissed(PopupDismissedEventArgs e)
    {
        Dismissed?.Invoke(this, e);
    }

    // ---- Command (cached) ----

    public ICommand PopModelCommand => _popModelCommand ??= new Command(async () =>
    {
        if (IsCloseOnBackgroundClick)
            await PopupAction.ClosePopup();
    });

    // ---- Overrides ----

    protected override bool OnBackButtonPressed()
    {
        if (!IsCloseOnBackgroundClick)
            return true;

        return base.OnBackButtonPressed();
    }

    internal void NotifyAppearing()
    {
        SendAppearing();
    }

    internal void NotifyDisappearing()
    {
        SendDisappearing();
    }

    internal void StartAutoCloseTimer()
    {
        if (AutoCloseAfter is not { } duration || duration <= TimeSpan.Zero)
            return;

        _autoCloseCts = new CancellationTokenSource();
        var token = _autoCloseCts.Token;

        _ = Task.Delay(duration, token).ContinueWith(async _ =>
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await PopupAction.ClosePopup();
            });
        }, token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
    }

    internal void CancelAutoCloseTimer()
    {
        _autoCloseCts?.Cancel();
        _autoCloseCts?.Dispose();
        _autoCloseCts = null;
    }

    internal void RaiseDismissed(object result)
    {
        OnDismissed(new PopupDismissedEventArgs(result));
    }
}
