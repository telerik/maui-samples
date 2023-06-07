using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CalendarControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
    public GettingStartedCSharp()
    {
        var content = new VerticalStackLayout();

        // >> calendar-gettingstarted-csharp
        var calendar = new RadCalendar();
        // << calendar-gettingstarted-csharp

        content.Add(calendar);
        this.Content = content;
    }
}