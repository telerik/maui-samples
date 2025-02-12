using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls.DataGrid;

namespace QSF.Examples.DataGridControl.FrozenColumnsExample;

public class FrozenColumnsViewModel : ConfigurationExampleViewModel
{
    private bool areGroupHeadersClippedWhenFrozen = true;
    public FrozenColumnsViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
    }
    
    public ObservableCollection<Order> Orders { get; private set; }

    public DataGridColumnCollection Columns { get; set; }

    public bool AreGroupHeadersClippedWhenFrozen
    {
        get => this.areGroupHeadersClippedWhenFrozen;
        set => this.UpdateValue(ref this.areGroupHeadersClippedWhenFrozen, value);
    }
}