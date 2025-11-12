using Microsoft.Maui.Controls;
using System.Linq;

namespace SDKBrowserMaui.Examples.SchedulerControl.AgendaViewCategory.ScrollIntoViewExample;

public partial class ScrollIntoView : ContentView
{
	ViewModel vm;
	public ScrollIntoView()
	{
		InitializeComponent();

		this.vm = new ViewModel();
		this.BindingContext = this.vm;
	}
	// >> scheduler-agenda-scrolltolastappointment
	private void OnScrollToLastAppointmentClicked(object sender, System.EventArgs e)
	{
		if (this.vm != null && this.vm.Appointments.Count > 0)
		{
			var lastAppointment = this.vm.Appointments.Last();
			this.scheduler.ScrollIntoView(lastAppointment);
		}
	}
	// << scheduler-agenda-scrolltolastappointment

	// >> scheduler-agenda-scrolltofirstappointment
	private void OnScrollToFirstAppointmentClicked(object sender, System.EventArgs e)
	{
		if (this.vm != null && this.vm.Appointments.Count > 0)
		{
			var firstAppointment = this.vm.Appointments.First();
			this.scheduler.ScrollIntoView(firstAppointment);
		}
	}
	// << scheduler-agenda-scrolltofirstappointment
}