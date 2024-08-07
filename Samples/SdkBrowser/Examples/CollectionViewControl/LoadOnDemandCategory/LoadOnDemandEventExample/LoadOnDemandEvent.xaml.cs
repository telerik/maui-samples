using Microsoft.Maui.Controls;
using SDKBrowser.Examples.DataGridControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Examples.CollectionViewControl.LoadOnDemandCategory.LoadOnDemandEventExample;

public partial class LoadOnDemandEvent : ContentView
{
    private ObservableCollection<Person> items;

    public LoadOnDemandEvent()
    {
        InitializeComponent();

        // >> collectionview-loadondemand-event-data
        this.items = new ObservableCollection<Person>();

        for (int i = 0; i < 20; i++)
        {
            this.items.Add(new Person { Name = "Person " + i, Age = i + 10 });
        }

        this.collectionView.ItemsSource = this.items;
        // << collectionview-loadondemand-event-data
    }
    // >> collectionview-loadondemand-event-implementation
    private async void OnLoadOnDemand(object sender, System.EventArgs e)
    {
        this.collectionView.IsLoadOnDemandActive = true;

        IEnumerable<Person> newItems = await this.GetNewItems();
        foreach (var newItem in newItems)
        {
            this.items.Add(newItem);
        }

        this.collectionView.IsLoadOnDemandActive = false;
    }

    private async Task<IEnumerable<Person>> GetNewItems()
    {
        List<Person> newItems = new List<Person>();
        int count = this.items.Count;

        try
        {
            await Task.Delay(2000);

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
    // << collectionview-loadondemand-event-implementation
}