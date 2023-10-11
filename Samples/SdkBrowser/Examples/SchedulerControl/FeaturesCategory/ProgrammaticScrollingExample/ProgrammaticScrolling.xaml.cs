using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.FeaturesCategory.ProgrammaticScrollingExample;

public partial class ProgrammaticScrolling : ContentView
{
    public ProgrammaticScrolling()
    {
        InitializeComponent();

        var date = DateTime.Today;

        this.scheduler.AppointmentsSource = new ObservableCollection<Appointment>
        {
                new Appointment {
                    Subject = "Meeting with Tom",
                    Start = date.AddHours(10),
                    End = date.AddHours(11)
                },
                new Appointment {
                    Subject = "Lunch with Sara",
                    Start = date.AddHours(12).AddMinutes(30),
                    End = date.AddHours(14)
                },
                new Appointment {
                    Subject = "Elle Birthday",
                    Start = date.AddDays(1),
                    End = date.AddDays(1).AddHours(12),
                    IsAllDay = true
                },
                new Appointment {
                    Subject = "Football Game",
                    Start = date.AddDays(2).AddHours(15),
                    End = date.AddDays(2).AddHours(17)
                }
        };
    }

    private void ScrollToTime(object sender, System.EventArgs e)
    {
        // >> scheduler-scrolltotime-code
        TimeOnly timeToScrollTo = new TimeOnly(10, 0, 0);
        this.scheduler.ScrollIntoView(timeToScrollTo);
        // << scheduler-scrolltotime-code
    }
}