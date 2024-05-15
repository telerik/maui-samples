using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKBrowserMaui.Examples.ComboBoxControl;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.SelectionCategory
{
    // >> collectionview-selection-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        private City selectedItem;
        private int selectedIndex;
        private ObservableCollection<object> selectedItems;

        public ViewModel()
        {
            this.Items = new ObservableCollection<City>
            {
                new City { Name = "Tokyo", Population = 13929286 },
                new City { Name = "New York", Population = 8623000 },
                new City { Name = "London", Population = 8908081 },
                new City { Name = "Madrid", Population = 3223334 },
                new City { Name = "Los Angeles", Population = 4000000},
                new City { Name = "Paris", Population = 2141000 },
                new City { Name = "Beijing", Population = 21540000 },
                new City { Name = "Singapore", Population = 5612000 },
                new City { Name = "New Delhi", Population = 18980000 },
                new City { Name = "Bangkok", Population = 8305218 },
                new City { Name = "Berlin", Population = 3748000 },
            };
        }

        public ObservableCollection<City> Items { get; set; }

        public City SelectedItem 
        {
            get => this.selectedItem;
            set
            {
                if (this.UpdateValue(ref this.selectedItem, value))
                {
                    this.selectedItem = value;
                    App.Current.MainPage.DisplayAlert(" ", $"You have selected {selectedItem.Name} which has a population of {selectedItem.Population}.", "OK");
                }
            }
        }

        public ObservableCollection<object> SelectedItems
        {
            get => this.selectedItems;
            set
            {
                if (this.UpdateValue(ref this.selectedItems, value))
                {
                    this.selectedItems = value;
                    this.selectedItems.Add(this.Items[2]);
                    this.selectedItems.Add(this.Items[4]);
                    this.selectedItems.Add(this.Items[6]);
                }
            }
        }
    }
    // << collectionview-selection-viewmodell
}
