using System;
using System.Collections.ObjectModel;
using System.Linq;
using QSF.Examples.CalendarControl.Common;
using QSF.ViewModels;

namespace QSF.Examples.CalendarControl.CustomizationExample;

public class CustomizationViewModel : ExampleViewModel
{
    private bool isEmptyMessageVisible;
    private DateTime selectedDate;
    private ObservableCollection<Reminder> remindersList;
    private ObservableCollection<Reminder> filteredRemindersList;

    public CustomizationViewModel()
    {
        this.filteredRemindersList = new ObservableCollection<Reminder>();
        this.remindersList = this.GenerateReminders();

        this.SelectedDate = DateTime.Today;
    }

    public bool IsEmptyMessageVisible
    {
        get => this.isEmptyMessageVisible;
        set => this.UpdateValue(ref this.isEmptyMessageVisible, value);
    }

    public DateTime SelectedDate
    {
        get => this.selectedDate;
        set
        {
            this.UpdateValue(ref this.selectedDate, value);
            this.Filter();
        }
    }

    public ObservableCollection<Reminder> RemindersList
    {
        get => this.remindersList;
        set => this.UpdateValue(ref this.remindersList, value);
    }

    public ObservableCollection<Reminder> FilteredRemindersList
    {
        get => this.filteredRemindersList;
        set => this.UpdateValue(ref this.filteredRemindersList, value);
    }

    public void Filter()
    {
        this.FilteredRemindersList = new ObservableCollection<Reminder>(this.remindersList.Where(r => r.Date == this.selectedDate).ToList());
        this.IsEmptyMessageVisible = this.filteredRemindersList.Count == 0;
    }

    internal static DateTime DateOfDay(DateTime date, int day)
        => new DateTime(date.Year, date.Month, day);

    private ObservableCollection<Reminder> GenerateReminders()
    {
        var reminders = new ObservableCollection<Reminder>();

        var today = DateTime.Today;
        var date = DateOfDay(today, 2);

        reminders.Add(new Reminder { Date = date, Title = "Rinse the vacuum's air filter" });
        reminders.Add(new Reminder { Date = date, Title = "Plan a surprise party for Jen" });
        reminders.Add(new Reminder { Date = date, Title = "Review budget" });

        date = DateOfDay(today, 5);
        reminders.Add(new Reminder { Date = date, Title = "Run the dishwasher's Machine Clean programme" });

        date = DateOfDay(today, 10);
        reminders.Add(new Reminder { Date = date, Title = "Schedule a date with John" });
        reminders.Add(new Reminder { Date = date, Title = "Update tax spreadsheet" });

        date = DateOfDay(today, 18);
        reminders.Add(new Reminder { Date = date, Title = "Book a hotel" });
        reminders.Add(new Reminder { Date = date, Title = "Book online tickets for the Eiffel Tower" });
        reminders.Add(new Reminder { Date = date, Title = "Book online tickets for the Louvre museum" });

        date = DateOfDay(today, 27);
        reminders.Add(new Reminder { Date = date, Title = "Organize iPhone photos" });
        reminders.Add(new Reminder { Date = date, Title = "Schedule cloud storage subscription payment" });

        reminders.Add(new Reminder { Date = DateOfDay(today, 3), Title = "Dave's birthday" });
        reminders.Add(new Reminder { Date = DateOfDay(today, 14), Title = "Rose's birthday" });
        reminders.Add(new Reminder { Date = DateOfDay(today, 23), Title = "John's birthday" });

        reminders.Add(new Reminder { Date = today, Title = "Read the latest Sands of Maui blog" });
        reminders.Add(new Reminder { Date = today, Title = "Go through emails" });

        return reminders;
    }
}