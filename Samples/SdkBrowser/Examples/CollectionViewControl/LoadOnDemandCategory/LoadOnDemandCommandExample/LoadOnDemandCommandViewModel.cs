using Microsoft.Maui.Controls;
using SDKBrowser.Examples.DataGridControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.LoadOnDemandCategory.LoadOnDemandCommandExample;

// >> collectionview-loadondemand-command-viewmodel
public class LoadOnDemandCommandViewModel : NotifyPropertyChangedBase
{
    private bool isLoadOnDemandActive;

    public LoadOnDemandCommandViewModel()
    {
        this.Items = new ObservableCollection<Person>();

        for (int i = 0; i < 20; i++)
        {
            this.Items.Add(new Person { Name = "Person " + i, Age = i + 10 });
        }

        this.LoadOnDemandCommand = new Command(async () =>
        {
            this.IsLoadOnDemandActive = true;

            IEnumerable<Person> newItems = await this.GetNewItems();
            foreach (var newItem in newItems)
            {
                this.Items.Add(newItem);
            }

            this.IsLoadOnDemandActive = false;
        });
    }

    public ObservableCollection<Person> Items { get; set; }

    public ICommand LoadOnDemandCommand { get; set; }

    public bool IsLoadOnDemandActive
    {
        get => this.isLoadOnDemandActive;
        set => this.UpdateValue(ref this.isLoadOnDemandActive, value);
    }

    private async Task<IEnumerable<Person>> GetNewItems()
    {
        List<Person> newItems = new List<Person>();
        int count = this.Items.Count;

        try
        {
            await Task.Delay(2000);

            for (int i = 0; i < 10; i++)
            {
                newItems.Add(new Person { Name = $"Loaded item: Person {count + i}", Age = i + 10 });
            }

            return newItems;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
// << collectionview-loadondemand-command-viewmodel
