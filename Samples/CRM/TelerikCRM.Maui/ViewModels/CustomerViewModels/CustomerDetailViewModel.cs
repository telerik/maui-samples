using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class CustomerDetailViewModel : ViewModelBase
{
#if MACCATALYST || WINDOWS
    private OrderEditViewModel orderViewModel;
    private CustomerEditViewModel editViewModel;
    private bool isOrderPopupOpen;
    private bool isEditPopupOpen;
#endif
    private bool isRefreshing;
    private Customer selectedCustomer;
    private ObservableCollection<Order> selectedCustomerOrders;
    private Customer currentCustomer;
    private Order currentOrder;

    public CustomerDetailViewModel(Customer selectedCustomer)
        : this()
    {
        this.SelectedCustomer = selectedCustomer;
    }

    public CustomerDetailViewModel()
    {
        this.RefreshRequestedCommand = new Command(this.RefreshRequested);
        this.ItemTapCommand = new Command(this.ItemTapped);
        this.ToolbarCommand = new Command(this.ToolbarItemTapped);

#if MACCATALYST || WINDOWS
        this.CreateNewCommand = new Command(this.CreateNewCommandExecuted);
        this.SaveModalCommand = new Command(this.SaveModalCommandExecuted);
        this.CloseModalCommand = new Command(this.CloseModalCommandExecuted);
        this.EditViewModel = this.services.GetService(typeof(CustomerEditViewModel)) as CustomerEditViewModel;
        this.OrderViewModel = this.services.GetService(typeof(OrderEditViewModel)) as OrderEditViewModel;
#else
        this.CanNavigateBack = true;
        this.NavigateBackContextName = "Customers";
#endif
    }

#if MACCATALYST || WINDOWS
    public OrderEditViewModel OrderViewModel
    {
        get => this.orderViewModel;
        set
        {
            if (this.UpdateValue(ref this.orderViewModel, value) && this.orderViewModel != null)
            {
                this.orderViewModel.LoadData();
            }
        }
    }


    public CustomerEditViewModel EditViewModel
    {
        get => this.editViewModel;
        set => this.UpdateValue(ref this.editViewModel, value);
    }

    public bool IsEditPopupOpen
    {
        get => this.isEditPopupOpen;
        set => this.UpdateValue(ref this.isEditPopupOpen, value);
    }

    public bool IsOrderPopupOpen
    {
        get => this.isOrderPopupOpen;
        set => this.UpdateValue(ref this.isOrderPopupOpen, value);
    }
#endif

    public bool IsRefreshing
    {
        get => this.isRefreshing;
        set => this.UpdateValue(ref this.isRefreshing, value);
    }

    public Customer SelectedCustomer
    {
        get => this.selectedCustomer;
        set
        {
            if (this.UpdateValue(ref this.selectedCustomer, value))
            {
                this.LoadSelectedCustomerOrdersAsync();
                this.Title = this.SelectedCustomer?.Name;
            }
        }
    }

    public ObservableCollection<Order> SelectedCustomerOrders
    {
        get => this.selectedCustomerOrders;
        set => this.UpdateValue(ref this.selectedCustomerOrders, value);
    }

    public Command ItemTapCommand { get; set; }

    public Command RefreshRequestedCommand { get; set; }

    public Command ToolbarCommand { get; set; }

#if MACCATALYST || WINDOWS
    public Command SaveModalCommand { get; set; }

    public Command CloseModalCommand { get; set; }
#endif

    public async Task LoadSelectedCustomerOrdersAsync()
    {
        if (this.SelectedCustomer == null)
        {
            return;
        }

        try
        {
            this.IsBusy = true;
            this.IsBusyMessage = "loading orders...";

            var orders = await this.services.GetService<RemoteOrderService>()?.GetItemsAsync()!;
            if (orders != null)
            {
                this.SelectedCustomerOrders = new ObservableCollection<Order>(orders.Where(o => o.CustomerId == this.SelectedCustomer.Id));
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading {this.SelectedCustomer.Name}'s orders. Details:\r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = string.Empty;
            this.IsBusy = false;
        }
    }

    private async void RefreshRequested()
    {
        await this.LoadSelectedCustomerOrdersAsync();
        this.IsRefreshing = false;
    }

    private async void ItemTapped(object item)
    {
        if (item is Order order)
        {
            this.currentOrder = order;
            var editedOrder = this.currentOrder.Copy();

#if MACCATALYST || WINDOWS
            this.orderViewModel.SelectedOrder = editedOrder;
            this.orderViewModel.SelectedDeliveryService = editedOrder?.DeliveryService;
            await this.orderViewModel.LoadData();

            this.Title = "Edit Order";
            this.DeleteContextName = "Order";

            this.IsOrderPopupOpen = true;
#else
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<OrderDetailViewModel>(editedOrder);
#endif
        }
    }

#if MACCATALYST || WINDOWS
    private async void SaveModalCommandExecuted()
    {
        if (this.IsEditPopupOpen)
        {
            this.editViewModel?.SaveCommand?.Execute(null);

            // NOTE: Commented code is applicable for when app is not in read-only mode
            // this.IsEditPopupOpen = false;
            // this.currentCustomer?.CopyFrom(this.editViewModel.SelectedCustomer);

            await this.editViewModel?.UpdateDatabaseAsync();
            this.currentCustomer = null;
        }
        else if (this.IsOrderPopupOpen)
        {
            this.orderViewModel?.SaveCommand?.Execute(null);

            // NOTE: Commented code is applicable for when app is not in read-only mode
            // this.IsOrderPopupOpen = false;
            // this.currentOrder?.CopyFrom(this.orderViewModel.SelectedOrder);

            this.currentOrder = null;
        }
    }

    private void CloseModalCommandExecuted()
    {
        if (this.IsEditPopupOpen)
        {
            this.IsEditPopupOpen = false;
            this.currentCustomer = null;
        }
        else if (this.IsOrderPopupOpen)
        {
            this.IsOrderPopupOpen = false;
            this.currentOrder = null;
        }
    }
#endif

    private async void ToolbarItemTapped(object obj)
    {
        if (this.SelectedCustomer == null)
        {
            return;
        }

        if (obj.ToString().Equals("order"))
        {
#if MACCATALYST || WINDOWS
            this.orderViewModel.SelectedOrder = new Order();
            this.orderViewModel.SelectedCustomer = this.SelectedCustomer;
            this.orderViewModel.SelectedProduct = null;
            this.orderViewModel.SelectedEmployee = null;
            this.orderViewModel.SelectedDeliveryService = null;
            this.Title = "Create Order";

            this.IsOrderPopupOpen = true;
#else
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<OrderEditViewModel>(this.SelectedCustomer);
#endif
        }
        else if (obj.ToString().Equals("edit"))
        {
            this.currentCustomer = this.SelectedCustomer;
            var editedCustomer = this.currentCustomer.Copy();

#if MACCATALYST || WINDOWS
            this.Title = "Edit Customer";
            this.DeleteContextName = "Customer";
            this.editViewModel.SelectedCustomer = editedCustomer;
            this.IsEditPopupOpen = true;
#else
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<CustomerEditViewModel>(editedCustomer, this);
#endif
        }
    }

#if MACCATALYST || WINDOWS
    private void CreateNewCommandExecuted()
    {
        this.editViewModel.SelectedCustomer = new Customer();
        this.Title = "Create Customer";
        this.IsEditPopupOpen = true;
    }
#endif
}