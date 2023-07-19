using System;
using System.Collections.Generic;
using QSF.ViewModels;
using Telerik.Maui.Controls.Calendar;

namespace QSF.Examples.CalendarControl.SelectionExample;

public class SelectionViewModel : ExampleViewModel
{
    private CalendarSelectionMode selectionMode;

    public SelectionViewModel()
    {
        this.SelectionModes = (IEnumerable<CalendarSelectionMode>)Enum.GetValues(typeof(CalendarSelectionMode));
        this.SelectionMode = CalendarSelectionMode.Single;
    }

    public IEnumerable<CalendarSelectionMode> SelectionModes { get; }

    public CalendarSelectionMode SelectionMode
    {
        get { return this.selectionMode; }
        set { this.UpdateValue(ref this.selectionMode, value); }
    }
}