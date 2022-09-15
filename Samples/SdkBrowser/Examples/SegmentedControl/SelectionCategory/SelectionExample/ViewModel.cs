using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.SegmentedControl.SelectionCategory.SelectionExample
{
    // >> segmentcontrol-selection-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        private string selectedItem;

        public ViewModel()
        {
            this.Categories = new ObservableCollection<string>() { "Popular", "Library", "Playlists", "Friends" };
            this.SelectedItem = this.Categories[2];
        }

        public ObservableCollection<string> Categories { get; set; }

        public string SelectedItem
        {
            get { return this.selectedItem; }
            set { this.UpdateValue(ref this.selectedItem, value); }
        }
    }
    // << segmentcontrol-selection-viewmodel
}

