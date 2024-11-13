using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.StylingCategory.StyleSelectorExample;

// >> datagrid-styleselector-group
class MyGroupSelector : IStyleSelector
{
    public Style CountryTemplate1 { get; set; }
    public Style CountryTemplate2 { get; set; }

    public Style SelectStyle(object item, BindableObject container)
    {
        GroupHeaderContext header = item as GroupHeaderContext;
        if (header != null)
        { 
            if (header.Group.Key == "India")
            {
                return CountryTemplate1;
            }
            else
            {
                return CountryTemplate2;
            }
        }
        return null;
    }
}
// << datagrid-styleselector-group