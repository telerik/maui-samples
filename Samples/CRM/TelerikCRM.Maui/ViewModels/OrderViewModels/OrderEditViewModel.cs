using System.Collections.ObjectModel;
using System.ComponentModel;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class OrderEditViewModel : ViewModelBase
{
    private Order selectedOrder;
    private Product selectedProduct;
    private Customer selectedCustomer;
    private Employee selectedEmployee;
    private string selectedDeliveryService;
    private bool isProductPreviewVisible;

    public OrderEditViewModel(Employee selectedEmployee)
        : this()
    {
        this.SelectedEmployee = selectedEmployee;
        this.Title = "Create Order";
    }

    public OrderEditViewModel(Customer selectedCustomer)
        : this()
    {
        this.SelectedCustomer = selectedCustomer;
        this.Title = "Create Order";
    }

    public OrderEditViewModel(Product selectedProduct)
        : this()
    {
        this.SelectedProduct = selectedProduct;
        this.Title = "Create Order";
    }

    public OrderEditViewModel(Order selectedOrder, Product relatedProduct, Customer relatedCustomer, Employee relatedEmployee)
        : this()
    {
        this.SelectedOrder = selectedOrder;
        this.SelectedProduct = relatedProduct;
        this.SelectedCustomer = relatedCustomer;
        this.SelectedEmployee = relatedEmployee;
        this.SelectedDeliveryService = selectedOrder.DeliveryService;

        this.Title = "Edit Order";
    }

    public OrderEditViewModel()
    {
        this.ProductImageTappedCommand = new Command(this.ProductImageTapped);
        this.SaveCommand = new Command(this.SaveCommandExecuted);

#if !(MACCATALYST || WINDOWS)
        this.CanNavigateBack = true;
        this.CanSave = true;
        this.DeleteContextName = "Order";
#endif

        this.Title = "Create Order";
    }

    public ObservableCollection<Product> Products { get; } = new();

    public ObservableCollection<Customer> Customers { get; } = new();

    public ObservableCollection<Employee> Employees { get; } = new();

    public ObservableCollection<string> DeliveryServices { get; } = new(new[] { "Shippersify", "Delivery Squad", "Superior Postal", "Bikezilla", "Express Transit", "Excite Courier" });

    public bool IsProductPreviewVisible
    {
        get => this.isProductPreviewVisible;
        set => this.UpdateValue(ref this.isProductPreviewVisible, value);
    }

    public Order SelectedOrder
    {
        get => this.selectedOrder;
        set
        {
            if (this.selectedOrder != null)
            {
                this.selectedOrder.PropertyChanged -= this.OnSelectedOrderPropertyChanged;
            }

            if (this.UpdateValue(ref this.selectedOrder, value))
            {
                if (this.selectedOrder != null)
                {
                    this.selectedOrder.PropertyChanged += this.OnSelectedOrderPropertyChanged;
                }

                this.SelectedProduct = this.Products.FirstOrDefault((p) => p.Id == this.selectedOrder?.ProductId);
                this.SelectedEmployee = this.Employees.FirstOrDefault((e) => e.Id == this.selectedOrder?.EmployeeId);
                this.SelectedCustomer = this.Customers.FirstOrDefault((c) => c.Id == this.selectedOrder?.CustomerId);
            }
        }
    }

    public Product SelectedProduct
    {
        get => this.selectedProduct;
        set
        {
            if (this.UpdateValue(ref this.selectedProduct, value) && this.selectedProduct != null && this.selectedOrder != null)
            {
                this.selectedOrder.ProductId = value.Id;
                this.selectedOrder.TotalPrice = value.Price * this.selectedOrder.Quantity;
            }
        }
    }

    public Customer SelectedCustomer
    {
        get => this.selectedCustomer;
        set
        {
            if (this.UpdateValue(ref this.selectedCustomer, value) && this.selectedCustomer != null && this.selectedOrder != null)
            {
                this.selectedOrder.CustomerId = this.selectedCustomer.Id;
            }
        }
    }

    public Employee SelectedEmployee
    {
        get => this.selectedEmployee;
        set
        {
            if (this.UpdateValue(ref this.selectedEmployee, value) && this.selectedEmployee != null && this.selectedOrder != null)
            {
                this.selectedOrder.EmployeeId = this.selectedEmployee.Id;
            }
        }
    }

    public string SelectedDeliveryService
    {
        get => this.selectedDeliveryService;
        set
        {
            if (this.UpdateValue(ref this.selectedDeliveryService, value) && this.selectedDeliveryService != null && this.selectedOrder != null)
            {
                this.selectedOrder.DeliveryService = value;
            }
        }
    }

    public Command ToolbarCommand { get; set; }

    public Command ProductImageTappedCommand { get; set; }

#if !(MACCATALYST || WINDOWS)
    public override async void OnAppearing()
    {
        base.OnAppearing();
        await this.LoadData();
    }
#endif

    public async Task LoadData()
    {
        try
        {
            this.IsBusy = true;
            this.IsBusyMessage = "loading data...";

            if (!this.Products.Any())
            {
                foreach (var p in await this.services.GetService<RemoteProductService>()?.GetItemsAsync()!)
                {
                    this.Products.Add(p);
                }
            }

            if (!this.Customers.Any())
            {
                foreach (var c in await this.services.GetService<RemoteCustomerService>()?.GetItemsAsync()!)
                {
                    this.Customers.Add(c);
                }
            }

            if (!this.Employees.Any())
            {
                foreach (var e in await this.services.GetService<RemoteEmployeeService>()?.GetItemsAsync()!)
                {
                    this.Employees.Add(e);
                }
            }

            if (this.SelectedOrder != null)
            {
                this.SelectedProduct = this.Products.FirstOrDefault((p) => p.Id == this.selectedOrder.ProductId);
                this.SelectedEmployee = this.Employees.FirstOrDefault((e) => e.Id == this.selectedOrder.EmployeeId);
                this.SelectedCustomer = this.Customers.FirstOrDefault((c) => c.Id == this.selectedOrder.CustomerId);
            }
            else
            {
                this.SelectedOrder = new Order();
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading associated data. Details:\r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusy = false;
        }
    }

    public async Task<bool> UpdateDatabaseAsync()
    {
        await this.DisplayReadOnlyAlertAsync();
        return true;

        // NOTE: Commented code is applicable for when app is not in read-only mode

        // try
        // {
        //     this.IsBusy = true;

        //     if (string.IsNullOrEmpty(this.SelectedOrder.ProductId))
        //     {
        //         this.SelectedOrder.ProductId = this.SelectedProduct.Id;
        //     }

        //     if (string.IsNullOrEmpty(this.SelectedOrder.EmployeeId))
        //     {
        //         this.SelectedOrder.EmployeeId = this.SelectedEmployee.Id;
        //     }

        //     if (string.IsNullOrEmpty(this.SelectedOrder.CustomerId))
        //     {
        //         this.SelectedOrder.CustomerId = this.SelectedCustomer.Id;
        //     }

        //     this.SelectedOrder.DeliveryService = SelectedDeliveryService;

        //     if (this.SelectedOrder == new Order())
        //     {
        //         await DependencyService.Get<Interfaces.IDataStore<Order>>().AddItemAsync(this.SelectedOrder);
        //     }
        //     else
        //     {
        //         await DependencyService.Get<Interfaces.IDataStore<Order>>().UpdateItemAsync(this.SelectedOrder);
        //     }

        //     return true;
        // }
        // catch (Exception ex)
        // {
        //     await this.DisplayAlertAsync("Error", $"There was a problem updating the database. Details:\r\n\n{ex.Message}", "OK");
        //     return false;
        // }
        // finally
        // {
        //     this.IsBusy = false;
        // }
    }

    private void OnSelectedOrderPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Order.Quantity) && this.selectedProduct != null)
        {
            this.selectedOrder.TotalPrice = this.selectedProduct.Price * this.selectedOrder.Quantity;
        }
    }

    private async void SaveCommandExecuted(object obj)
    {
        if (this.selectedProduct != null && this.selectedProduct.IsDiscontinued)
        {
            await this.DisplayAlertAsync("Discontinued", "This product is discontinued, please select a different product.", "OK");
        }
        else if (string.IsNullOrEmpty(this.selectedOrder.ProductId)
            || string.IsNullOrEmpty(this.selectedOrder.EmployeeId)
            || string.IsNullOrEmpty(this.selectedOrder.CustomerId))
        {
            await this.DisplayAlertAsync("Required Details Missing", "Double check your Employee, Product and Customer selections.", "OK");
        }
        else if (string.IsNullOrEmpty(this.selectedOrder.Street)
            || string.IsNullOrEmpty(this.selectedOrder.City)
            || string.IsNullOrEmpty(this.selectedOrder.State)
            || string.IsNullOrEmpty(this.selectedOrder.ZipCode)
            || string.IsNullOrEmpty(this.selectedOrder.Country))
        {
            await this.DisplayAlertAsync("Incomplete Address", "Please complete the shipping address of the order.", "OK");
        }
        else
        {
            await this.UpdateDatabaseAsync();
        }
    }

    private void ProductImageTapped()
    {
        this.IsProductPreviewVisible = !this.IsProductPreviewVisible;
    }
}