using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Linq;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.BuiltInDialogsCategory.DialogsMethodsExample;

public partial class DialogsMethods : ContentView
{
	public DialogsMethods()
	{
		InitializeComponent();
        this.BindingContext = new ViewModel();

        this.scheduler.ScrollIntoView(new TimeOnly(8, 0));
	}

    // >> scheduler-dialogs-methods-eventhandlers
    private void AddAppointmentClicked(object sender, EventArgs e)
    {
        this.scheduler.CreateAppointmentWithDialog(new DateRange(DateTime.Now, DateTime.Now.AddHours(1)));
    }

    private void EditAppointmentClicked(object sender, EventArgs e)
    {
        var app = this.scheduler.AppointmentsSource?.FirstOrDefault();
        if (app != null)
        {
            if (app.RecurrenceRule != null)
            {
                this.scheduler.EditAppointmentWithDialog(Occurrence.CreateOccurrence(app, app.Start, app.End));
            }
            else
            {
                this.scheduler.EditAppointmentWithDialog(Occurrence.CreateMaster(app));
            }
        }
    }

    private void DeleteAppointmentClicked(object sender, EventArgs e)
    {
        var app = this.scheduler.AppointmentsSource?.FirstOrDefault();
        if (app != null)
        {
            if (app.RecurrenceRule != null)
            {
                this.scheduler.DeleteAppointmentWithDialog(Occurrence.CreateOccurrence(app, app.Start, app.End));
            }
            else
            {
                this.scheduler.DeleteAppointmentWithDialog(Occurrence.CreateMaster(app));
            }
        }
    }
    // << scheduler-dialogs-methods-eventhandlers
}