using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.BuiltInDialogsCategory;

public class ViewModel
{
    public ViewModel()
    {
        var date = DateTime.Today;
        this.Appointments = new ObservableCollection<Appointment>
        {
            new Appointment {
                Subject = "Meeting with Tom",
                Start = date.AddHours(9),
                End = date.AddHours(10)
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
            }, 
            new Appointment
            {
                Subject = ".NET MAUI Learning",
                Start = date.AddHours(10), 
                End = date.AddHours(12),
                RecurrenceRule = new RecurrenceRule(new RecurrencePattern{ Frequency= RecurrenceFrequency.Daily, MaxOccurrences=3})
            }
        };
    }

    public ObservableCollection<Appointment> Appointments { get; set; }
}