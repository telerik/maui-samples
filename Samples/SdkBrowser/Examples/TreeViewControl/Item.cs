using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.TreeViewControl
{
    // >> treeview-getting-started-item
    public class Item
    {
        public Item(string name)
        {
            this.Name = name;
            this.Children = new ObservableCollection<Item>();
        }

        public string Name { get; set; }
        public IList<Item> Children { get; set; }
    }
    // << treeview-getting-started-item
}
