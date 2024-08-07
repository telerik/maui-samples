using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedButtonControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
    public GettingStartedCSharp()
    {
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
        grid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
        grid.HorizontalOptions = LayoutOptions.Center;
        // >> templatedbutton-gettingstarted-csharp
        var templatedButton = new RadTemplatedButton();
        templatedButton.Content = "My TemplatedButton Content";
        // << templatedbutton-gettingstarted-csharp
        grid.Children.Add(templatedButton);
        this.Content = grid;
    }
}