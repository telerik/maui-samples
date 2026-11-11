using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.RefreshViewControl.IntegrationCategory;

// >> refreshview-integration-viewmodel
public class ViewModel : NotifyPropertyChangedBase
{
    private static readonly string[] Names = { "Andrew Fuller", "Nancy Davolio", "Janet Leverling", "Margaret Peacock", "Steven Buchanan", "Michael Suyama", "Robert King", "Laura Callahan" };
    private static readonly string[] Cities = { "Seattle", "London", "Tacoma", "Kirkland", "Redmond", "Berlin", "Madrid", "Paris" };

    private bool isRefreshing;
    private int nextId = 1;

    public ViewModel()
    {
        this.Items = new ObservableCollection<Customer>();
        for (int i = 0; i < 5; i++)
        {
            this.Items.Add(this.CreateCustomer());
        }

        this.RefreshCommand = new Command(this.ExecuteRefresh);
    }

    public ObservableCollection<Customer> Items { get; }

    public ICommand RefreshCommand { get; }

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set => this.UpdateValue(ref this.isRefreshing, value);
    }

    private async void ExecuteRefresh()
    {
        await Task.Delay(1000);
        for (int i = 0; i < 3; i++)
        {
            this.Items.Insert(0, this.CreateCustomer());
        }

        this.IsRefreshing = false;
    }

    private Customer CreateCustomer()
    {
        int id = this.nextId++;
        return new Customer
        {
            Id = id,
            Name = Names[id % Names.Length],
            City = Cities[(id * 3) % Cities.Length],
            Orders = (id * 7) % 40 + 1,
        };
    }
}
// << refreshview-integration-viewmodel
