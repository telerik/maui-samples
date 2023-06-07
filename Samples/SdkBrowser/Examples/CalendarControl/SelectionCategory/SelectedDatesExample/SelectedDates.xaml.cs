using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CalendarControl.SelectionCategory.SelectedDatesExample;

public partial class SelectedDates : ContentView
{
	public SelectedDates()
	{
		InitializeComponent();
		this.BindingContext = new ViewModel();
	}
}