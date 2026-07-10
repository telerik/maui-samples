using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class CustomersViewModel : ViewModelBase
{
    private bool isLoaded = false;
    private Customer selectedCustomer;
#if MACCATALYST || WINDOWS
    private CustomerDetailViewModel detailViewModel;
#endif

    public CustomersViewModel()
    {
        this.ItemTapCommand = new Command(this.ItemTapped);

#if MACCATALYST || WINDOWS
        this.DetailViewModel = services.GetService<CustomerDetailViewModel>();
#else
        this.CanCreateNew = true;
        this.CreateNewCommand = new Command(this.CreateNewCommandExecuted);
        this.SearchCommand = new Command(this.SearchCommandExecuted);
#endif
    }

#if MACCATALYST || WINDOWS
    public event EventHandler SearchCompleted;

    public CustomerDetailViewModel DetailViewModel
    {
        get => this.detailViewModel;
        set => this.UpdateValue(ref this.detailViewModel, value);
    }
#endif

    public ObservableCollection<Customer> Customers { get; } = new();

    public Customer SelectedCustomer
    {
        get => this.selectedCustomer;
        set
        {
            if (this.UpdateValue(ref this.selectedCustomer, value))
            {
#if MACCATALYST || WINDOWS
                if (this.detailViewModel != null)
                {
                    this.detailViewModel.SelectedCustomer = this.selectedCustomer;
                }
#endif
            }
        }
    }

    public Command ItemTapCommand { get; set; }

    public override async void OnAppearing()
    {
        await this.LoadCustomersAsync();
    }

    public async Task LoadCustomersAsync()
    {
        if (this.IsBusy || this.isLoaded)
        {
            return;
        }

        try
        {
            if (this.Customers.Count == 0)
            {
                this.IsBusy = true;
                this.IsBusyMessage = "loading customers...";

                var customers = await this.services.GetService<RemoteCustomerService>()?.GetItemsAsync()!;
                if (customers != null)
                {
                    foreach (var customer in customers)
                    {
                        this.Customers.Add(customer);
                    }
                }

                this.SelectedCustomer = this.Customers.FirstOrDefault();

#if MACCATALYST || WINDOWS
                this.detailViewModel.SelectedCustomer = this.SelectedCustomer;
#endif
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading customers, check your network connection and try again. Details: \r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = "";
            this.IsBusy = false;
            this.isLoaded = true;
        }
    }

#if MACCATALYST || WINDOWS
    public void InvokeSearchCompleted()
        => this.SearchCompleted?.Invoke(this, EventArgs.Empty);
#endif

    private async void ItemTapped(object item)
    {
        if (item is Customer customer)
        {
#if MACCATALYST || WINDOWS
            if (this.detailViewModel != null)
            {
                this.detailViewModel.SelectedCustomer = customer;
            }
#else
            var service = this.services.GetService<INavigationService>();
            await service.NavigateToAsync<CustomerDetailViewModel>(customer);
#endif
        }
    }

#if !(MACCATALYST || WINDOWS)
    private async void CreateNewCommandExecuted()
    {
        var service = this.services.GetService<INavigationService>();
        await service.NavigateToAsync<CustomerEditViewModel>();
    }
#endif
}