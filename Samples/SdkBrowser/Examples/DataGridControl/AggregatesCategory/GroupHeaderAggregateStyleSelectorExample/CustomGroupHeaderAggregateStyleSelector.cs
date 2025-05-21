using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.AggregatesCategory.GroupHeaderAggregateStyleSelectorExample;

// >> datagrid-aggregates-styleselector
public class CustomGroupHeaderAggregateStyleSelector : IStyleSelector
{
    public Style NumericalColumnStyle { get; set; }
    public Style OtherColumnStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        var context = item as GroupAggregateCellContext;
        var column = context?.Column;
        if (column != null && column.GetType() == typeof(DataGridNumericalColumn))
        {
            return this.NumericalColumnStyle;
        }

        return this.OtherColumnStyle;
    }
}
// << datagrid-aggregates-styleselector
