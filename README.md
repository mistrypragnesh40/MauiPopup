# Plugin.Maui.Popup

High-performance, platform-native popups for .NET MAUI. Uses native overlays (Android Dialog, iOS/Mac Catalyst UIView, Windows WinUI Popup) instead of modal navigation for faster popup display.

![ezgif com-video-to-gif (1)](https://user-images.githubusercontent.com/47309472/222663761-bf9a54d6-d395-4a25-b69c-24c22867bb1c.gif)

## Installation

Available on NuGet: https://www.nuget.org/packages/Plugin.Maui.Popup

```
dotnet add package Plugin.Maui.Popup
```

## Supported Platforms

| Platform | Min Version |
|---|---|
| Android | API 21+ |
| iOS | 14.0+ |
| Mac Catalyst | 14.0+ |
| Windows | 10.0.19041+ |

## Quick Start

### 1. Create a Popup Page

Add the MAUI Popup namespace and replace the `ContentPage` tag with `BasePopupPage`:

**MyPopup.xaml**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<mauiPopup:BasePopupPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:mauiPopup="clr-namespace:MauiPopup.Views;assembly=MauiPopup"
             x:Class="MyApp.MyPopup">
    <VerticalStackLayout Padding="20" Spacing="15" WidthRequest="300">
        <Label Text="Hello from Popup!"
               FontSize="20" FontAttributes="Bold"
               HorizontalOptions="Center" />
        <Button Text="Close" Clicked="OnCloseClicked" />
    </VerticalStackLayout>
</mauiPopup:BasePopupPage>
```

**MyPopup.xaml.cs**
```csharp
using MauiPopup;
using MauiPopup.Views;

namespace MyApp;

public partial class MyPopup : BasePopupPage
{
    public MyPopup()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await PopupAction.ClosePopup();
    }
}
```

### 2. Show the Popup

```csharp
using MauiPopup;

// Simple popup (no return value)
await PopupAction.DisplayPopup(new MyPopup());
```

### 3. Close the Popup

```csharp
// Close with no value
await PopupAction.ClosePopup();

// Close with a return value
await PopupAction.ClosePopup("some result");
```

## Return Values

You can get values back from a popup when it closes.

**Return a string:**
```csharp
string result = await PopupAction.DisplayPopup(new MyPopup());
```

**Return a typed object:**
```csharp
var user = await PopupAction.DisplayPopup<UserInfo>(new MyPopup());
```

**Close with value (from inside the popup):**
```csharp
await PopupAction.ClosePopup(new UserInfo { Name = "John", Age = 30 });
```

## Close All Popups

Close every open popup at once — useful for navigation changes or logout scenarios.

```csharp
await PopupAction.CloseAllPopups();
```

## Check If Popup Is Open

```csharp
bool isOpen = PopupAction.IsPopupOpen;
```

## Properties

All properties are `BindableProperty` and can be set in XAML or code-behind.

### Color Properties

| Property | Type | Default | Description |
|---|---|---|---|
| `ForegroundColor` | `Color` | `White` (light) / `#ff282828` (dark) | Background color of the popup content area |
| `OverlayColor` | `Color` | `#80000000` (50% black) | Color of the semi-transparent overlay behind the popup |

```csharp
var popup = new MyPopup();
popup.ForegroundColor = Colors.LightBlue;
popup.OverlayColor = Color.FromArgb("#CC000000"); // 80% black overlay
```

### Layout Properties

| Property | Type | Default | Description |
|---|---|---|---|
| `VerticalOptions` | `LayoutOptions` | `Center` | Vertical position (`Start`, `Center`, `End`, `Fill`) |
| `HorizontalOptions` | `LayoutOptions` | `Center` | Horizontal position (`Start`, `Center`, `End`, `Fill`) |
| `Margin` | `Thickness` | `0` | Outer margin around the popup |
| `ContentPadding` | `Thickness` | `0` | Inner padding between popup border and content |
| `CornerRadius` | `CornerRadius` | `0` | Rounded corners for the popup |

**Position examples:**
```csharp
// Center (default)
popup.VerticalOptions = LayoutOptions.Center;
popup.HorizontalOptions = LayoutOptions.Center;

// Bottom sheet
popup.VerticalOptions = LayoutOptions.End;
popup.HorizontalOptions = LayoutOptions.Fill;

// Top notification banner
popup.VerticalOptions = LayoutOptions.Start;
popup.HorizontalOptions = LayoutOptions.Fill;
popup.Margin = new Thickness(10, 50, 10, 0);
```

**CornerRadius:**
```csharp
// Uniform radius
popup.CornerRadius = new CornerRadius(20);

// Top corners only (great for bottom sheets)
popup.CornerRadius = new CornerRadius(20, 20, 0, 0);

// Per-corner: TopLeft, TopRight, BottomRight, BottomLeft
popup.CornerRadius = new CornerRadius(10, 20, 10, 20);
```

**ContentPadding:**
```csharp
// Uniform padding
popup.ContentPadding = new Thickness(16);

// Horizontal and vertical
popup.ContentPadding = new Thickness(20, 30);

// Per-side: Left, Top, Right, Bottom
popup.ContentPadding = new Thickness(10, 30, 10, 30);
```

### Animation Properties

| Property | Type | Default | Description |
|---|---|---|---|
| `Animation` | `PopupAnimation` | `Fade` | Animation type for show/dismiss |
| `AnimationDuration` | `uint` | `200` | Animation duration in milliseconds |

**Available animations:**

| Value | Description |
|---|---|
| `PopupAnimation.None` | No animation, instant appear/disappear |
| `PopupAnimation.Fade` | Smooth fade in/out (default) |
| `PopupAnimation.Scale` | Scale up from center on appear, scale down on dismiss |
| `PopupAnimation.SlideUp` | Slide up from bottom on appear, slide down on dismiss |
| `PopupAnimation.SlideDown` | Slide down from top on appear, slide up on dismiss |

```csharp
popup.Animation = PopupAnimation.SlideUp;
popup.AnimationDuration = 300;
```

### Behavior Properties

| Property | Type | Default | Description |
|---|---|---|---|
| `IsCloseOnBackgroundClick` | `bool` | `true` | Close popup when tapping the overlay background |
| `AutoCloseAfter` | `TimeSpan?` | `null` | Auto-close popup after the specified duration |

**Auto-close:**
```csharp
// Auto-close after 3 seconds
popup.AutoCloseAfter = TimeSpan.FromSeconds(3);

// Timer is automatically cancelled if the user closes manually
```

**Prevent background dismiss (for required input):**
```csharp
popup.IsCloseOnBackgroundClick = false;
```

## Events

### Dismissed Event

Fires when the popup is closed, providing the return value.

```csharp
var popup = new MyPopup();
popup.Dismissed += (sender, args) =>
{
    var result = args.Result;
    Console.WriteLine($"Popup dismissed with: {result}");
};
await PopupAction.DisplayPopup(popup);
```

You can also override the `OnDismissed` method in your popup subclass:

```csharp
public partial class MyPopup : BasePopupPage
{
    protected override void OnDismissed(PopupDismissedEventArgs e)
    {
        base.OnDismissed(e);
        // Custom dismiss logic here
    }
}
```

## Dark Mode Support

`BasePopupPage` automatically adapts to the system theme:
- **Light mode** — white popup background
- **Dark mode** — dark (`#282828`) popup background

You can override these by setting `ForegroundColor` and `OverlayColor` explicitly.

## Common Patterns

### Bottom Sheet
```csharp
var popup = new MyPopup
{
    VerticalOptions = LayoutOptions.End,
    HorizontalOptions = LayoutOptions.Fill,
    Margin = new Thickness(0),
    CornerRadius = new CornerRadius(20, 20, 0, 0),
    ContentPadding = new Thickness(20),
    Animation = PopupAnimation.SlideUp
};
```

### Toast / Notification Banner
```csharp
var popup = new MyPopup
{
    VerticalOptions = LayoutOptions.Start,
    HorizontalOptions = LayoutOptions.Fill,
    Margin = new Thickness(10, 50, 10, 0),
    CornerRadius = new CornerRadius(12),
    ContentPadding = new Thickness(16),
    Animation = PopupAnimation.SlideDown,
    AutoCloseAfter = TimeSpan.FromSeconds(3),
    ForegroundColor = Color.FromArgb("#4CAF50")
};
```

### Confirm Dialog
```csharp
var popup = new ConfirmPopup
{
    IsCloseOnBackgroundClick = false,
    CornerRadius = new CornerRadius(16),
    ContentPadding = new Thickness(20),
    Animation = PopupAnimation.Scale
};
bool confirmed = await PopupAction.DisplayPopup<bool>(popup);
```

### Full Page Popup
```csharp
var popup = new MyPopup
{
    VerticalOptions = LayoutOptions.Fill,
    HorizontalOptions = LayoutOptions.Fill,
    Margin = new Thickness(0),
    IsCloseOnBackgroundClick = false,
    Animation = PopupAnimation.Scale
};
```

## API Reference

### PopupAction (Static Methods)

| Method | Description |
|---|---|
| `DisplayPopup(BasePopupPage page)` | Show popup and return `string` result |
| `DisplayPopup<T>(BasePopupPage page)` | Show popup and return typed `T` result |
| `ClosePopup(object returnValue = null)` | Close the topmost popup with optional return value |
| `CloseAllPopups()` | Close all open popups at once |
| `IsPopupOpen` | `bool` — returns `true` if any popup is currently open |

### BasePopupPage (Properties)

| Property | Type | Default |
|---|---|---|
| `ForegroundColor` | `Color` | Theme-aware (white/dark) |
| `OverlayColor` | `Color` | `#80000000` |
| `VerticalOptions` | `LayoutOptions` | `Center` |
| `HorizontalOptions` | `LayoutOptions` | `Center` |
| `Margin` | `Thickness` | `0` |
| `ContentPadding` | `Thickness` | `0` |
| `CornerRadius` | `CornerRadius` | `0` |
| `Animation` | `PopupAnimation` | `Fade` |
| `AnimationDuration` | `uint` | `200` |
| `IsCloseOnBackgroundClick` | `bool` | `true` |
| `AutoCloseAfter` | `TimeSpan?` | `null` |

### BasePopupPage (Events)

| Event | Type | Description |
|---|---|---|
| `Dismissed` | `EventHandler<PopupDismissedEventArgs>` | Fires when popup is closed |

### PopupDismissedEventArgs

| Property | Type | Description |
|---|---|---|
| `Result` | `object` | The return value passed to `ClosePopup()` |

