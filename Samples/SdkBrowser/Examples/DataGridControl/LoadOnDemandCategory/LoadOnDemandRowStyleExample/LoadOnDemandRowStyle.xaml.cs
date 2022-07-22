using SDKBrowser.Examples.DataGridControl;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadOnDemandRowStyleExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoadOnDemandRowStyle : RadContentView
{
    public LoadOnDemandRowStyle()
    {
        this.InitializeComponent();

        this.BindingContext = new LoadOnDemandRowStyleViewModel();
    }

    // >> datagrid-loadondemandrowstyle-csharp
    private async void dataGrid_LoadOnDemand(object sender, Telerik.Maui.Controls.Compatibility.DataGrid.LoadOnDemandEventArgs e)
    {
        await Task.Delay(3000);
        for (int i = 0; i < 15; i++)
        {
            ((sender as RadDataGrid).ItemsSource as ObservableCollection<Person>).Add(new Person() { Name = "Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female });
        }
        e.IsDataLoaded = true;
    }
    // << datagrid-loadondemandrowstyle-csharp
}