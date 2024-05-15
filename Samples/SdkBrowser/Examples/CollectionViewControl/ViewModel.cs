using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl;

// >> collectionview-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    public ViewModel()
    {
        this.Locations = new ObservableCollection<DataModel>
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
            new DataModel { Continent = "Europe", Country = "Portugal", City = "Porto", Id = 23 },
            new DataModel { Continent = "Europe", Country = "Spain", City = "Barcelona", Id = 24 },
            new DataModel { Continent = "Europe", Country = "Spain", City = "Madrid", Id = 25 },
            new DataModel { Continent = "Europe", Country = "United Kingdom", City = "London", Id = 26 },
            new DataModel { Continent = "Europe", Country = "United Kingdom", City = "Manchester", Id = 27 },
            new DataModel { Continent = "North America", Country = "United States", City = "New York", Id = 28 },
            new DataModel { Continent = "North America", Country = "United States", City = "Los Angeles", Id = 29 },
            new DataModel { Continent = "North America", Country = "United States", City = "Chicago", Id = 30 },
            new DataModel { Continent = "North America", Country = "United States", City = "Boston", Id = 31 },
            new DataModel { Continent = "North America", Country = "United States", City = "San Francisco", Id = 32 },
            new DataModel { Continent = "North America", Country = "Canada", City = "Toronto", Id = 33 },
            new DataModel { Continent = "North America", Country = "Canada", City = "Vancouver", Id = 34 },
            new DataModel { Continent = "North America", Country = "Canada", City = "Ottawa", Id = 35 },
            new DataModel { Continent = "South America", Country = "Brazil", City = "Rio de Janeiro", Id = 36 },
            new DataModel { Continent = "South America", Country = "Brazil", City = "Sao Paulo", Id = 37 },
            new DataModel { Continent = "South America", Country = "Brazil", City = "Salvador", Id = 38 },
            new DataModel { Continent = "South America", Country = "Argentina", City = "Buenos Aires", Id = 39 },
            new DataModel { Continent = "South America", Country = "Peru", City = "Lima", Id = 40 },
            new DataModel { Continent = "South America", Country = "Peru", City = "Cusco", Id = 41 },
            new DataModel { Continent = "South America", Country = "Colombia", City = "Bogota", Id = 42 },
            new DataModel { Continent = "South America", Country = "Bolivia", City = "La Paz", Id = 43 },
            new DataModel { Continent = "South America", Country = "Venezuela", City = "Caracas", Id = 44 },
            new DataModel { Continent = "South America", Country = "Chile", City = "Santiago", Id = 45 },
            new DataModel { Continent = "Asia", Country = "China", City = "Hong Kong", Id = 46 },
            new DataModel { Continent = "Asia", Country = "China", City = "Shanghai", Id = 47 },
            new DataModel { Continent = "Asia", Country = "China", City = "Macau", Id = 48 },
            new DataModel { Continent = "Asia", Country = "India", City = "Delhi", Id = 49 },
            new DataModel { Continent = "Asia", Country = "India", City = "Hyderabad", Id = 50 },
            new DataModel { Continent = "Asia", Country = "Japan", City = "Tokyo", Id = 51 },
            new DataModel { Continent = "Asia", Country = "Japan", City = "Osaka", Id = 52 },
            new DataModel { Continent = "Asia", Country = "Japan", City = "Kyoto", Id = 53 },
            new DataModel { Continent = "Asia", Country = "Vietnam", City = "Ho Chi Minh", Id = 54 },
            new DataModel { Continent = "Asia", Country = "Vietnam", City = "Hanoi", Id = 55 },
            new DataModel { Continent = "Asia", Country = "Thailand", City = "Bangkok", Id = 56 },
            new DataModel { Continent = "Asia", Country = "Thailand", City = "Phuket", Id = 57 },
            new DataModel { Continent = "Africa", Country = "Nigeria", City = "Lagos", Id = 58 },
            new DataModel { Continent = "Africa", Country = "Egypt", City = "Cairo", Id = 59 },
            new DataModel { Continent = "Africa", Country = "Algeria", City = "Algiers", Id = 60 },
            new DataModel { Continent = "Oceania", Country = "Australia", City = "Sydney", Id = 61 },
            new DataModel { Continent = "Oceania", Country = "Australia", City = "Melbourne", Id = 62 },
            new DataModel { Continent = "Oceania", Country = "Australia", City = "Canberra", Id = 63 },
            new DataModel { Continent = "Oceania", Country = "New Zealand", City = "Auckland", Id = 64 },
            new DataModel { Continent = "Oceania", Country = "New Zealand", City = "Wellington", Id = 65 },
        };
    }

    public ObservableCollection<DataModel> Locations { get; set; }
}
// << collectionview-viewmodel

