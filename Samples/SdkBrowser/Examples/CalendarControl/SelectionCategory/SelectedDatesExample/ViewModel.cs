using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace SDKBrowserMaui.Examples.CalendarControl.SelectionCategory.SelectedDatesExample
{
    // >> calendar-selection-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        private CalendarSelectionCollection selectedDates;
        public CalendarSelectionCollection SelectedDates
        {
            get => this.selectedDates;
            set
            {
                this.UpdateValue(ref this.selectedDates, value);
                this.selectedDates.Add(DateTime.Today.AddDays(2));
                this.selectedDates.Add(DateTime.Today.AddDays(7));
                this.selectedDates.Add(DateTime.Today.AddDays(10));
            }
        }
    }
    // << calendar-selection-viewmodel
}
