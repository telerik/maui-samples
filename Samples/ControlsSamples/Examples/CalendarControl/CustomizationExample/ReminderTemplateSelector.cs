using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace QSF.Examples.CalendarControl.CustomizationExample;

public class ReminderTemplateSelector : DataTemplateSelector
{
    private DateTime today;
    private List<DateTime> tasks;
    private List<DateTime> birthdays;

    public ReminderTemplateSelector()
    {
        this.today = DateTime.Today;

        this.tasks = new List<DateTime>();
        this.tasks.Add(CustomizationViewModel.DateOfDay(today, 2));
        this.tasks.Add(CustomizationViewModel.DateOfDay(today, 5));
        this.tasks.Add(CustomizationViewModel.DateOfDay(today, 10));
        this.tasks.Add(CustomizationViewModel.DateOfDay(today, 18));
        this.tasks.Add(CustomizationViewModel.DateOfDay(today, 27));

        this.birthdays = new List<DateTime>();
        this.birthdays.Add(CustomizationViewModel.DateOfDay(today, 3));
        this.birthdays.Add(CustomizationViewModel.DateOfDay(today, 14));
        this.birthdays.Add(CustomizationViewModel.DateOfDay(today, 23));
    }

    public DataTemplate TaskTemplate { get; set; }
    public DataTemplate BirthdayTemplate { get; set; }
    public DataTemplate NonWorkingDayTemplate { get; set; }
    public DataTemplate NonWorkingDayWithReminderTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var node = item as CalendarNode;
        var date = node.Date.Value;

        if (this.today.Month != date.Month || this.today.Year != date.Year)
        {
            return null;
        }

        bool hasTaskReminder = this.tasks.Contains(date);
        bool hasBirthdayReminder = this.birthdays.Contains(date);
        bool hasReminder = hasTaskReminder || hasBirthdayReminder;

        var dayOfWeek = date.DayOfWeek;
        if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
        {
            return hasReminder ? this.NonWorkingDayWithReminderTemplate : this.NonWorkingDayTemplate;
        }

        if (hasTaskReminder)
        {
            return this.TaskTemplate;
        }
        
        if (hasBirthdayReminder)
        {
            return this.BirthdayTemplate;
        }

        return null;
    }
}