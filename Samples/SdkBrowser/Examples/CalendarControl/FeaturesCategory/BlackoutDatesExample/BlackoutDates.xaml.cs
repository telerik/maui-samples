using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.CalendarControl.FeaturesCategory.BlackoutDatesExample;

public partial class BlackoutDates : ContentView
{
	public BlackoutDates()
	{
		InitializeComponent();
        this.BindingContext = new ViewModel();
    }
}