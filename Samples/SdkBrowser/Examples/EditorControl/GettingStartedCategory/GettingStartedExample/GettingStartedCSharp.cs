using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.EditorControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
    public GettingStartedCSharp()
    {
        // >> editor-getting-started-csharp
        var editor = new RadEditor();
        editor.HeightRequest = 120;
        editor.Placeholder = "Enter text here";
        // << editor-getting-started-csharp

        var grid = new Grid
        {
            VerticalOptions = LayoutOptions.Start
        };

        if (DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            grid.HorizontalOptions = LayoutOptions.Start;
            grid.WidthRequest = 300;
        }
        else
        {
            grid.HorizontalOptions = LayoutOptions.Fill;
        }

        grid.Children.Add(editor);
        this.Content = grid;
    }
}