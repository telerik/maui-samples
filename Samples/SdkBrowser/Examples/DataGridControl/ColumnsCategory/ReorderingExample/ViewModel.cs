using Telerik.Maui.Controls;
using System.Collections.ObjectModel;
using SDKBrowser.Examples.DataGridControl;

namespace SDKBrowserMaui.Examples.DataGridControl.ColumnsCategory.ReorderingExample;
// >> datagrid-reordering-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private bool isReorderingEnabled;

    public ViewModel()
    {
        this.Data = new ObservableCollection<PersonDetails>
        {
            new PersonDetails { Name = "Juan", Age = 21, Gender = Gender.Male, Weight = 94.2 },
            new PersonDetails { Name = "Larry", Age = 22, Gender = Gender.Male, Weight = 68.9 },
            new PersonDetails { Name = "Tiffany", Age = 34, Gender = Gender.Female, Weight = 83 },
            new PersonDetails { Name = "Sebastian", Age = 16, Gender = Gender.Other, Weight = 72 },
            new PersonDetails { Name = "Bojidara", Age = 42, Gender = Gender.Female, Weight = 52 },
        };
    }

    public ObservableCollection<PersonDetails> Data { get; set; }

    public bool IsReorderingEnabled
    {
        get => this.isReorderingEnabled;
        set => this.UpdateValue(ref this.isReorderingEnabled, value);
    }
}
// << datagrid-reordering-viewmodel