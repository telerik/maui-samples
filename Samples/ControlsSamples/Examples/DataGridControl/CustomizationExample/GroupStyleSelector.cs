using Microsoft.Maui.Controls;
using QSF.Examples.DataGridControl.Common;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace QSF.Examples.DataGridControl.CustomizationExample;

internal class GroupStyleSelector : DataGridStyleSelector
{
    public DataGridGroupHeaderStyle CountryStyleTemplate { get; set; }
    public DataGridGroupHeaderStyle RegionStyleTemplate { get; set; }
    public override DataGridStyle SelectStyle(object item, BindableObject container)
    {
        GroupHeaderContext header = item as GroupHeaderContext;
        string stringContent = (string)header.Descriptor.DisplayContent;
        
        switch (stringContent)
        {
            case nameof(SalesPerson.CountryName):
                return CountryStyleTemplate;
            case nameof(SalesPerson.RegionName):
                return RegionStyleTemplate;
            default:
                return null;
        }
    }
}
