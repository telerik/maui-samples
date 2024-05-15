using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TabViewControl.StylingCategory.HeaderItemStyleSelectorExample;

// >> tabview-header-styleselector-class -->
public class MyCustomHeaderItemStyleSelector : IStyleSelector
{
    public Style HomeStyle { get; set; }
    public Style AccountStyle { get; set; }
    public Style FilesStyle { get; set; }

    public Style SelectStyle(object item, BindableObject bindable)
    {
        TabViewItem tabViewItem = (TabViewItem)item;

        if (tabViewItem.HeaderText.Contains("Home"))
        {
            return this.HomeStyle;
        }
        else if (tabViewItem.HeaderText.Contains("Account"))
        {
            return this.AccountStyle;
        }
        else if (tabViewItem.HeaderText.Contains("Files"))
        {
            return this.FilesStyle;
        }
        else
        {
            return null;
        }
    }
}
// << tabview-header-styleselector-class -->
