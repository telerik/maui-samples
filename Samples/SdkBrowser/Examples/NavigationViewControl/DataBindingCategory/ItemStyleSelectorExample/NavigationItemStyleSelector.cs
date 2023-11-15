using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.NavigationViewControl.DataBindingCategory.ItemStyleSelectorExample;

// >> navigationview-styleselector
public class NavigationItemStyleSelector : IStyleSelector
{
    public Style NormalItemStyle { get; set; }
    public Style UpdatedItemStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        return !string.IsNullOrEmpty(((DataItem)item).Tag) ? UpdatedItemStyle : NormalItemStyle;
    }
}
// << navigationview-styleselector
