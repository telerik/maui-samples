using System.Collections.Generic;
using SDKBrowserMaui.Examples.AutoCompleteControl.Models;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels
{

    // >> autocomplete-city-viewmodel
    public class CityViewModel
    {
        public List<City> Source { get; set; }
        public CityViewModel()
        {
            this.Source = new List<City>()
              {
                  new City("Madrid"),
                  new City("Paris"),
                  new City("Barcelona"),
                  new City("New York"),
                  new City("Budapest"),
                  new City("Sofia"),
                  new City("Palermo"),
                  new City("Melbourne"),
                  new City("London"),
                  new City("Nagoya"),
                  new City("Tokyo"),
                  new City("Atlanta"),
                  new City("Toronto"),
                  new City("Athens"),
              };
        }
    }
    // << autocomplete-city-viewmodel
}
