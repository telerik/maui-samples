using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CalendarControl.EventsCategory.DisplayDateChangedExample;

public partial class DisplayDateChanged : ContentView
{
	public DisplayDateChanged()
	{
		InitializeComponent();
	}

	// >> calendar-displaydate-changed
    private void OnDisplayDateChanged(object sender, ValueChangedEventArgs<System.DateTime> e)
    {
		App.Current.MainPage.DisplayAlert("Date is ","" + e.NewValue, "OK");
    }
    // << calendar-displaydate-changed
}