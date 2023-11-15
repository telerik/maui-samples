using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.MoreIndicatorStylingExample;

public partial class MoreIndicatorStyling : ContentView
{
    public MoreIndicatorStyling()
    {
        InitializeComponent();

        var date = DateTime.Today;
        this.scheduler.AppointmentsSource = new ObservableCollection<Appointment>
        {
            new Appointment
            {
                Subject = "Daily Meeting",
                Start = date.AddHours(9),
                End = date.AddHours(9).AddMinutes(30)
            },
            new Appointment {
                Subject = "Meeting with Tom",
                Start = date.AddHours(10),
                End = date.AddHours(11)
            },
            new Appointment
            {
                Subject = "Planning",
                Start = date.AddHours(11),
                End = date.AddHours(12)
            },
            new Appointment {
                Subject = "Lunch with Sara",
                Start = date.AddHours(12).AddMinutes(30),
                End = date.AddHours(14)
            },
            new Appointment {
                Subject = "Elle Birthday",
                Start = date.AddHours(14),
                End = date.AddHours(15)
            },
            new Appointment {
                Subject = "Learning Session",
                Start = date.AddHours(15),
                End = date.AddHours(16)
            },
            new Appointment {
                Subject = "Coffee Break",
                Start = date.AddHours(16),
                End = date.AddHours(16).AddMinutes(30)
            },
            new Appointment {
                Subject = "Football Game",
                Start = date.AddHours(18),
                End = date.AddHours(20)
            }
        };

        this.scheduler.MonthDayTapped += this.OnMonthDayTapped;
    }

    private void OnMonthDayTapped(object sender, TappedEventArgs<DateTime> e)
    {
        this.scheduler.CurrentDate = e.Data;
        this.scheduler.ActiveViewDefinition = this.dayViewDefinition;
    }
}