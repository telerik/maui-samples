using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Telerik.Maui.Controls.Compatibility.Common.Data;

namespace SDKBrowserMaui.Examples.DataGridControl.AggregatesCategory.DelegateAggregateDescriptorsExample;

public partial class DelegateAggregateDescriptors : ContentView
{
    public DelegateAggregateDescriptors()
    {
        InitializeComponent();

        this.dataGrid.ItemsSource = new List<Data>
        {
            new Data { Name = "KeyBoard", Price = 24.6, DeliveryPrice = 2, Quantity = 32 },
            new Data { Name = "Mouse", Price = 30.9, DeliveryPrice = 2, Quantity = 54 },
            new Data { Name = "Video Card", Price = 760.7, DeliveryPrice = 3, Quantity = 17 },
            new Data { Name = "Motherboard", Price = 210.4, DeliveryPrice = 4, Quantity = 12 },
            new Data { Name = "SSD", Price = 42.9, DeliveryPrice = 3, Quantity = 88 },
            new Data { Name = "RAM", Price = 50, DeliveryPrice = 4, Quantity = 126 }
        };
    }
}

// >> datagrid-delegate-aggregate-key
public class SumIfKeyLookUp : IKeyLookup
{
    public object GetKey(object instance) => ((Data)instance).Price; 
}
// << datagrid-delegate-aggregate-key

// >> datagrid-delegate-aggregate-function
public class SumIfAggregateFunction : IAggregateFunction
{
    private double value;
    public double GreaterThanValue { get; set; }

    public object GetValue() => $"SumIf (Price > {this.GreaterThanValue}): " + string.Format("{0:C}", this.value);

    public IAggregateFunction Clone() => new SumIfAggregateFunction() { GreaterThanValue = this.GreaterThanValue };

    public void Accumulate(object value)
    {
        var price = (double)value;
        if (price > this.GreaterThanValue)
        {
            this.value += price;
        }
    }

    public void Merge(IAggregateFunction aggregateFunction)
    {
        var myFunction = aggregateFunction as SumIfAggregateFunction;
        if (myFunction != null)
        {
            this.value += myFunction.value;
        }
    }
}
// << datagrid-delegate-aggregate-function