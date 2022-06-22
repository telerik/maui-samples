using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.ListViewControl.LayoutsCategory
{
    // >> listview-layouts-linearlayout-source
    public class ViewModel
    {
        public ViewModel()
        {
            this.Items = new ObservableCollection<Item>();
            for (int i = 0; i < 14; i++)
            {
                var c = 200 - 10 * i;
                this.Items.Add(new Item() { Name = "Item " + i, });
            }
        }
        public ObservableCollection<Item> Items { get; set; }
    }
    // << listview-layouts-linearlayout-source
}
