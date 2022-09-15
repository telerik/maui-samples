using SDKBrowserMaui.Examples.AutoCompleteControl.FeaturesCategory.CustomFilteringExample;
using SDKBrowserMaui.Examples.AutoCompleteControl.Models;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels
{
    // >> autocomplete-filering-viewmodel
    public class CustomFilteringViewModel
    {
        public CustomFilteringViewModel()
        {
            this.Source = new ObservableCollection<Person>()
            {
                new Person("Freda", "Curtis"),
                new Person("Jeffery", "Francis"),
                new Person("Eva", "Lawson"),
                new Person("Emmett", "Santos"),
                new Person("Theresa", "Bryan"),
                new Person("Terrell", "Norris"),
                new Person("Eric", "Wheeler"),
                new Person("Alfredo", "Thornton"),
                new Person("Roberto", "Romero"),
                new Person("Orlando", "Mathis"),
                new Person("Eduardo", "Thomas"),
                new Person("Harry", "Douglas"),
                new Person("Merry", "Lasker")
            };

            this.Filter = new CustomAutoCompleteFilter();
        }

        public ObservableCollection<Person> Source { get; set; }

        public CustomAutoCompleteFilter Filter { get; set; }
    }
    // << autocomplete-filering-viewmodel
}
