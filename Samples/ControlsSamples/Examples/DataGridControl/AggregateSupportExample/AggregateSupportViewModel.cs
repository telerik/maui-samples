using QSF.ViewModels;
using QSF.Examples.DataGridControl.Common;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.AggregateSupportExample;

public class AggregateSupportViewModel : ExampleViewModel
{
    public ObservableCollection<Order> Orders { get; private set; }

    public AggregateSupportViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
    }
}
