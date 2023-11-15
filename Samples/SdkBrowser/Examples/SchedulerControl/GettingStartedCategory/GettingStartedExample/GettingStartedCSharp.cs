using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SchedulerControl.GettingStartedCategory.GettingStartedExample
{
	public class GettingStartedCSharp : ContentView
	{
		public GettingStartedCSharp ()
		{
            var content = new Grid();

            // >> scheduler-gettingstarted-csharp
            var scheduler = new RadScheduler();
            scheduler.ViewDefinitions.Add(new WeekViewDefinition());
            scheduler.ViewDefinitions.Add(new WeekViewDefinition(){IsWeekendVisible = false, Title = "Work Week"});
            scheduler.ViewDefinitions.Add(new MultidayViewDefinition(){VisibleDays = 3, Title = "3 Day"});
            scheduler.ViewDefinitions.Add(new MonthViewDefinition());
            scheduler.ViewDefinitions.Add(new DayViewDefinition());
            // << scheduler-gettingstarted-csharp

            content.Add(scheduler);
            this.Content = content;
        }
	}
}