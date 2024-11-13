using Microsoft.Maui.Controls;
using QSF.Examples.DataGridControl.Common;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.DataGrid;

namespace QSF.Examples.DataGridControl.CustomizationExample;

internal class GroupStyleSelector : IStyleSelector
{
    public Style CountryStyleTemplate { get; set; }
    public Style RegionStyleTemplate { get; set; }

    public Style SelectStyle(object item, BindableObject container)
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
