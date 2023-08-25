using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeViewControl.ScrollingCategory
{
    // >> treeview-location-viewmodel
    public class LocationViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<Country> countries;
        private Location selectedLocation;

        public LocationViewModel()
        {
            this.Countries = new ObservableCollection<Country>
            {
                new Country
                {
                    Name = "Austria",
                    Cities =
                    {
                        new City { Name = "Graz" },
                        new City { Name = "Innsbruck" },
                        new City { Name = "Leonding - a city southwest of Linz in the Austrian state of Upper Austria which borders Puchenau and the river Danube in the north, Wilhering and Pasching in the west, Traun in the south and Linz in the east and has a population of more than 27 thousand people, making it the most populous city of the Linz-Land district and the fourth most populous city in Upper Austria" },
                        new City { Name = "Linz" },
                        new City { Name = "Ratz" },
                        new City { Name = "Salzburg" },
                        new City { Name = "Vienna" },
                        new City { Name = "Wolfsberg" },
                        new City { Name = "Zeltweg" }
                    }
                },
                new Country
                {
                    Name = "Belgium",
                    Cities =
                    {
                        new City { Name = "Antwerp" },
                        new City { Name = "Assesse" },
                        new City { Name = "Bruges" },
                        new City { Name = "Charleroi" },
                        new City { Name = "Lint" },
                        new City { Name = "Ranst" },
                        new City { Name = "Schaffen" },
                        new City { Name = "Veurne" },
                        new City { Name = "Zingem" },
                    }
                },
                new Country
                {
                    Name = "Denmark",
                    Cities =
                    {
                        new City { Name = "Aalborg" },
                        new City { Name = "Aarhus" },
                        new City { Name = "Billund" },
                        new City { Name = "Copenhagen" },
                        new City { Name = "Karup" },
                        new City { Name = "Odense" },
                        new City { Name = "Viborg" },
                        new City { Name = "Vojens" }
                    }
                },
                new Country
                {
                    Name = "France",
                    Cities =
                    {
                        new City { Name = "Aurillac" },
                        new City { Name = "Belley" },
                        new City { Name = "Carcassonne" },
                        new City { Name = "Caen" },
                        new City { Name = "Deauville" },
                        new City { Name = "La Rochelle" },
                        new City { Name = "Nice" },
                        new City { Name = "Marseille" },
                        new City { Name = "Paris" },
                        new City { Name = "Rodez" }
                    }
                },
                new Country
                {
                    Name = "Germany",
                    Cities =
                    {
                        new City { Name = "Baden-Baden" },
                        new City { Name = "Berlin" },
                        new City { Name = "Borkum" },
                        new City{ Name = "Bremen" },
                        new City{ Name = "Dortmund" },
                        new City{ Name = "Dresden" },
                        new City{ Name = "Hamburg" },
                        new City{ Name = "Hannover" },
                        new City{ Name = "Leipzig" },
                        new City{ Name = "Mannheim" },
                        new City{ Name = "Munich" },
                        new City{ Name = "Nuremberg" }
                    }
                },
                new Country
                {
                    Name = "Italy",
                    Cities =
                    {
                        new City { Name = "Aosta" },
                        new City { Name = "Bari" },
                        new City { Name = "Bologna" },
                        new City { Name = "Parma" },
                        new City { Name = "Rimini" },
                        new City { Name = "Rome" }
                    }
                },
                new Country
                {
                    Name = "Netherlands",
                    Cities =
                    {
                        new City { Name = "Amsterdam" },
                        new City { Name = "Bonaire" },
                        new City { Name = "Eindhoven" },
                        new City { Name = "Maastricht" },
                        new City { Name = "Rotterdam" }
                    }
                },
                new Country
                {
                    Name = "Portugal",
                    Cities =
                    {
                        new City { Name = "Braga" },
                        new City { Name = "Cascais" },
                        new City { Name = "Lisbon" },
                        new City { Name = "Porto" }
                    }
                },
                new Country
                {
                    Name = "Spain",
                    Cities =
                    {
                        new City { Name = "Alicante" },
                        new City { Name = "Barcelona" },
                        new City { Name = "Madrid" },
                        new City { Name = "Seville" },
                        new City { Name = "Valencia" },
                        new City { Name = "Zaragoza" }
                    }
                },
                new Country
                {
                    Name = "United Kingdom",
                    Cities =
                    {
                        new City { Name = "Bristol" },
                        new City { Name = "Liverpool" },
                        new City { Name = "London" },
                        new City { Name = "Manchester" },
                        new City { Name = "Norwich" },
                        new City { Name = "Southampton" }
                    }
                },
            };
        }

        public Location SelectedLocation
        {
            get => this.selectedLocation;
            set => this.UpdateValue(ref this.selectedLocation, value);
        }

        public ObservableCollection<Country> Countries 
        {
            get => this.countries;
            set => this.UpdateValue(ref this.countries, value);
        }
    }
    // << treeview-location-viewmodel
}
