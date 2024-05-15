using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
	public GettingStartedCSharp()
	{
        var stack = new VerticalStackLayout();
        stack.HorizontalOptions = LayoutOptions.Center;
        // >> togglebutton-gettingstarted-csharp
        var toggleButton = new RadToggleButton();
        toggleButton.Content = "My ToggleButton Content";
        // << togglebutton-gettingstarted-csharp
        stack.Children.Add(toggleButton);
        this.Content = stack;
    }
}