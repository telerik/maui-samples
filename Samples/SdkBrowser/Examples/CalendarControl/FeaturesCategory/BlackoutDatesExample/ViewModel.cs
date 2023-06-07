using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Calendar;

namespace SDKBrowserMaui.Examples.CalendarControl.FeaturesCategory.BlackoutDatesExample
{
    // >> calendar-blackoutdates-viewmode
    public class ViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<DateTime> blackoutDates;

        public ViewModel()
        {
            this.BlackoutDates = new ObservableCollection<DateTime>();
            this.BlackoutDates.Add(DateTime.Today.AddDays(3));
            this.BlackoutDates.Add(DateTime.Today.AddDays(8));
            this.BlackoutDates.Add(DateTime.Today.AddDays(6));
            this.BlackoutDates.Add(DateTime.Today.AddDays(15));
        }

        public ObservableCollection<DateTime> BlackoutDates
        {
            get => this.blackoutDates;
            set => this.UpdateValue(ref this.blackoutDates, value);
        }
    }
    // << calendar-blackoutdates-viewmodel
}
