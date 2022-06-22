using SDKBrowser.Examples.DataGridControl;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadOnDemandEventExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoadOnDemandEvent : RadContentView
{
    public LoadOnDemandEvent()
    {
        this.InitializeComponent();

        this.BindingContext = new LoadOnDemandEventViewModel();
    }

    // >> datagrid-loadondemand-event-csharp
    private void dataGrid_LoadOnDemand(object sender, Telerik.Maui.Controls.Compatibility.DataGrid.LoadOnDemandEventArgs e)
    {
        for (int i = 0; i < 15; i++)
        {
            ((sender as RadDataGrid).ItemsSource as ObservableCollection<Person>).Add(new Person() { Name = "Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female });
        }
        e.IsDataLoaded = true;
    }
    // << datagrid-loadondemand-event-csharp
}