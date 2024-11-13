using Microsoft.Maui.Controls;
using QSF.Examples.CalendarControl.Common;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.AppUtils.Services;

namespace QSF.Examples.CalendarControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private Random random;
    private DateTime selectedDate;
    private List<string> titles;
    private ObservableCollection<Reminder> filteredRemindersList;
    private bool isEmptyMessageVisible;

    public FirstLookViewModel()
    {
        this.random = DependencyService
            .Get<ITestingService>()
            .Random(10);
        this.titles = new List<string>()
        {
            "MAUI Dev Team Sync",
            ".NET MAUI Weekly Planning",
            "Unit Product Visibility",
            "Knowledge Sharing Session",
            "Reporting Product Team Sync",
            "UX Growth Series n",
            "DevTools Monthly Series",
            "MAUI Product Team Sync",
            "AllSpark Results Discussion",
            "Design Team Retrospective"
        };

        this.filteredRemindersList = new ObservableCollection<Reminder>();

        this.SelectedDate = DateTime.Today;
    }

    public DateTime SelectedDate
    {
        get => this.selectedDate;
        set
        {
            this.UpdateValue(ref this.selectedDate, value);
            this.GenerateReminders();
        }
    }

    public bool IsEmptyMessageVisible
    {
        get => this.isEmptyMessageVisible;
        set => this.UpdateValue(ref this.isEmptyMessageVisible, value);
    }

    public ObservableCollection<Reminder> FilteredRemindersList
    {
        get => this.filteredRemindersList;
        set => this.UpdateValue(ref this.filteredRemindersList, value);
    }

    public void GenerateReminders()
    {
        var dayOfWeek = this.selectedDate.DayOfWeek;
        if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
        {
            if (this.filteredRemindersList.Count > 0)
            {
                this.FilteredRemindersList = new ObservableCollection<Reminder>();
            }

            this.IsEmptyMessageVisible = true;

            return;
        }

        var count = this.random.Next(1, this.titles.Count - 1);

        var copyTitles = new List<string>(this.titles);
        var reminders = new List<Reminder>();
        for (int i = 0; i < count; i++)
        {
            var index = this.random.Next(0, copyTitles.Count - 1);
            var title = copyTitles[index];

            var reminder = new Reminder() { Date = this.selectedDate, Title = title };
            reminders.Add(reminder);

            copyTitles.RemoveAt(index);
        }

        this.FilteredRemindersList = new ObservableCollection<Reminder>(reminders);
        this.IsEmptyMessageVisible = false;
    }
}