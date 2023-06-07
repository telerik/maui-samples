using Microsoft.Maui.Controls;
using System;
using Telerik.Maui.Controls.Calendar;

namespace SDKBrowserMaui.Examples.CalendarControl.StyleSelectorsCategory;

// >> calendar-styleselectors-custom-calendarstyleselector
public class CustomCalendarStyleSelector : CalendarStyleSelector
{
    private Style customNormalLabelStyle;
    private Style customOutOfScopeLabelStyle;
    private Style customSelectedLabelStyle;

    public override Style SelectStyle(object item, BindableObject container)
    {
        var node = (CalendarNode)item;
        var date = node.Date;
        if (SundayLabelStyle != null && !node.IsSelected && date != null && date.Value.DayOfWeek == DayOfWeek.Sunday)
        {
            return SundayLabelStyle;
        }

        if (node.IsOutOfScope)
        {
            return customOutOfScopeLabelStyle;
        }

        if (node.IsSelected)
        {
            return customSelectedLabelStyle;
        }

        if (node.IsToday || !node.IsEnabled)
        {
            return base.SelectStyle(item, container);
        }

        return customNormalLabelStyle;
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

    public Style SundayLabelStyle { get; set; }
}
// << calendar-styleselectors-custom-calendarstyleselector