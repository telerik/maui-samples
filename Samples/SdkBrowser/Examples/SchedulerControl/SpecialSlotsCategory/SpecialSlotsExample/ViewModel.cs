using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.SpecialSlotsCategory.SpecialSlotsExample;

// >> scheduler-specialslots-viewmodel
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

        this.RestHours.Add(new Slot(today.AddHours(12), today.AddHours(13))
        {
            RecurrencePattern = dailyRecurrence,
            IsReadOnly = true
        });

        this.RestHours.Add(new Slot(today.AddHours(16), today.AddHours(16).AddMinutes(15))
        {
            RecurrencePattern = dailyRecurrence
        });
    }

    public ObservableCollection<Slot> RestHours { get; set; }
}
// << scheduler-specialslots-viewmodel