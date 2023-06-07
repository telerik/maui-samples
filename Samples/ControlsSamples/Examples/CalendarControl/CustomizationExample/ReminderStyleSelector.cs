using Microsoft.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace QSF.Examples.CalendarControl.CustomizationExample;

public class ReminderStyleSelector : CalendarStyleSelector
{
    private Style reminderMouseOverBorderStyle;
    private Style reminderSelectedBorderStyle;
    private Style reminderSelectedMouseOverBorderStyle;

    public override Style SelectStyle(object item, BindableObject container)
    {
        if (container == null)
        {
            return base.SelectStyle(item, container);
        }

        var node = (CalendarNode)item;

        if (node.IsSelected)
        {
            return node.IsMouseOver ? this.ReminderSelectedMouseOverBorderStyle : this.ReminderSelectedBorderStyle;
        }

        if (node.IsMouseOver)
        {
            return this.ReminderMouseOverBorderStyle;
        }

        return null;
    }

    public Style ReminderMouseOverBorderStyle
    {
        get => this.reminderMouseOverBorderStyle;
        set
        {
            this.reminderMouseOverBorderStyle = value;
            this.reminderMouseOverBorderStyle.BasedOn = this.MouseOverBorderStyle;
        }
    }

    public Style ReminderSelectedBorderStyle
    {
        get => this.reminderSelectedBorderStyle;
        set
        {
            this.reminderSelectedBorderStyle = value;
            this.reminderSelectedBorderStyle.BasedOn = this.SelectedBorderStyle;
        }
    }

    public Style ReminderSelectedMouseOverBorderStyle
    {
        get => this.reminderSelectedMouseOverBorderStyle;
        set
        {
            this.reminderSelectedMouseOverBorderStyle = value;
            this.reminderSelectedMouseOverBorderStyle.BasedOn = this.ReminderSelectedBorderStyle;
        }
    }
}