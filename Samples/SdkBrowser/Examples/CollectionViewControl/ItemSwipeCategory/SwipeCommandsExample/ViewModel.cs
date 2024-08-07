using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.CollectionView;


namespace SDKBrowserMaui.Examples.CollectionViewControl.ItemSwipeCategory.SwipeCommandsExample;

// >> collectionview-itemswipe-commands-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private string swipeActionLog;

    public ViewModel()
    {
        this.Locations = new ObservableCollection<DataModel>
        {
            new DataModel { Country = "Austria", City = "Vienna", Visited = true },
            new DataModel { Country = "Belgium", City = "Antwerp" },
            new DataModel { Country = "Denmark", City = "Copenhagen" },
            new DataModel { Country = "France", City = "Nice" },
            new DataModel { Country = "France", City = "Paris", Visited = true },
            new DataModel { Country = "Germany", City = "Berlin", Visited = true },
            new DataModel { Country = "Germany", City = "Munich" },
            new DataModel { Country = "Germany", City = "Nuremberg", Visited = true },
            new DataModel { Country = "Italy", City = "Bari" },
            new DataModel { Country = "Italy", City = "Rome" },
            new DataModel { Country = "Netherlands", City = "Amsterdam" },
            new DataModel { Country = "Portugal", City = "Lisbon" },
            new DataModel { Country = "Spain", City = "Barcelona" },
            new DataModel { Country = "Spain", City = "Madrid" },
            new DataModel { Country = "United Kingdom", City = "London" },
            new DataModel { Country = "United Kingdom", City = "Manchester" },
            new DataModel { Country = "United States", City = "New York" },
            new DataModel { Country = "United States", City = "Los Angeles" },
            new DataModel { Country = "United States", City = "Chicago" },
            new DataModel { Country = "United States", City = "Boston" },
            new DataModel { Country = "United States", City = "San Francisco" },
            new DataModel { Country = "Canada", City = "Vancouver" },
            new DataModel { Country = "Brazil", City = "Rio de Janeiro" },
            new DataModel { Country = "Brazil", City = "Sao Paulo" },
            new DataModel { Country = "Argentina", City = "Buenos Aires" },
            new DataModel { Country = "Peru", City = "Lima", Visited = true },
            new DataModel { Country = "Colombia", City = "Bogota" },
            new DataModel { Country = "Bolivia", City = "La Paz" },
            new DataModel { Country = "Venezuela", City = "Caracas" },
            new DataModel { Country = "Chile", City = "Santiago" },
            new DataModel { Country = "China", City = "Hong Kong" },
            new DataModel { Country = "China", City = "Shanghai" },
            new DataModel { Country = "India", City = "Delhi" },
            new DataModel { Country = "Japan", City = "Tokyo", Visited = true },
            new DataModel { Country = "Japan", City = "Osaka" },
            new DataModel { Country = "Vietnam", City = "Hanoi" },
            new DataModel { Country = "Thailand", City = "Bangkok" },
            new DataModel { Country = "Thailand", City = "Phuket" },
            new DataModel { Country = "Nigeria", City = "Lagos" },
            new DataModel { Country = "Egypt", City = "Cairo" },
            new DataModel { Country = "Algeria", City = "Algiers" },
            new DataModel { Country = "Australia", City = "Sydney", Visited = true },
            new DataModel { Country = "Australia", City = "Melbourne" },
            new DataModel { Country = "New Zealand", City = "Auckland" },
            new DataModel { Country = "New Zealand", City = "Wellington" }
        };

        this.SwipeStartingCommand = new Command(this.OnSwipeStartingCommandExecute);
        this.SwipingCommand = new Command(this.OnSwipingCommandExecute);
        this.SwipeCompletedCommand = new Command(this.OnSwipeCompletedCommandExecute);
    }

    public ObservableCollection<DataModel> Locations { get; set; }

    public ICommand SwipeStartingCommand { get; set; }
    public ICommand SwipingCommand { get; set; }
    public ICommand SwipeCompletedCommand { get; set; }
    public ICommand EndItemSwipeCommand { get; set; }

    public string SwipeActionLog
    {
        get => this.swipeActionLog;
        set => UpdateValue(ref this.swipeActionLog, value);
    }

    private void OnSwipeStartingCommandExecute(object obj)
    {
        var context = obj as CollectionViewSwipeStartingCommandContext;
        if (context != null)
        {
            var item = (DataModel)context.Item;
            if (item.Country == "Spain")
            {
                context.Cancel = true;
                this.SwipeActionLog = $"Swiping cities in {item.Country} is canceled";
            }
        }
    }

    private void OnSwipingCommandExecute(object obj)
    {
        var context = obj as CollectionViewSwipingCommandContext;
        if (context != null)
        {
            var item = (DataModel)context.Item;
            this.SwipeActionLog = $"Swiping {item.City} with Offset: {context.Offset:F0}";
        }
    }

    private async void OnSwipeCompletedCommandExecute(object obj)
    {
        var context = obj as CollectionViewSwipeCompletedCommandContext;
        if (context != null)
        {
            var item = (DataModel)context.Item;
            this.SwipeActionLog = $"SwipeCompleted {item.City}";

            if (context.Offset <= -25)
            {
                await Task.Delay(200);
                this.Locations.Remove(item);

                this.SwipeActionLog += $", deleted {item.City}";
                this.EndItemSwipeCommand?.Execute(false);
            }
            else if (context.Offset >= 25)
            {
                await Task.Delay(200);
                item.Visited = !item.Visited;

                this.SwipeActionLog += $", updated {item.City}";
                this.EndItemSwipeCommand?.Execute(true);
            }
        }
    }
}
// << collectionview-itemswipe-commands-viewmodel