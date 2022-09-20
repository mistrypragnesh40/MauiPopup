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
<h2> How To Display Popup Page </h2> 
Use following code to display Popup Page

```
MauiPopup.PopupAction.DisplayPopup(new PopupPage());
```

<h2> How To Close Popup Page </h2> 
Use following code to close Popup Page

```
MauiPopup.PopupAction.ClosePopup();
```

<h2> Popup Page Formation Property </h2>  

1. HorizontalOptions : To Adjust Layout Horizontally
2. VerticalOptions : To Adjust Layout Vertically
3. IsCloseOnBackgroundClick : Set it to true to close popup on background Click. (By Default True)
4. Margin : Set Margin of Layout.
5. ForegroundColor : Used To Set Background Color Of Popup Content.

