using Microsoft.Maui.Controls;
using SDKBrowserMaui.Examples.CollectionViewControl.CommandsCategory.GroupTappedCommandExample;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.CollectionViewControl.PullToRefreshCategory.PullToRefreshExample;

// >> collectionview-pull-to-refresh-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private bool isRefreshing;

    public ViewModel()
    {
        this.People = new ObservableCollection<Person>()
        {
            new Person { Name = "Kiko", Age = 23, Department = "Production" },
            new Person { Name = "Jerry", Age = 23, Department = "Accounting and Finance" },
            new Person { Name = "Ethan", Age = 51, Department = "Marketing" },
            new Person { Name = "Isabella", Age = 25, Department = "Marketing" },
            new Person { Name = "Joshua", Age = 45, Department = "Production" },
            new Person { Name = "Logan", Age = 26, Department = "Production"},
            new Person { Name = "Aaron", Age = 32, Department = "Sales" },
            new Person { Name = "Elena", Age = 37, Department = "Accounting and Finance" },
            new Person { Name = "Ross", Age = 30, Department = "Marketing" },
            new Person { Name = "Jane", Age = 45, Department = "Production" },
            new Person { Name = "Luke", Age = 26, Department = "Marketing"},
            new Person { Name = "Tommy", Age = 32, Department = "Sales" },
            new Person { Name = "Juan", Age = 37, Department = "Accounting and Finance" },
            new Person { Name = "Kate", Age = 30, Department = "Marketing" },
            new Person { Name = "William", Age = 23, Department = "Sales" },
            new Person { Name = "Zach", Age = 23, Department = "Accounting and Finance" },
            new Person { Name = "Ivan", Age = 51, Department = "Sales" },
            new Person { Name = "Helena", Age = 25, Department = "Marketing" },
        };

        this.RefreshCommand = new Command(async () => 
        {
            this.IsRefreshing = true;

            await Task.Delay(1000);

            var startIndex = this.People.Count;
            var endIndex = this.People.Count + 10;
            int i = 0;

            for (int itemIndex = startIndex; itemIndex < endIndex; itemIndex++)
            {
                this.People.Insert(i, new Person { Name = "Name " + itemIndex, Age = (itemIndex * 2) + 1, Department = "Department " + itemIndex });
                i++;
            }

            this.IsRefreshing = false;
        });
    }

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set => this.UpdateValue(ref this.isRefreshing, value);
    }

    public ICommand RefreshCommand { get; set; }

    public ObservableCollection<Person> People { get; set; }
}
// << collectionview-pull-to-refresh-viewmodel
