﻿using SDKBrowser.Examples.DataGridControl;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadOnDemandRowTemplateExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoadOnDemandRowTemplate : RadContentView
{
    public LoadOnDemandRowTemplate()
    {
        this.InitializeComponent();

        this.BindingContext = new LoadOnDemandRowTemplateViewModel();
    }

    // >> datagrid-loadondemandrowtemplate-csharp
    private async void dataGrid_LoadOnDemand(object sender, Telerik.Maui.Controls.DataGrid.LoadOnDemandEventArgs e)
    {
        await Task.Delay(3000);
        for (int i = 0; i < 15; i++)
        {
            ((sender as RadDataGrid).ItemsSource as ObservableCollection<Person>).Add(new Person() { Name = "Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female });
        }
        e.IsDataLoaded = true;
    }
    // << datagrid-loadondemandrowtemplate-csharp
}