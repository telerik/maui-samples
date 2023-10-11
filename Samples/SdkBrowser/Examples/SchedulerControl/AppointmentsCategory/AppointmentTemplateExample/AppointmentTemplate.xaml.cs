using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.AppointmentsCategory.AppointmentTemplateExample;

public partial class AppointmentTemplate : ContentView
{
	public AppointmentTemplate()
	{
		InitializeComponent();

		this.BindingContext = new ViewModel();
	}
}