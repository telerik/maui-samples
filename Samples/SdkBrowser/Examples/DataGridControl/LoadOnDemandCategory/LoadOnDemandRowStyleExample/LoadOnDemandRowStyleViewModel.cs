﻿using SDKBrowser.Examples.DataGridControl;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataGridControl.LoadOnDemandCategory.LoadOnDemandRowStyleExample;
// >> datagrid-loadondemandrowstyle-viewmodel-csharp
public class LoadOnDemandRowStyleViewModel
{
    public LoadOnDemandRowStyleViewModel()
    {
        this.Items = new ObservableCollection<Person>();

        for (int i = 0; i < 20; i++)
        {
            var person = new Person { Name = "Person " + i, Age = i + 18, Gender = i % 2 == 0 ? Gender.Male : Gender.Female };
            this.Items.Add(person);
        }
    }
    
    public ObservableCollection<Person> Items { get; set; }

}
// << datagrid-loadondemandrowstyle-viewmodel-csharp