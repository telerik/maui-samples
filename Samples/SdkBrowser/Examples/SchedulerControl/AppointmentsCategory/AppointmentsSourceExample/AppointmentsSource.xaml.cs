using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.AppointmentsCategory.AppointmentsSourceExample;

public partial class AppointmentsSource : ContentView
{
	public AppointmentsSource()
	{
		InitializeComponent();

        // >> scheduler-appointmentssource-setvm
        this.BindingContext = new ViewModel();
        // << scheduler-appointmentssource-setvm
    }
}