using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TreeViewControl.EventsCategory
{
    // >> treeview-events-data
    public class Data
    {
        public Data(string name)
        {
            this.Name = name;
            this.Children = new ObservableCollection<Data>();
        }

        public string Name { get; set; }
        public IList<Data> Children { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
    // << treeview-events-data
}
