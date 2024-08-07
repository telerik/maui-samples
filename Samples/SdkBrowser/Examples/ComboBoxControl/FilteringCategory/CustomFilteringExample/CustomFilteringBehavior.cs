using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.ComboBox;

namespace SDKBrowserMaui.Examples.ComboBoxControl.FilteringCategory.CustomFilteringExample;

// >> combobox-filtering-customfilterbehavior
public class CustomFilteringBehavior : ComboBoxFilteringBehavior
{
    public override IList<object> FilterItems(string searchText, SearchMode searchMode, string path, IEnumerable source)
    {
        return source.OfType<City>().Where(city => city.Name.Contains(searchText, System.StringComparison.CurrentCultureIgnoreCase) &&
                                          (city.Population > 5000000)).ToList<object>();
    }
}
// << combobox-filtering-customfilterbehavior