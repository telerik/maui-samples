using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ComboBoxControl.SelectionCategory.MultipleSelectionExample;

// >> combobox-multipleselection-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private ObservableCollection<object> selectedCities;

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

    public ObservableCollection<object> SelectedCities
    {
        get
        {
            return this.selectedCities;
        }
        set
        {
            if (this.selectedCities != value)
            {
                this.selectedCities = value;

                this.selectedCities.Add(this.Items[0]);
                this.selectedCities.Add(this.Items[1]);

                this.OnPropertyChanged();
            }
        }
    }
}
// << combobox-multipleselection-viewmodel