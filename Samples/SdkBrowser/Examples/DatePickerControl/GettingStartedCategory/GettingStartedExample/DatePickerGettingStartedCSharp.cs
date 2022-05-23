using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DatePickerControl.GettingStartedCategory.GettingStartedExample
{
    public class DatePickerGettingStartedCSharp : RadContentView
    {
        public DatePickerGettingStartedCSharp()
        {
            // >> datepicker-getting-started-csharp
            var datePicker = new RadDatePicker();
            // << datepicker-getting-started-csharp
            var panel = new VerticalStackLayout();
            panel.Children.Add(datePicker);
            panel.WidthRequest = 300;
            panel.HorizontalOptions = LayoutOptions.Center;
            this.Content = panel;
        }
    }
}
