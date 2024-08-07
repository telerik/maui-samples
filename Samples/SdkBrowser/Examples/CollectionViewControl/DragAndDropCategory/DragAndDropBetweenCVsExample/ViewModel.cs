using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.DragAndDropCategory.DragAndDropBetweenCVsExample;

// >> collectionview-draganddrop-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    public ViewModel()
    {
        this.LocationsLeft = new ObservableCollection<DataModel>
        {
            new DataModel { Continent = "Europe", Country = "Austria", City = "Graz", Id = 1 },
            new DataModel { Continent = "Europe", Country = "Austria", City = "Innsbruck", Id = 2 },
            new DataModel { Continent = "Europe", Country = "Austria", City = "Ratz", Id = 3 },
            new DataModel { Continent = "Europe", Country = "Austria", City = "Vienna", Id = 4 },
            new DataModel { Continent = "Europe", Country = "Belgium", City = "Antwerp", Id = 5 },
            new DataModel { Continent = "Europe", Country = "Belgium", City = "Charleroi", Id = 6 },
            new DataModel { Continent = "Europe", Country = "Belgium", City = "Schaffen", Id = 7 },
            new DataModel { Continent = "Europe", Country = "Denmark", City = "Copenhagen", Id = 8 },
            new DataModel { Continent = "Europe", Country = "Denmark", City = "Odense", Id = 9 },
            new DataModel { Continent = "Europe", Country = "France", City = "Nice", Id = 10 },
            new DataModel { Continent = "Europe", Country = "France", City = "Paris", Id = 11 },
            new DataModel { Continent = "Europe", Country = "Germany", City = "Baden-Baden", Id = 12 },
            new DataModel { Continent = "Europe", Country = "Germany", City = "Berlin", Id = 13 },
            new DataModel { Continent = "Europe", Country = "Germany", City = "Hamburg", Id = 14 },
            new DataModel { Continent = "Europe", Country = "Germany", City = "Munich", Id = 15 },
            new DataModel { Continent = "Europe", Country = "Germany", City = "Nuremberg", Id = 16 },
            new DataModel { Continent = "Europe", Country = "Italy", City = "Bari", Id = 17 },
            new DataModel { Continent = "Europe", Country = "Italy", City = "Rome", Id = 18 },
            new DataModel { Continent = "Europe", Country = "Netherlands", City = "Amsterdam", Id = 19 },
            new DataModel { Continent = "Europe", Country = "Netherlands", City = "Eindhoven", Id = 20 },
            new DataModel { Continent = "Europe", Country = "Netherlands", City = "Rotterdam", Id = 21 },
            new DataModel { Continent = "Europe", Country = "Portugal", City = "Lisbon", Id = 22 },
            new DataModel { Continent = "Europe", Country = "Portugal", City = "Porto", Id = 23 }
        };

        this.LocationsRight = new ObservableCollection<DataModel>
        {
            new DataModel { Continent = "Europe", Country = "Spain", City = "Barcelona", Id = 24 },
            new DataModel { Continent = "Europe", Country = "Spain", City = "Madrid", Id = 25 },
            new DataModel { Continent = "Europe", Country = "United Kingdom", City = "London", Id = 26 },
            new DataModel { Continent = "Europe", Country = "United Kingdom", City = "Manchester", Id = 27 }
        };
    }

    public ObservableCollection<DataModel> LocationsLeft { get; set; }

    public ObservableCollection<DataModel> LocationsRight { get; set; }
}
// << collectionview-draganddrop-viewmodel