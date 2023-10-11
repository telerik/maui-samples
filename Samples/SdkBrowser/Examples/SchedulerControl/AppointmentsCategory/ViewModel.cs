using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.AppointmentsCategory;

// >> scheduler-appointments-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        var date = DateTime.Today;
        this.Appointments = new ObservableCollection<Appointment>
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
                Start = date,
                End = date.AddHours(11),
                IsAllDay = true
            },
            new Appointment {
                Subject = "Football Game",
                Start = date.AddDays(2).AddHours(15),
                End = date.AddDays(2).AddHours(17)
            }
        };
    }

    public ObservableCollection<Appointment> Appointments { get; set; }
}
// << scheduler-appointments-viewmodel