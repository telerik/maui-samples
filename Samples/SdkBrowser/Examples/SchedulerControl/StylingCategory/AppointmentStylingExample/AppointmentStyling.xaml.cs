using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.AppointmentStylingExample;

public partial class AppointmentStyling : ContentView
{
	public AppointmentStyling()
	{
		InitializeComponent();

        var date = DateTime.Today;
        this.scheduler.AppointmentsSource = new ObservableCollection<Appointment>
        {
            new Appointment {
                Subject = "Meeting with Tom",
                Start = date.AddDays(-1).AddHours(10),
                End = date.AddDays(-1).AddHours(11)
            },
            new Appointment {
                Subject = "Lunch with Sara",
                Start = date.AddHours(12).AddMinutes(30),
                End = date.AddHours(14)
            },
            new Appointment {
                Subject = "Elle Birthday",
                Start = date,
                End = date.AddHours(11),
                IsAllDay = true
            },
            new Appointment {
                Subject = "Football Game",
                Start = date.AddDays(1).AddHours(15),
                End = date.AddDays(1).AddHours(17)
            }
        };
    }
}