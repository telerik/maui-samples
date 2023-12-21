using Microsoft.Maui.Devices;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls.Scheduler;

namespace QSF.Examples.SchedulerControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private double zoomLevel = 1400;
    private TimeSpan minorTickLength;
    private TimeSpan majorTickLength;
    private TimeSpan dayStartTimeSpan;
    private TimeOnly dayStartTime;
    private TimeSpan dayEndTimeSpan;
    private TimeOnly dayEndTime;
    private SchedulerInteractionMode interactionMode;
    private DayOfWeek firstDayOfWeek;
    private int visibleDays = 3;
    private bool isTodayButtonVisible = true;
    private bool iCurrentTimeIndicatorVisible = true;
    private bool isWeekendVisible = true;
    private object activeViewDefinition;

    public ConfigurationViewModel()
    {
        this.MinorTicksSource = new List<TimeSpan>() 
        { 
            TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(30),
            TimeSpan.FromHours(1) 
        };

        this.MajorTicksSource = new List<TimeSpan>()
        {
            TimeSpan.FromHours(1),
            TimeSpan.FromHours(2),
            TimeSpan.FromHours(3),
        };

        this.MinorTickLength = this.MinorTicksSource[1];
        this.MajorTickLength = this.MajorTicksSource[0];

        this.InteractionModes = Enum.GetValues(typeof(SchedulerInteractionMode)).OfType<SchedulerInteractionMode>().ToList();
        this.DaysOfWeek = (IEnumerable<DayOfWeek>)Enum.GetValues(typeof(DayOfWeek));

        this.DayStartTimeSpan = TimeOnly.MinValue.ToTimeSpan();
        this.DayEndTimeSpan = TimeOnly.MaxValue.ToTimeSpan();

        if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.Android)
        {
            this.InteractionMode = SchedulerInteractionMode.Pan;
        }
        else
        {
            this.InteractionMode = SchedulerInteractionMode.None;
        }

        this.Appointments = this.CreateAppointments();
    }

    public List<TimeSpan> MinorTicksSource { get; private set; }

    public List<TimeSpan> MajorTicksSource { get; private set; }

    public List<SchedulerInteractionMode> InteractionModes { get; private set;}

    public IEnumerable<DayOfWeek> DaysOfWeek { get; }

    public ObservableCollection<Appointment> Appointments { get; }

    public double ZoomLevel
    {
        get => this.zoomLevel;
        set => this.UpdateValue(ref this.zoomLevel, value);
    }

    public TimeSpan MinorTickLength
    {
        get => this.minorTickLength;
        set => this.UpdateValue(ref this.minorTickLength, value);
    }

    public TimeSpan MajorTickLength
    {
        get => this.majorTickLength;
        set => this.UpdateValue(ref this.majorTickLength, value);
    }

    public TimeSpan DayStartTimeSpan
    {
        get => this.dayStartTimeSpan;
        set
        {
            if (this.UpdateValue(ref this.dayStartTimeSpan, value))
            {
                this.DayStartTime = TimeOnly.FromTimeSpan(this.dayStartTimeSpan);
            }
        }
    }

    public TimeOnly DayStartTime
    {
        get => this.dayStartTime;
        set => this.UpdateValue(ref this.dayStartTime, value);
    }

    public TimeSpan DayEndTimeSpan
    {
        get => this.dayEndTimeSpan;
        set
        {
            if (this.UpdateValue(ref this.dayEndTimeSpan, value))
            {
                this.DayEndTime = TimeOnly.FromTimeSpan(this.dayEndTimeSpan);
            }
        }
    }

    public TimeOnly DayEndTime
    {
        get => this.dayEndTime;
        set => this.UpdateValue(ref this.dayEndTime, value);
    }

    public SchedulerInteractionMode InteractionMode
    {
        get => this.interactionMode;
        set => this.UpdateValue(ref this.interactionMode, value);
    }

    public DayOfWeek FirstDayOfWeek
    {
        get => this.firstDayOfWeek;
        set => this.UpdateValue(ref this.firstDayOfWeek, value);
    }

    public int VisibleDays
    {
        get => this.visibleDays;
        set => this.UpdateValue(ref this.visibleDays, value);
    }

    public bool IsTodayButtonVisible
    {
        get => this.isTodayButtonVisible;
        set => this.UpdateValue(ref this.isTodayButtonVisible, value);
    }

    public bool IsCurrentTimeIndicatorVisible
    {
        get => this.iCurrentTimeIndicatorVisible;
        set => this.UpdateValue(ref this.iCurrentTimeIndicatorVisible, value);
    }

    public bool IsWeekendVisible
    {
        get => this.isWeekendVisible;
        set => this.UpdateValue(ref this.isWeekendVisible, value);
    }

    public object ActiveViewDefinition
    {
        get => this.activeViewDefinition;
        set
        {
            if (value == null)
            {
                return;
            }

            this.UpdateValue(ref this.activeViewDefinition, value);
        }
    }

    private ObservableCollection<Appointment> CreateAppointments()
    {
        var recurrencePattern = new RecurrencePattern(null, RecurrenceDays.WeekDays, RecurrenceFrequency.Daily, 1, null, null) { MaxOccurrences = 15 };
        var dailyRecurrenceRule = new RecurrenceRule(recurrencePattern);
        var today = DateTime.Today;

        return new ObservableCollection<Appointment>
        {
            new Appointment()
            {
                Start = today.AddHours(8).AddMinutes(30),
                End = today.AddHours(9).AddMinutes(30),
                Subject = "Fitness with Peter"
            },
            new Appointment()
            {
                Start = today.AddHours(11),
                End = today.AddHours(11).AddMinutes(30),
                Subject = "Daily SCRUM",
                RecurrenceRule = dailyRecurrenceRule
            },
            new Appointment()
            {
                Start = today.AddHours(11),
                End = today.AddHours(12),
                Subject = "Job Interview"
            },
            new Appointment()
            {
                Start = today.AddHours(12).AddMinutes(30),
                End = today.AddHours(14),
                Subject = "Lunch with Sam"
            },
            new Appointment()
            {
                Start = today.AddHours(14).AddMinutes(30),
                End = today.AddHours(15).AddMinutes(30),
                Subject = "Webinar"
            },
            new Appointment()
            {
                Start = today.AddHours(16),
                End = today.AddHours(17).AddMinutes(30),
                Subject = "Tokio Deal call"
            },
            new Appointment()
            {
                Start = today.AddHours(16).AddMinutes(45),
                End = today.AddHours(17).AddMinutes(45),
                Subject = "Dentist"
            },
            new Appointment()
            {
                Start = today.AddHours(18),
                End = today.AddHours(19).AddMinutes(30),
                Subject = "Home theater evening"
            },
            new Appointment()
            {
                Start = today.AddHours(20),
                End = today.AddHours(22),
                Subject = "Watch a movie"
            },
            new Appointment()
            {
                Start = today,
                End = today.AddHours(5),
                Subject = "Alex's Birthday",
                IsAllDay = true
            },
            new Appointment()
            {
                Start = today.AddDays(-1).AddHours(18),
                End = today.AddDays(-1).AddHours(19),
                Subject = "Swimming"
            },
            new Appointment()
            {
                Start = today.AddDays(-1).AddHours(13),
                End = today.AddDays(-1).AddHours(14).AddMinutes(30),
                Subject = "Lunch with the Morgans"
            },
            new Appointment()
            {
                Start = today.AddDays(-1).AddHours(20),
                End = today.AddDays(-1).AddHours(22).AddMinutes(30),
                Subject = "Theater evening"
            },
            new Appointment()
            {
                Start = today.AddDays(-1).AddHours(15),
                End = today.AddDays(-1).AddHours(16),
                Subject = "Conference call with HQ2"
            },
        };
    }
}

  