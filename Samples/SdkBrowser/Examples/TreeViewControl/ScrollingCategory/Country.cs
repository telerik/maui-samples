using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SDKBrowserMaui.Examples.TreeViewControl.ScrollingCategory
{
    // >> treeview-country-model
    public class Country : Location
    {
        public Country()
        {
            this.Cities = new ObservableCollection<City>();
        }

        public IList<City> Cities { get; }
    }
    // << treeview-country-model
}
