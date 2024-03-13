using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SDKBrowserMaui.Examples.DataGridControl.DynamicDataCategory.DynamicObjectExample
{
    // >> datagrid-dynamicobject-viewmodel
    public class ViewModel
    {
        public ViewModel()
        {
            this.Data = new ObservableCollection<MyDynamicObject>();
            for (int i = 0; i < 30; i++)
            {
                var row = new MyDynamicObject() { Id = i };
                for (int j = 0; j < 5; j++)
                {
                    row[string.Format("Column {0}", j)] = string.Format("Cell {0} {1}", i, j);
                }

                this.Data.Add(row);
            }
        }

        public ObservableCollection<MyDynamicObject> Data { get; set; }
    }
    // << datagrid-dynamicobject-viewmodel
}
