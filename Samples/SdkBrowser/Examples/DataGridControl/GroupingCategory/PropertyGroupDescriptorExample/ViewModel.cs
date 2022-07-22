using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.GroupingCategory.PropertyGroupDescriptorExample;

// >> datagrid-grouping-propertygroupdescriptor-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private DataGridCellInfo cell;

    public ViewModel()
    {
        this.People = new ObservableCollection<Person>()
        {
            new Person { Name = "Kiko", Age = 23, Department = "Production" },
            new Person { Name = "Jerry", Age = 23, Department = "Accounting and Finance"},
            new Person { Name = "Ethan", Age = 51, Department = "Marketing" },
            new Person { Name = "Isabella", Age = 25, Department = "Marketing" },
            new Person { Name = "Joshua", Age = 45, Department = "Production" },
            new Person { Name = "Logan", Age = 26, Department = "Production"},
            new Person { Name = "Aaron", Age = 32, Department = "Production" },
            new Person { Name = "Elena", Age = 37, Department = "Accounting and Finance"},
            new Person { Name = "Ross", Age = 30, Department = "Marketing" },
        };
    }

    public DataGridCellInfo Cell
    {
        get => this.cell;
        set
        {
            if (this.cell != value)
            {
                this.cell = value;
                this.OnPropertyChanged();
            }
        }
    }
    public ObservableCollection<Person> People { get; set; }
}
// << datagrid-grouping-propertygroupdescriptor-viewmodel