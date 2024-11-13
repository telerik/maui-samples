using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.StyleSelectorExample;

// >> datagrid-styleselector-celldecoration
class MyCellDecorationSelector : IStyleSelector
{
    public Style CellTemplate1 { get; set; }
    public Style CellTemplate2 { get; set; }

    public Style SelectStyle(object item, BindableObject container)
    {
        DataGridCellInfo cellInfo = item as DataGridCellInfo;
        if (cellInfo != null)
        {
            if ((cellInfo.Item as Data).Capital == "Singapore")
            {
                return CellTemplate1;
            }
            else
            {
                return CellTemplate2;
            }
        }

        return null;
    }
}
// << datagrid-styleselector-celldecoration