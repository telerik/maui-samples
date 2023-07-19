using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;

namespace QSF.Examples.DataGridControl.HeaderContentTemplateExample;

public class HeaderContentTemplateViewModel : ExampleViewModel
{
    public HeaderContentTemplateViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
    }

    public ObservableCollection<Order> Orders { get; private set; }
}