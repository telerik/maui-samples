using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;
using SDKBrowserMaui.Examples.TreeViewControl.ScrollingCategory;

namespace SDKBrowserMaui.Examples.TreeViewControl.StylingCategory
{
    // >> treeview-styleselector
    public class LocationsStyleSelector : IStyleSelector
    {
        public Style CityStyle { get; set; }

        public Style CountryStyle { get; set; }

        public Style SelectStyle(object item, BindableObject bindable)
        {
            return item switch
            {
                City => this.CityStyle,
                Country => this.CountryStyle,
                _ => null
            };
        }
    }
    // << treeview-styleselector
}
