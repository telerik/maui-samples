using System;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TimePickerControl.TimeOnlySupportCategory.TimeOnlySupportExample;

// >> timepicker-timeonly-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private TimeOnly? selectedTime;

    public ViewModel()
    {
        this.MinimumTime = new TimeOnly(10, 30);
        this.DefaultHighlightedTime = new TimeOnly(12, 00);
        this.SelectedTime = new TimeOnly(17, 00);
        this.MaximumTime = new TimeOnly(22, 50);
    }

    public TimeOnly MinimumTime { get; set; }

    public TimeOnly DefaultHighlightedTime { get; set; }

    public TimeOnly? SelectedTime
    {
        get => this.selectedTime;
        set => this.UpdateValue(ref this.selectedTime, value);
    }

    public TimeOnly MaximumTime { get; set; }
}
// << timepicker-timeonly-viewmodel
