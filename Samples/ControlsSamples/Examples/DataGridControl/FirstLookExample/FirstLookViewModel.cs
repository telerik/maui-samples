using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    public ObservableCollection<Order> OrderDetails { get; private set; }
    public ObservableCollection<SalesPerson> People { get; private set; }

    public FirstLookViewModel()
    {
        this.OrderDetails = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        this.People = DataGenerator.GetItems<ObservableCollection<SalesPerson>>(ResourcePaths.PeoplePath);
    }
}
