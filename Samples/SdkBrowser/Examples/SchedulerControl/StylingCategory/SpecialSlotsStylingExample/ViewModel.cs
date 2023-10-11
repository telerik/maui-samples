using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Scheduler;

namespace SDKBrowserMaui.Examples.SchedulerControl.StylingCategory.SpecialSlotsStylingExample
{
    // >> scheduler-specialslots-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        public ViewModel()
        {
            var now = DateTime.Now;

            this.NonWorkingHours = new ObservableCollection<Slot>();

            DateTime start = new DateTime(2010, 1, 1, 8, 0, 0);
            DateTime end = new DateTime(2010, 1, 1, 18, 0, 0);
            this.NonWorkingHours.Add(new Slot(end, start.AddDays(1))
            {
                RecurrencePattern = new RecurrencePattern(null, RecurrenceDays.Monday | RecurrenceDays.Tuesday | RecurrenceDays.Wednesday | RecurrenceDays.Thursday, RecurrenceFrequency.Weekly, 1, null, null)
            });

            this.NonWorkingHours.Add(new Slot(end, start.AddDays(3))
            {
                RecurrencePattern = new RecurrencePattern(null, RecurrenceDays.Friday, RecurrenceFrequency.Weekly, 1, null, null)
            });
        }

        public ObservableCollection<Slot> NonWorkingHours { get; set; }
    }
    // << scheduler-specialslots-viewmodel
}
