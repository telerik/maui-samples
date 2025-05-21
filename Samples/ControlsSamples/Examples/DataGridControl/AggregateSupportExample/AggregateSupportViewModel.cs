using QSF.Examples.DataGridControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Maui.Controls.DataGrid;

namespace QSF.Examples.DataGridControl.AggregateSupportExample;

public class AggregateSupportViewModel : ConfigurationExampleViewModel
{
    private DataGridGroupAggregatesAlignment groupAggregatesAlignment = DataGridGroupAggregatesAlignment.NextToHeader;


    public AggregateSupportViewModel()
    {
        this.Orders = DataGenerator.GetItems<ObservableCollection<Order>>(ResourcePaths.OrdersPath);
    }

    public ObservableCollection<Order> Orders { get; private set; }

    public IEnumerable<DataGridGroupAggregatesAlignment> GroupAggregatesAlignments { get; } = Enum.GetValues(typeof(DataGridGroupAggregatesAlignment)).Cast<DataGridGroupAggregatesAlignment>();

    public DataGridGroupAggregatesAlignment GroupAggregatesAlignment
    {
        get => this.groupAggregatesAlignment;
        set => this.UpdateValue(ref this.groupAggregatesAlignment, value);
    }
}
