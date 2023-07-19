using System;
using System.Collections.Generic;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using Telerik.Maui;
using Telerik.Maui.Controls.Calendar;

namespace QSF.Examples.CalendarControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private DateTime minDate;
    private DateTime maxDate;
    private DayOfWeek firstDayOfWeek;
    private bool areDayNamesVisible;
    private bool isOutOfScopeVisible;
    private CalendarDisplayMode displayMode;
    private CalendarSelectionMode selectionMode;
    private CalendarInteractionMode interactionMode;
    private Orientation navigationDirection;

    public ConfigurationViewModel()
    {
        this.DaysOfWeek = (IEnumerable<DayOfWeek>)Enum.GetValues(typeof(DayOfWeek));
        this.DisplayModes = (IEnumerable<CalendarDisplayMode>)Enum.GetValues(typeof(CalendarDisplayMode));
        this.SelectionModes = (IEnumerable<CalendarSelectionMode>)Enum.GetValues(typeof(CalendarSelectionMode));
        this.InteractionModes = (IEnumerable<CalendarInteractionMode>)Enum.GetValues(typeof(CalendarInteractionMode));
        this.NavigationDirections = (IEnumerable<Orientation>)Enum.GetValues(typeof(Orientation));

        if (DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            this.InteractionMode = CalendarInteractionMode.None;
        }
        else
        {
            this.InteractionMode = CalendarInteractionMode.Pan;
        }

        this.MinDate = new DateTime(1900, 1, 1);
        this.MaxDate = new DateTime(2099, 12, 31);
        this.AreDayNamesVisible = true;
        this.IsOutOfScopeVisible = true;
        this.DisplayMode = CalendarDisplayMode.Month;
        this.SelectionMode = CalendarSelectionMode.Single;
    }

    public IEnumerable<DayOfWeek> DaysOfWeek { get; }

    public IEnumerable<CalendarDisplayMode> DisplayModes { get; }

    public IEnumerable<CalendarSelectionMode> SelectionModes { get; }

    public IEnumerable<CalendarInteractionMode> InteractionModes { get; }

    public IEnumerable<Orientation> NavigationDirections { get; }

    public DateTime MinDate
    {
        get { return this.minDate; }
        set { this.UpdateValue(ref this.minDate, value); }
    }

    public DateTime MaxDate
    {
        get { return this.maxDate; }
        set { this.UpdateValue(ref this.maxDate, value); }
    }

    public DayOfWeek FirstDayOfWeek
    {
        get { return this.firstDayOfWeek; }
        set { this.UpdateValue(ref this.firstDayOfWeek, value); }
    }

    public bool AreDayNamesVisible
    {
        get { return this.areDayNamesVisible; }
        set { this.UpdateValue(ref this.areDayNamesVisible, value); }
    }

    public bool IsOutOfScopeVisible
    {
        get { return this.isOutOfScopeVisible; }
        set { this.UpdateValue(ref this.isOutOfScopeVisible, value); }
    }

    public CalendarDisplayMode DisplayMode
    {
        get { return this.displayMode; }
        set { this.UpdateValue(ref this.displayMode, value); }
    }

    public CalendarSelectionMode SelectionMode
    {
        get { return this.selectionMode; }
        set { this.UpdateValue(ref this.selectionMode, value); }
    }

    public CalendarInteractionMode InteractionMode
    {
        get { return this.interactionMode; }
        set { this.UpdateValue(ref this.interactionMode, value); }
    }

    public Orientation NavigationDirection
    {
        get { return this.navigationDirection; }
        set { this.UpdateValue(ref this.navigationDirection, value); }
    }
}