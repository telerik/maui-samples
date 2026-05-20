using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.SelectionCategory.SelectionExample
{
    // >> segmentcontrol-selection-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        private object selectedItem;
        private int selectedIndex;

        public ViewModel()
        {
            this.Categories = new ObservableCollection<string>() { "Popular", "Library", "Playlists", "Friends" };
            this.SelectedItem = this.Categories[2];
            this.SelectedIndex = 2;
        }

        public ObservableCollection<string> Categories { get; set; }

        public object SelectedItem
        {
            get { return this.selectedItem; }
            set { this.UpdateValue(ref this.selectedItem, value); }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set { this.UpdateValue(ref this.selectedIndex, value); }
        }
    }
    // << segmentcontrol-selection-viewmodel
}

