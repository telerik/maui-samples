using System.Collections.ObjectModel;

namespace QSF.Examples.TabViewControl.ConfigurationExample;

public class Location
{
    public string Name { get; set; }
    public ObservableCollection<Customer> Customers { get; set; }
}