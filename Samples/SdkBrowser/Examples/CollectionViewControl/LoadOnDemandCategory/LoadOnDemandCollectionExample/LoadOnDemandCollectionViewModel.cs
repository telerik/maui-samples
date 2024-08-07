using Microsoft.Maui.Controls;
using SDKBrowser.Examples.DataGridControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.LoadOnDemandCategory.LoadOnDemandCollectionExample;

// >> collectionview-loadondemand-collection-viewmodel
public class LoadOnDemandCollectionViewModel : NotifyPropertyChangedBase
{
    public LoadOnDemandCollectionViewModel()
    {
        this.Items = new LoadOnDemandCollection<Person>(this.LoadMoreItems);

        for (int i = 0; i < 20; i++)
        {
            this.Items.Add(new Person { Name = "Person " + i, Age = i + 10 });
        }
    }

    public LoadOnDemandCollection<Person> Items { get; set; }
    private IEnumerable LoadMoreItems(CancellationToken cancellationToken)
    {
        List<Person> newItems = new List<Person>();
        int count = this.Items.Count;

        try
        {
            Thread.Sleep(2500);

            for (int i = 0; i < 10; i++)
            {
                newItems.Add(new Person { Name = $"Loaded item: Person {count + i}", Age = count + i + 10 });
            }

            return newItems;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
// << collectionview-loadondemand-collection-viewmodel