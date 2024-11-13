using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace QSF.Examples.DataGridControl.ThemingExample;

public class ThemingViewModel : ExampleViewModel
{
    private Order selectedOrder;

    public ThemingViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
        this.SelectedOrder = this.Orders.FirstOrDefault();
    }

    public ObservableCollection<Order> Orders { get; private set; }

    public Order SelectedOrder
    {
        get => this.selectedOrder;
        set { this.UpdateValue(ref this.selectedOrder, value); }
    }
}