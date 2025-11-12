using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.AgendaViewCategory;

// >> agenda-view-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        this.Appointments = new ObservableCollection<Appointment>();
        this.GenerateAppointments();
    }

    public ObservableCollection<Appointment> Appointments { get; set; }

    private void GenerateAppointments()
    {
        var startDate = DateTime.Today;
        var random = new Random();
        
        var subjects = new[]
        {
            "Team Meeting",
            "Project Review",
            "Client Call",
            "Design Session",
            "Code Review",
            "Planning Meeting",
            "Standup",
            "Training Session",
            "Workshop",
            "Performance Review"
        };

        for (int i = 0; i < 40; i++)
        {
            var dayOffset = i / 4; // Spread across days (4 appointments per day)
            var hourOffset = 8 + (i % 4) * 3; // Start at 8 AM, add 3 hours for each appointment
            
            var start = startDate.AddDays(dayOffset).AddHours(hourOffset);
            var end = start.AddHours(1.5);

            this.Appointments.Add(new Appointment
            {
                Subject = $"{subjects[i % subjects.Length]} {i + 1}",
                Start = start,
                End = end,
                IsAllDay = false
            });
        }
    }
}
// << agenda-view-viewmodel
