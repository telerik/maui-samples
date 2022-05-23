using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SDKBrowserMaui.Examples.DataGridControl.CommandsCategory.ValidationExample;

// >> datagrid-commands-validation-viewmodel
public class ViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Data> Items { get; set; }
    public ViewModel()
    {
        this.Items = new ObservableCollection<Data>()
        {
                new Data { Country = "India", Capital = "New Delhi"},
                new Data { Country = "South Africa", Capital = "Cape Town"},
                new Data { Country = "Nigeria", Capital = "Abuja" },
                new Data { Country = "Singapore", Capital = "Singapore" }
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
// << datagrid-commands-validation-viewmodel