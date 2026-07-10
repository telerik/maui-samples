#if !(MACCATALYST || WINDOWS)
using System.Windows.Input;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class OrderDetailViewModel : ViewModelBase
{
    private Order selectedOrder;
    private Customer relatedCustomer;
    private Employee relatedEmployee;
    private Product relatedProduct;

    public OrderDetailViewModel(Order selectedOrder)
        : this()
    {
        this.SelectedOrder = selectedOrder;
    }

    public OrderDetailViewModel()
    {
        this.CanNavigateBack = true;
        this.Title = "Order Details";
        this.ToolbarCommand = new Command(this.ToolbarItemTapped);
    }

    public Order SelectedOrder
    {
        get => this.selectedOrder;
        set => this.UpdateValue(ref this.selectedOrder, value);
    }

    public Customer RelatedCustomer
    {
        get => this.relatedCustomer;
        set => this.UpdateValue(ref this.relatedCustomer, value);
    }

    public Employee RelatedEmployee
    {
        get => this.relatedEmployee;
        set => this.UpdateValue(ref this.relatedEmployee, value);
    }

    public Product RelatedProduct
    {
        get => this.relatedProduct;
        set => this.UpdateValue(ref this.relatedProduct, value);
    }

    public ICommand ToolbarCommand { get; set; }

    public override async void OnAppearing()
    {
        base.OnAppearing();
        await this.GetRelatedDataAsync();
    }

    internal async Task GetRelatedDataAsync()
    {
        if (this.IsBusy || this.SelectedOrder == null)
        {
            return;
        }

        this.IsBusy = true;

        try
        {
            try
            {
                if (this.RelatedCustomer == null)
                {
                    this.IsBusyMessage = "loading customer...";
                    this.RelatedCustomer = await this.services.GetService<RemoteCustomerService>()?.GetItemAsync(this.SelectedOrder.CustomerId)!;
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlertAsync("Error", $"There was a problem loading the customer details: \r\n\n{ex.Message}", "OK");
            }

            try
            {
                if (this.RelatedEmployee == null)
                {
                    this.IsBusyMessage = "loading employee...";
                    this.RelatedEmployee = await this.services.GetService<RemoteEmployeeService>()?.GetItemAsync(this.SelectedOrder.EmployeeId)!;
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlertAsync("Error", $"There was a problem loading the employee details:  \r\n\n{ex.Message}", "OK");
            }

            try
            {
                if (this.RelatedProduct == null)
                {
                    this.IsBusyMessage = "loading product...";
                    this.RelatedProduct = await this.services.GetService<RemoteProductService>()?.GetItemAsync(this.SelectedOrder.ProductId)!;
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlertAsync("Error", $"There was a problem loading the product details: \r\n\n{ex.Message}", "OK");
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading the order's related data. Details:\r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = "";
            this.IsBusy = false;
        }
    }

    private async void ToolbarItemTapped(object obj)
    {
        if (this.SelectedOrder == null)
        {
            return;
        }

        if (obj.ToString()!.Equals("edit"))
        {
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<OrderEditViewModel>(this.selectedOrder, this.relatedProduct, this.relatedCustomer, this.relatedEmployee);
        }
    }
}
#endif