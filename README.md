# Plugin.Maui.Popup
Plugin.Maui.Popup - is a cross-platform plugin for .NET MAUI , Used for displaying content on Popup.


<h2> How To Use </h2> 
Available on NuGet: https://www.nuget.org/packages/Plugin.Maui.Popup/1.0.3  

Install this Plugin in your .NET MAUI Project

Now add following namespace to your content page & replace content page tag with <mauiPopup:BasePopupPage

```
xmlns:mauiPopup="clr-namespace:MauiPopup.Views;assembly=MauiPopup"
```

PopupPage.xaml
```
<?xml version="1.0" encoding="utf-8" ?>
<mauiPopup:BasePopupPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:mauiPopup="clr-namespace:MauiPopup.Views;assembly=MauiPopup"
             x:Class="MauiPopupDemo.PopupPage"
             Title="PopupPage">
    <VerticalStackLayout>
        <Label 
            Text="Welcome to .NET MAUI!"
            VerticalOptions="Center" 
            HorizontalOptions="Center" />
    </VerticalStackLayout>
</mauiPopup:BasePopupPage>
```

PopupPage.xaml.cs

```
using MauiPopup.Views;
 
namespace MauiPopupDemo;
 
public partial class PopupPage : BasePopupPage
{
	public PopupPage()
	{
		InitializeComponent();
	}
}
```
