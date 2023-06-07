using Microsoft.Maui.Controls;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace SDKBrowserMaui.Examples.CalendarControl.EventsCategory.SelectionChangedExample;

public partial class SelectionChanged : ContentView
{
	public SelectionChanged()
	{
		InitializeComponent();
	}

    // >> calendar-selection-changed-event
    private void OnSelectionChanged(object sender, Telerik.Maui.Controls.Calendar.CalendarSelectionChangedEventArgs e)
    {
        App.Current.MainPage.DisplayAlert("You have selected: ", "" + e.AddedDates.FirstOrDefault(), "OK");
    }
    // << calendar-selection-changed-event
}