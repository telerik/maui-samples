using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TreeViewControl.LoadChildrenOnDemandCategory.LoadChildrenOnDemandCommandExample
{
    // >> treeview-load-command-datamodel
    public class Category : NotifyPropertyChangedBase
    {
        public string Name { get; set; }

        ObservableCollection<string> children;
        public ObservableCollection<string> Children
        {
            get
            {
                return this.children;
            }
            set
            {
                if (this.children != value)
                {
                    this.children = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
    // << treeview-load-command-datamodel
}
