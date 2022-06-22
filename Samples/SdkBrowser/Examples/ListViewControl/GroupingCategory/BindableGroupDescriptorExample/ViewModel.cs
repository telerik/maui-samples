using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView;

namespace SDKBrowserMaui.Examples.ListViewControl.GroupingCategory.BindableGroupDescriptorExample
{
    // >> listview-features-bindable-groupdescriptor-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<GroupDescriptorBase> groupDescriptors;
        private bool isPropertyNameGroupSwitchToggled;
        private bool isSortOrderGroupSwitchToggled;
        private List<Event> items;

        public ViewModel()
        {
            this.Items = this.GetItems();
            this.groupDescriptors = new ObservableCollection<GroupDescriptorBase>();
        }
     
        public ObservableCollection<GroupDescriptorBase> GroupDescriptors
        {
            get { return this.groupDescriptors; }
            set { this.UpdateValue(ref this.groupDescriptors, value); }

        }
    
        public bool IsPropertyNameGroupSwitchToggled
        {
            get { return this.isPropertyNameGroupSwitchToggled; }
            set
            {
                 if (this.UpdateValue(ref this.isPropertyNameGroupSwitchToggled, value))
                 {
                    this.UpdateExistingGroupDescriptor(nameof(IsPropertyNameGroupSwitchToggled));
                 }
            }
        }

        public bool IsSortOrderGroupSwitchToggled
        {
            get { return this.isSortOrderGroupSwitchToggled; }
            set
            {
                if (this.UpdateValue(ref this.isSortOrderGroupSwitchToggled, value))
                {
                    this.UpdateExistingGroupDescriptor(nameof(IsSortOrderGroupSwitchToggled));
                }
            }
        }

        public List<Event> Items
        {
            get { return this.items; }
            set { this.UpdateValue(ref this.items, value); }

        }

        private List<Event> GetItems()
        {
            var results = new List<Event>();

            results.Add(new Event() { Content = "Content of the item", Day = "Tommorow", Category = "A" });
            results.Add(new Event() { Content = "This also happens today", Day = "Yesterday", Category = "A" });
            results.Add(new Event() { Content = "More events today", Day = "Today", Category = "A" });
            results.Add(new Event() { Content = "Go shopping after 19:00", Day = "Yesterday", Category = "B" });
            results.Add(new Event() { Content = "You are now free to do whathever", Day = "Today", Category = "B" });

            results.Add(new Event() { Content = "For tommorow", Day = "Today", Category = "B" });
            results.Add(new Event() { Content = "It is a free day", Day = "Yesterday", Category = "C" });
            results.Add(new Event() { Content = "Go have some fun", Day = "Tommorow", Category = "C" });
            results.Add(new Event() { Content = "Party", Day = "Tommorow", Category = "C" });

            return results;
        }

        private void UpdateExistingGroupDescriptor(string propertyToUpdate)
        {
            if (this.GroupDescriptors == null)
                return;

            if (this.GroupDescriptors.Count == 0)
            {
                this.GroupDescriptors.Add(new ListViewPropertyGroupDescriptor()
                {
                    PropertyName = "Day",
                    SortOrder = SortOrder.Ascending
                });
            }

            if (propertyToUpdate.Equals(nameof(IsSortOrderGroupSwitchToggled)))
            {
                var descriptor = (ListViewPropertyGroupDescriptor)this.GroupDescriptors.FirstOrDefault();
                descriptor.SortOrder = isSortOrderGroupSwitchToggled ? SortOrder.Descending : SortOrder.Ascending;
            }
            else if (propertyToUpdate.Equals(nameof(IsPropertyNameGroupSwitchToggled)))
            {
                var descriptor = (ListViewPropertyGroupDescriptor)this.GroupDescriptors.FirstOrDefault();
                descriptor.PropertyName = isPropertyNameGroupSwitchToggled ? "Category" : "Day";
            }
        }
    }
    // << listview-features-bindable-groupdescriptor-viewmodel
}
