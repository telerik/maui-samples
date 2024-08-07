using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ToggleButtonControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
    public GettingStartedCSharp()
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        grid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        grid.HorizontalOptions = LayoutOptions.Center;
        // >> togglebutton-gettingstarted-csharp
        var toggleButton = new RadToggleButton();
        toggleButton.Content = "My ToggleButton Content";
        // << togglebutton-gettingstarted-csharp
        grid.Children.Add(toggleButton);
        this.Content = grid;
    }
}