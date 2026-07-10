using System.Collections.ObjectModel;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.HideColumnHeadersExample;

// >> datagrid-hide-column-headers-viewmodel
public class HideColumnHeadersViewModel : NotifyPropertyChangedBase
{
    private ObservableCollection<SampleData> items;

    public HideColumnHeadersViewModel()
    {
        this.Items = this.CreateSampleData();
    }

    public ObservableCollection<SampleData> Items
    {
        get => this.items;
        set => this.UpdateValue(ref this.items, value);
    }

    private ObservableCollection<SampleData> CreateSampleData()
    {
        return new ObservableCollection<SampleData>
        {
            new SampleData { Name = "John", Age = 30, City = "New York" },
            new SampleData { Name = "Jane", Age = 25, City = "Los Angeles" },
            new SampleData { Name = "Bob", Age = 35, City = "Chicago" },
            new SampleData { Name = "Alice", Age = 28, City = "Houston" },
            new SampleData { Name = "Charlie", Age = 32, City = "Phoenix" }
        };
    }
}
// << datagrid-hide-column-headers-viewmodel
