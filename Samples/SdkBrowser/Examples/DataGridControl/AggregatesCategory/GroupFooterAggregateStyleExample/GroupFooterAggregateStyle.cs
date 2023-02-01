using Microsoft.Maui.Controls;
using System.Collections.Generic;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls.Compatibility.Common.Data;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.AggregatesCategory.GroupFooterAggregateStyleExample;

public partial class GroupFooterAggregateStyle : ContentView
{
    public GroupFooterAggregateStyle()
    {
        InitializeComponent();

        this.dataGrid.ItemsSource = new List<Data>
        {
            new Data { Name = "Mouse", Price = 30.9, DeliveryPrice = 2, Quantity = 54 },
            new Data { Name = "Mouse", Price = 42.9, DeliveryPrice = 2, Quantity = 32 },
            new Data { Name = "Video Card", Price = 760.7, DeliveryPrice = 3, Quantity = 17 },
            new Data { Name = "Video Card", Price = 1060.7, DeliveryPrice = 3, Quantity = 13 },
            new Data { Name = "Video Card", Price = 920.1, DeliveryPrice = 3, Quantity = 12 },
            new Data { Name = "RAM", Price = 50, DeliveryPrice = 4, Quantity = 126 },
            new Data { Name = "RAM", Price = 120, DeliveryPrice = 1, Quantity = 54 },
            new Data { Name = "RAM", Price = 20, DeliveryPrice = 4, Quantity = 32 },
            new Data { Name = "RAM", Price = 30, DeliveryPrice = 2, Quantity = 67 }
        };

        this.dataGrid.GroupDescriptors.Add(new PropertyGroupDescriptor() { PropertyName = "Name" });
    }
}

// >> datagrid-group-aggregate-style-selector
class CustomGroupFooterStyleSelector : DataGridStyleSelector
{
    public DataGridGroupFooterStyle CustomStyle { get; set; }

    public override DataGridStyle SelectStyle(object item, BindableObject container)
    {
        if (item is GroupFooterContext footerContext
           && footerContext.Group.Key.ToString() == "Video Card"
           && footerContext.Column is DataGridTypedColumn column
           && column.PropertyName == "Quantity")
        {
            return this.CustomStyle;
        }

        return null;
    }
}
// << datagrid-group-aggregate-style-selector