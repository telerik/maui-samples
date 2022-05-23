using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataGridControl.NestedPropertyCategory.NestedPropertyExample;

// >> datagrid-nested-property-viewmodel
public class ViewModel
{
    public ObservableCollection<Person> Persons { get; set; }

    public ViewModel()
    {
        var source = new ObservableCollection<Person>();

        source.Add(new Person() { Name = "Alejandro Gonzalez ", Age = 23, Address = new Address() { City = "Madrid" } });
        source.Add(new Person() { Name = "John Smith", Age = 31, Address = new Address() { City = "London" } });
        source.Add(new Person() { Name = "Emily Jakinson", Age = 42, Address = new Address() { City = "New York" } });
        source.Add(new Person() { Name = "Amelia Johnson", Age = 19, Address = new Address() { City = "Bath" } });
        source.Add(new Person() { Name = "Jack Connor", Age = 28, Address = new Address() { City = "Oxford" } });
        source.Add(new Person() { Name = "Thomas Ford", Age = 36, Address = new Address() { City = "Atlanta" } });
        source.Add(new Person() { Name = "James Williams", Age = 25, Address = new Address() { City = "Houston" } });
        source.Add(new Person() { Name = "Nikole Smith", Age = 38, Address = new Address() { City = "Chicago" } });

        this.Persons = source;
    }
}
// << datagrid-nested-property-viewmodel
