using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CalendarControl.GettingStartedCategory.GettingStartedExample;

public class GettingStartedCSharp : ContentView
{
    public GettingStartedCSharp()
    {
        var content = new Grid();

        // >> calendar-gettingstarted-csharp
        var calendar = new RadCalendar();
        // << calendar-gettingstarted-csharp

        if (DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            calendar.HorizontalOptions = LayoutOptions.Start;
            calendar.VerticalOptions = LayoutOptions.Start;
        }

        content.Add(calendar);
        this.Content = content;
    }
}