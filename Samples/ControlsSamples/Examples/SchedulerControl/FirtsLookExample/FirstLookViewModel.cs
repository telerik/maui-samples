using System;
using System.Collections.ObjectModel;
using QSF.ViewModels;
using Telerik.Maui.Controls.Scheduler;

namespace QSF.Examples.SchedulerControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private ObservableCollection<Appointment> appointmentsSource;

    public FirstLookViewModel()
    {
        this.AppointmentsSource = CreateAppointments();
    }

    public ObservableCollection<Appointment> AppointmentsSource
    {
        get => this.appointmentsSource;
        set => this.UpdateValue(ref this.appointmentsSource, value);
    }

    private static ObservableCollection<Appointment> CreateAppointments()
    {
        var now = DateTime.Now.Date;
        var appointments = new ObservableCollection<Appointment>();

        var dailySyncPattern = new RecurrencePattern()
        {
            Frequency = RecurrenceFrequency.Daily,
            MaxOccurrences = 90,
            DaysOfWeekMask = RecurrenceDays.WeekDays
        };
        var dailySyncRule = new RecurrenceRule(dailySyncPattern);
        appointments.Add(new Appointment { Start = now.AddMonths(-1).AddHours(10), End = now.AddMonths(-1).AddHours(10).AddMinutes(30), Subject = "Maui Team Sync", RecurrenceRule = dailySyncRule });

        var officeDaysPattern = new RecurrencePattern()
        {
            Frequency = RecurrenceFrequency.Weekly,
            MaxOccurrences = 36,
            DaysOfWeekMask = RecurrenceDays.Tuesday | RecurrenceDays.Wednesday
        };
        var officeDaysRule = new RecurrenceRule(officeDaysPattern);
        appointments.Add(new Appointment { Start = now.AddMonths(-1), End = now.AddMonths(-1).AddMilliseconds(1), IsAllDay = true, Subject = "[Reminder] Weekly Team Day at the Office", RecurrenceRule = officeDaysRule });

        appointments.Add(new Appointment { Start = now.AddDays(-1).AddHours(14), End = now.AddDays(-1).AddHours(15), Subject = "Overview Page Redesign Discussion" });
        appointments.Add(new Appointment { Start = now.AddDays(-1).AddHours(15).AddMinutes(30), End = now.AddDays(-1).AddHours(16).AddMinutes(15), Subject = "Improving controls samples - UX & UI discussion" });
        appointments.Add(new Appointment { Start = now.AddHours(11), End = now.AddHours(12).AddMinutes(30), Subject = "Onboarding New Internship Applicants" });
        appointments.Add(new Appointment { Start = now.AddHours(12), End = now.AddHours(13), Subject = "Team Lunch" });
        appointments.Add(new Appointment { Start = now.AddHours(15), End = now.AddHours(16), Subject = "Content Writing Guidelines Discussion" });
        appointments.Add(new Appointment { Start = now.AddHours(17), End = now.AddHours(18), Subject = "Managing Personal Finances - Part I" });
        appointments.Add(new Appointment { Start = now.AddHours(17), End = now.AddHours(17).AddMinutes(30), Subject = "Marketing Monthly Sync" });
        appointments.Add(new Appointment { Start = now.AddDays(1).AddHours(17), End = now.AddDays(1).AddHours(18), Subject = "Managing Personal Finances - Part II" });

        return appointments;
    }
}