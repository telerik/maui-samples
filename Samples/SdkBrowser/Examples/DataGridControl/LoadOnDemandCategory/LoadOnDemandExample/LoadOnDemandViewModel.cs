using Microsoft.Maui.Controls;
using SDKBrowser.Examples.DataGridControl;
using System.Collections.Generic;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.Common;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadOnDemandExample;

public class LoadOnDemandViewModel : NotifyPropertyChangedBase
{
    private LoadOnDemandMode loadOnDemandMode;

    public LoadOnDemandViewModel()
    {
        // >> datagrid-loadondemand-collection-csharp
        this.Items = new LoadOnDemandCollection<Person>((cancelationToken) =>
        {
            var list = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                var person = new Person { Name = "LOD Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female };
                list.Add(person);
            }

            return list;
        });
        // << datagrid-loadondemand-collection-csharp

        for (int i = 0; i < 20; i++)
        {
            var person = new Person { Name = "Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female };
            this.Items.Add(person);
        }

        this.LoadOnDemandChangeModeCommand = new Command(this.OnLoadOnDemandChangeModeCommandExecuted);
    }
    
    public ICommand LoadOnDemandChangeModeCommand { get; set; }
    // >> datagrid-loadondemand-collection-property-csharp
    public LoadOnDemandCollection<Person> Items { get; set; }

    // << datagrid-loadondemand-collection-property-csharp

    public LoadOnDemandMode LoadOnDemandMode
    {
        get
        {
            return this.loadOnDemandMode;
        }
        set
        {
            if (this.loadOnDemandMode != value)
            {
                this.loadOnDemandMode = value;
                this.OnPropertyChanged("LoadOnDemandMode");
            }
        }
    }

    private void OnLoadOnDemandChangeModeCommandExecuted(object obj)
    {
        this.LoadOnDemandMode = this.LoadOnDemandMode == LoadOnDemandMode.Manual ? LoadOnDemandMode.Automatic : LoadOnDemandMode.Manual;
    }
}