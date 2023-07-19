using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QSF.ViewModels;
using Telerik.Maui.Controls.Calendar;

namespace QSF.Examples.CalendarControl.DisplayModeExample;

public class DisplayModeViewModel : ExampleViewModel
{
    private CalendarDisplayMode displayMode = CalendarDisplayMode.Month;
    private CalendarDisplayMode minDisplayMode = CalendarDisplayMode.Month;
    private CalendarDisplayMode maxDisplayMode = CalendarDisplayMode.Century;
    private ObservableCollection<CalendarDisplayMode> availableDisplayModes;
    private ObservableCollection<CalendarDisplayMode> displayModes;
    private ObservableCollection<CalendarDisplayMode> minDisplayModes;
    private ObservableCollection<CalendarDisplayMode> maxDisplayModes;

    public DisplayModeViewModel()
    {
        this.availableDisplayModes = new ObservableCollection<CalendarDisplayMode>((IEnumerable<CalendarDisplayMode>)Enum.GetValues(typeof(CalendarDisplayMode)));;
        this.DisplayModes = this.availableDisplayModes;
        this.MaxDisplayModes = this.availableDisplayModes;
        this.MinDisplayModes = this.availableDisplayModes;
    }

    public ObservableCollection<CalendarDisplayMode> DisplayModes
    {
        get { return this.displayModes; }
        set { this.UpdateValue(ref this.displayModes, value); }
    }

    public ObservableCollection<CalendarDisplayMode> MinDisplayModes
    {
        get { return this.minDisplayModes; }
        set { this.UpdateValue(ref this.minDisplayModes, value); }
    }

    public ObservableCollection<CalendarDisplayMode> MaxDisplayModes
    {
        get { return this.maxDisplayModes; }
        set { this.UpdateValue(ref this.maxDisplayModes, value); }
    }

    public CalendarDisplayMode DisplayMode
    {
        get { return this.displayMode; }
        set { this.UpdateValue(ref this.displayMode, value); }
    }

    public CalendarDisplayMode MinDisplayMode
    {
        get { return this.minDisplayMode; }
        set
        {
            if (this.minDisplayMode != value)
            {
                this.minDisplayMode = value;

                this.MaxDisplayModes = this.GenerateMaxDisplayModeSource();
                this.OnPropertyChanged(nameof(this.MaxDisplayMode));

                this.DisplayModes = this.GenerateDisplayModeSource();
                this.OnPropertyChanged(nameof(this.DisplayMode));

                this.OnPropertyChanged();
            }
        }
    }

    public CalendarDisplayMode MaxDisplayMode
    {
        get { return this.maxDisplayMode; }
        set
        {
            if (this.maxDisplayMode != value)
            {
                this.maxDisplayMode = value;

                this.MinDisplayModes = this.GenerateMinDisplayModeSource();
                this.OnPropertyChanged(nameof(this.MinDisplayMode));

                this.DisplayModes = this.GenerateDisplayModeSource();
                this.OnPropertyChanged(nameof(this.DisplayMode));

                this.OnPropertyChanged();
            }
        }
    }

    private ObservableCollection<CalendarDisplayMode> GenerateDisplayModeSource()
    {
        var modes = new ObservableCollection<CalendarDisplayMode>();
        foreach (CalendarDisplayMode mode in this.availableDisplayModes)
        {
            if (this.maxDisplayMode >= mode && mode >= this.minDisplayMode)
            {
                modes.Add(mode);
            }
        }

        return modes;
    }

    private ObservableCollection<CalendarDisplayMode> GenerateMinDisplayModeSource()
    {
        var modes = new ObservableCollection<CalendarDisplayMode>();
        foreach (CalendarDisplayMode mode in this.availableDisplayModes)
        {
            if (this.maxDisplayMode >= mode)
            {
                modes.Add(mode);
            }
        }

        return modes;
    }

    private ObservableCollection<CalendarDisplayMode> GenerateMaxDisplayModeSource()
    {
        var modes = new ObservableCollection<CalendarDisplayMode>();
        foreach (CalendarDisplayMode mode in this.availableDisplayModes)
        {
            if (this.minDisplayMode <= mode)
            {
                modes.Add(mode);
            }
        }

        return modes;
    }
}