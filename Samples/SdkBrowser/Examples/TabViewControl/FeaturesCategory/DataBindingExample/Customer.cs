using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TabViewControl.FeaturesCategory.DataBindingExample;

// >> tabview-databinding-datamodel
public class Customer : Location
{
    public string Name { get; set; }
    public int Number { get; set; }
}

public class Location
{
    public string Name { get; set; }
    public ObservableCollection<Customer> Customers { get; set; }
}
// << tabview-databinding-datamodel
