using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.ItemSwipeCategory;

// >> collectionview-itemswipe-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
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
    }

    public ObservableCollection<DataModel> Locations { get; set; }
}
// << collectionview-itemswipe-viewmodel
