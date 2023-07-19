using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace SDKBrowserMaui.Examples.CalendarControl.StyleSelectorsCategory;

// >> calendar-styleselectors-custom-calendarstyleselector
public class CustomCalendarStyleSelector : CalendarStyleSelector
{
    private Style customNormalLabelStyle;
    private Style customOutOfScopeLabelStyle;
    private Style customSelectedLabelStyle;
    private Style customSelectedMouseOverLabelStyle;
    private Style sundayMouseOverLabelStyle;
    private Style sundaySelectedLabelStyle;
    private Style sundaySelectedMouseOverLabelStyle;

    public override Style SelectStyle(object item, BindableObject container)
    {
        var node = (CalendarNode)item;
        var calendar = (RadCalendar)node.Calendar;
        bool isToday = node.IsToday;
        bool isSunday = calendar.DisplayMode == CalendarDisplayMode.Month && node.Date.Value.DayOfWeek == DayOfWeek.Sunday;
        bool isMouseOver = node.IsMouseOver;

        if (node.IsSelected)
        {
            return isMouseOver
                ? isSunday ? this.SundaySelectedMouseOverLabelStyle : this.CustomSelectedMouseOverLabelStyle
                : isSunday ? this.SundaySelectedLabelStyle : this.CustomSelectedLabelStyle;
        }

        if (isMouseOver)
        {
            return isSunday ? this.SundayMouseOverLabelStyle : base.SelectStyle(item, container);
        }

        if (isToday || !node.IsEnabled)
        {
            return base.SelectStyle(item, container);
        }

        if (node.IsOutOfScope)
        {
            return this.CustomOutOfScopeLabelStyle;
        }

        return isSunday ? this.SundayLabelStyle : this.CustomNormalLabelStyle;
    }

    public Style CustomNormalLabelStyle
    {
        get => customNormalLabelStyle;
        set
        {
            customNormalLabelStyle = value;
            customNormalLabelStyle.BasedOn = this.NormalLabelStyle;
        }
    }

    public Style CustomOutOfScopeLabelStyle
    {
        get => customOutOfScopeLabelStyle;
        set
        {
            customOutOfScopeLabelStyle = value;
            customOutOfScopeLabelStyle.BasedOn = this.OutOfScopeLabelStyle;
        }
    }

    public Style CustomSelectedLabelStyle
    {
        get => customSelectedLabelStyle;
        set
        {
            customSelectedLabelStyle = value;
            customSelectedLabelStyle.BasedOn = this.SelectedBorderStyle;
        }
    }

    public Style CustomSelectedMouseOverLabelStyle
    {
        get => customSelectedMouseOverLabelStyle;
        set
        {
            customSelectedMouseOverLabelStyle = value;
            customSelectedMouseOverLabelStyle.BasedOn = this.CustomSelectedLabelStyle;
        }
    }

    public Style SundayLabelStyle { get; set; }

    public Style SundayMouseOverLabelStyle
    {
        get => sundayMouseOverLabelStyle;
        set
        {
            sundayMouseOverLabelStyle = value;
            sundayMouseOverLabelStyle.BasedOn = this.SundayLabelStyle;
        }
    }

    public Style SundaySelectedLabelStyle
    {
        get => sundaySelectedLabelStyle;
        set
        {
            sundaySelectedLabelStyle = value;
            sundaySelectedLabelStyle.BasedOn = this.SundayLabelStyle;
        }
    }

    public Style SundaySelectedMouseOverLabelStyle
    {
        get => sundaySelectedMouseOverLabelStyle;
        set
        {
            sundaySelectedMouseOverLabelStyle = value;
            sundaySelectedMouseOverLabelStyle.BasedOn = this.SundayMouseOverLabelStyle;
        }
    }
}
// << calendar-styleselectors-custom-calendarstyleselector