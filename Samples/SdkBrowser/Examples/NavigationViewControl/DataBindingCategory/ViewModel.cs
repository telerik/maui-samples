using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.NavigationViewControl.DataBindingCategory;

// >> navigationview-databinding-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private readonly ObservableCollection<DataItem> navigationItems;
    private object selectedItem;

    public ViewModel()
    {
        this.navigationItems = new ObservableCollection<DataItem>
        {
            new DataItem
            {
                Text = "Inbox",
                Icon = "\ue8a2",
                Tag = "5"
            },
            new DataItem
            {
                Text = "Draft",
                Icon = "\ue870"
            },
             new DataItem
            {
                Text = "Archive",
                Icon = "\ue826"
            },
            new DataItem
            {
                Text = "Sent",
                Icon = "\ue82d"
            },
            new DataItem
            {
                Text = "Spam",
                Icon = "\ue82e",
                Tag = "99+"
            },
            new DataItem
            {
                Text = "Deleted",
                Icon = "\ue827"
            },
        };
    }

    public IList<DataItem> Items => this.navigationItems;
}
// << navigationview-databinding-viewmodel
