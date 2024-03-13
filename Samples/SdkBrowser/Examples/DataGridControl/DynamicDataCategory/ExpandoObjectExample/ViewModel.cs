using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;

namespace SDKBrowserMaui.Examples.DataGridControl.DynamicDataCategory.ExpandoObjectExample
{
    // >> datagrid-expandoobject-viewmodel
    public class ViewModel
    {
        public ViewModel()
        {
            this.Data = new ObservableCollection<ExpandoObject>();
            for (int i = 0; i < 30; i++)
            {
                dynamic item = new ExpandoObject();
                item.Country = "Country " + i;
                item.Capital = "Capital " + i;
                item.Population = i * 10000;

                this.Data.Add(item);
            }
        }

        public ObservableCollection<ExpandoObject> Data { get; set; }
    }
    // << datagrid-expandoobject-viewmodel
}
