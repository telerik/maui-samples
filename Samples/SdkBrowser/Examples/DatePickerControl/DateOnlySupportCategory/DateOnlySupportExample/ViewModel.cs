using System;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DatePickerControl.DateOnlySupportCategory.DateOnlySupportExample;

// >> datepicker-dateonly-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private DateOnly? selectedDate;

    public ViewModel()
    {
        this.MinimumDate = new DateOnly(2020, 1, 1);
        this.DefaultHighlightedDate = new DateOnly(2024, 11, 20);
        this.SelectedDate = new DateOnly(2024, 11, 23);
        this.MaximumDate = new DateOnly(2028, 12, 31);
    }

    public DateOnly MinimumDate { get; set; }

    public DateOnly DefaultHighlightedDate { get; set; }

    public DateOnly? SelectedDate
    {
        get => this.selectedDate;
        set => this.UpdateValue(ref this.selectedDate, value);
    }

    public DateOnly MaximumDate { get; set; }
}
// << datepicker-dateonly-viewmodel
