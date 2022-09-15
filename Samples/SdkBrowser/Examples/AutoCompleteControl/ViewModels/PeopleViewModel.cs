using SDKBrowserMaui.Examples.AutoCompleteControl.Models;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.AutoCompleteControl.ViewModels
{
    // >> autocomplete-people-viewmodel
    public class PeopleViewModel
    {
        public PeopleViewModel()
        {
            this.Items = new ObservableCollection<Person>
            {
                new Person{FirstName = "Alex", LastName = "Ramos"},
                new Person{FirstName = "Ben", LastName = "Johnas"},
                new Person{FirstName = "Carlos", LastName = "Romero"},
                new Person{FirstName = "Anna", LastName = "Kurtis"},
                new Person{FirstName = "Eva", LastName = "Johnson"},
                new Person{FirstName = "Terry", LastName = "Willson"},
                new Person{FirstName = "John", LastName = "Doe"},
                new Person{FirstName = "Teressa", LastName = "Bryan"},
                new Person{FirstName = "Nick", LastName = "Norris"},
                new Person{FirstName = "Eric", LastName = "Wheeler"},
                new Person{FirstName = "William", LastName = "Montero"},
                new Person{FirstName = "Sam", LastName = "Browm"},
                new Person{FirstName = "Ivan", LastName = "Petrov"},
                new Person{FirstName = "Martin", LastName = "Romero"},
                new Person{FirstName = "Eva", LastName = "Gonzales"},
                new Person{FirstName = "An", LastName = "Watson"},
                new Person{FirstName = "David", LastName = "Alvarez"},
                new Person{FirstName = "Danny", LastName = "Johnes"},
                new Person{FirstName = "Pawel", LastName = "Ivanon"},
                new Person{FirstName = "Henrry", LastName = "Carty"},
                new Person{FirstName = "Nick", LastName = "Morales"},
                new Person{FirstName = "Eric", LastName = "Samaniego"},
                new Person{FirstName = "William", LastName = "Curtis"},
                new Person{FirstName = "James", LastName = "Santos"}
            };
        }

        public ObservableCollection<Person> Items { get; set; }
    }
    // << autocomplete-people-viewmodel
}
