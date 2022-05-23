using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TimeSpanPickerControl.GettingStartedCategory.GettingStartedExample
{
    public class TimeSpanPickerGettingStartedCSharp : RadContentView
    {
        public TimeSpanPickerGettingStartedCSharp()
        {
            // >> timespanpicker-getting-started-csharp
            var timeSpanPicker = new RadTimeSpanPicker();
            // << timespanpicker-getting-started-csharp
            var panel = new VerticalStackLayout();
            panel.Children.Add(timeSpanPicker);
            panel.WidthRequest = 300;
            panel.HorizontalOptions = LayoutOptions.Center;
            this.Content = panel;
        }
    }
}
