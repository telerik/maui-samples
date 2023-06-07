using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.FrozenColumnsExample;

public class FrozenColumnsViewModel : ConfigurationExampleViewModel
{
    public FrozenColumnsViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
    }
    
    public ObservableCollection<Order> Orders { get; private set; }

    public DataGridColumnCollection Columns { get; set; }
}