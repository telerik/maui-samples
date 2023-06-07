using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Devices;
using QSF.ViewModels;
using Telerik.Maui;
using Telerik.Maui.Controls.Calendar;

namespace QSF.Examples.CalendarControl.ConfigurationExample;

public class ConfigurationViewModel : ExampleViewModel
{
    private CultureInfo culture;
    private CultureData selectedCulture;
    private DateTime minDate;
    private DateTime maxDate;
    private DayOfWeek firstDayOfWeek;
    private bool areDayNamesVisible;
    private bool isOutOfScopeVisible;
    private CalendarDisplayMode displayMode;
    private CalendarDisplayMode minDisplayMode;
    private CalendarDisplayMode maxDisplayMode;
    private CalendarSelectionMode selectionMode;
    private CalendarInteractionMode interactionMode;
    private Orientation navigationDirection;

    public ConfigurationViewModel()
    {
        this.Cultures = new List<CultureData>()
        {
            new CultureData("Gregorian US", "en-US"),
            new CultureData("Gregorian BG", "bg-BG"),
            new CultureData("Hebrew", "he-IL"),
            new CultureData("Hijri", "ar-SA"),
            new CultureData("Japanese", "ja"),
            new CultureData("Persian", "fa"),
            new CultureData("Taiwan", "zh-TW"),
            new CultureData("Thai/Buddhist", "th"),
            new CultureData("Umm al-Qura", "ar-SA")
        };

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

        this.SelectedCulture = this.Cultures[0];
        this.MinDate = new DateTime(1900, 1, 1);
        this.MaxDate = new DateTime(2099, 12, 31);
        this.AreDayNamesVisible = true;
        this.IsOutOfScopeVisible = true;
        this.DisplayMode = CalendarDisplayMode.Month;
        this.MinDisplayMode = CalendarDisplayMode.Month;
        this.MaxDisplayMode = CalendarDisplayMode.Century;
        this.SelectionMode = CalendarSelectionMode.Single;
    }

    public List<CultureData> Cultures { get; }

    public IEnumerable<DayOfWeek> DaysOfWeek { get; }

    public IEnumerable<CalendarDisplayMode> DisplayModes { get; }

    public IEnumerable<CalendarSelectionMode> SelectionModes { get; }

    public IEnumerable<CalendarInteractionMode> InteractionModes { get; }

    public IEnumerable<Orientation> NavigationDirections { get; }

    public CultureInfo Culture
    {
        get { return this.culture; }
        set { this.UpdateValue(ref this.culture, value); }
    }

    public CultureData SelectedCulture
    {
        get { return this.selectedCulture; }
        set
        {
            if (this.selectedCulture != value)
            {
                this.selectedCulture = value;
                this.UpdateValue(ref this.selectedCulture, value);

                this.Culture = CreateCultureInfo(this.selectedCulture);
            }
        }
    }

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

    public CalendarDisplayMode MinDisplayMode
    {
        get { return this.minDisplayMode; }
        set { this.UpdateValue(ref this.minDisplayMode, value); }
    }

    public CalendarDisplayMode MaxDisplayMode
    {
        get { return this.maxDisplayMode; }
        set { this.UpdateValue(ref this.maxDisplayMode, value); }
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

    private static CultureInfo CreateCultureInfo(CultureData data)
    {
        CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(data.Name);
        switch (data.DisplayName)
        {
            case "Hebrew":
                cultureInfo.DateTimeFormat.Calendar = new HebrewCalendar();
                break;
            case "Hijri":
                cultureInfo.DateTimeFormat.Calendar = new HijriCalendar();
                break;
            case "Japanese":
                // The JapaneseCalendar and JapaneseLunisolarCalendar are currently the only two calendar classes in .NET that recognize more than one era.
                // As the Calendar control works with only one era, we use the GregorianCalendar.
                cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
                break;
            case "Persian":
                cultureInfo.DateTimeFormat.Calendar = new PersianCalendar();
                break;
            case "Taiwan":
                cultureInfo.DateTimeFormat.Calendar = new TaiwanCalendar();
                break;
            case "Thai/Buddhist":
                cultureInfo.DateTimeFormat.Calendar = new ThaiBuddhistCalendar();
                break;
            case "Umm al-Qura":
                cultureInfo.DateTimeFormat.Calendar = new UmAlQuraCalendar();
                break;
        }

        return cultureInfo;
    }
}