using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.SpecialSlotsCategory.SpecialSlotTemplateExample;

// >> scheduler-customslots-vm
public class ViewModel
{
    public ViewModel()
    {
        this.RestHours = new ObservableCollection<Slot>();

        var today = DateTime.Today;
        var dailyRecurrence = new RecurrencePattern()
        {
            DaysOfWeekMask = RecurrenceDays.WeekDays,
            Frequency = RecurrenceFrequency.Weekly,
            MaxOccurrences = 30
        };

        this.RestHours.Add(new Slot()
        {
            Start = today.AddHours(12).AddMinutes(30),
            End = today.AddHours(13),
            IsReadOnly = true,
            RecurrencePattern = dailyRecurrence
        });

        this.RestHours.Add(new Slot()
        {
            Start = today.AddHours(16),
            End = today.AddHours(16).AddMinutes(30),
            RecurrencePattern = dailyRecurrence
        });
    }

    public ObservableCollection<Slot> RestHours { get; set; }
}
// << scheduler-customslots-vm