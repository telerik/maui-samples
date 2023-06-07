using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeViewControl.ScrollingCategory
{
    // >> treeview-location-model
    public class Location : NotifyPropertyChangedBase
    {
        private string name;

        public string Name
        {
            get => this.name;
            set => this.UpdateValue(ref this.name, value);
        }
    }
    // << treeview-location-model
}
