using System;
using Telerik.Maui.Controls;

namespace QSF.Examples.CalendarControl.Common;

public class Reminder : NotifyPropertyChangedBase
{
    private DateTime date;
    private string title;

    public DateTime Date
    {
        get => this.date;
        set => this.UpdateValue(ref this.date, value);
    }

    public string Title
    {
        get => this.title;
        set => this.UpdateValue(ref this.title, value);
    }
}