using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class EmployeeDetailViewModel : ViewModelBase
{
    private int companySalesCount;
    private int employeeSalesCount;
    private double companySalesRevenue;
    private double employeeSalesRevenue;
    private double averageOrderValue;
    private double salesContributionPercent;
    private int vacationRemaining;
    private int tenureYears;
    private Employee selectedEmployee;
#if MACCATALYST || WINDOWS
    private EmployeeEditViewModel editViewModel;
    private OrderEditViewModel orderViewModel;
    private bool isEditPopupOpen;
    private bool isOrderPopupOpen;
#endif

    public EmployeeDetailViewModel(Employee selectedEmployee)
        : this()
    {
        this.SelectedEmployee = selectedEmployee;
    }

    public EmployeeDetailViewModel()
    {
        this.ToolbarCommand = new Command(this.ToolbarItemTapped);

#if MACCATALYST || WINDOWS
        this.CreateNewCommand = new Command(this.CreateNewCommandExecuted);
        this.SaveModalCommand = new Command(this.SaveModalCommandExecuted);
        this.CloseModalCommand = new Command(this.CloseModalCommandExecuted);
        this.EditViewModel = services.GetService(typeof(EmployeeEditViewModel)) as EmployeeEditViewModel;
        this.OrderViewModel = services.GetService(typeof(OrderEditViewModel)) as OrderEditViewModel;
#else
        this.CanNavigateBack = true;
        this.NavigateBackContextName = "Employees";
#endif
    }

#if MACCATALYST || WINDOWS
    public EmployeeEditViewModel EditViewModel
    {
        get => this.editViewModel;
        set => this.UpdateValue(ref this.editViewModel, value);
    }

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

    public Employee SelectedEmployee
    {
        get => this.selectedEmployee;
        set
        {
            if (this.UpdateValue(ref this.selectedEmployee, value))
            {
                this.Title = this.SelectedEmployee?.Name;
            }
        }
    }

    public ObservableCollection<ChartDataPoint> CompensationData { get; } = new();

    public ObservableCollection<ChartDataPoint> SalesHistory { get; } = new();

    public int CompanySalesCount
    {
        get => this.companySalesCount;
        set => this.UpdateValue(ref this.companySalesCount, value);
    }

    public int EmployeeSalesCount
    {
        get => this.employeeSalesCount;
        set => this.UpdateValue(ref this.employeeSalesCount, value);
    }

    public double CompanySalesRevenue
    {
        get => this.companySalesRevenue;
        set => this.UpdateValue(ref this.companySalesRevenue, value);
    }

    public double EmployeeSalesRevenue
    {
        get => this.employeeSalesRevenue;
        set => this.UpdateValue(ref this.employeeSalesRevenue, value);
    }

    public double AverageOrderValue
    {
        get => this.averageOrderValue;
        set => this.UpdateValue(ref this.averageOrderValue, value);
    }

    public double SalesContributionPercent
    {
        get => this.salesContributionPercent;
        set => this.UpdateValue(ref this.salesContributionPercent, value);
    }

    public int VacationRemaining
    {
        get => this.vacationRemaining;
        set => this.UpdateValue(ref this.vacationRemaining, value);
    }

    public int TenureYears
    {
        get => this.tenureYears;
        set => this.UpdateValue(ref this.tenureYears, value);
    }

    public Command ToolbarCommand { get; set; }

    public Command SaveModalCommand { get; set; }

    public Command CloseModalCommand { get; set; }

    public async Task PrepareGaugeDataAsync()
    {
        if (this.IsBusy || this.SelectedEmployee == null)
        {
            return;
        }

        this.IsBusy = true;

        try
        {
            // *** Compensation Calculations *** //
            this.IsBusyMessage = "calculating compensation...";

            this.CompensationData.Clear();
            this.CalculateCompensationData(this.SelectedEmployee.Salary);

            // *** Employee KPIs *** //
            this.IsBusyMessage = "calculating KPIs...";
            this.TenureYears = (int)((DateTime.Now - this.SelectedEmployee.HireDate).TotalDays / 365.25);
            this.VacationRemaining = this.SelectedEmployee.VacationBalance - this.SelectedEmployee.VacationUsed;

            // *** Sales Calculations ** //
            this.IsBusyMessage = "calculating sales...";

            var orders = await this.services.GetService<RemoteOrderService>()?.GetItemsAsync()!;
            if (orders != null)
            {
                this.SalesHistory.Clear();
                this.CalculateSalesData(orders);
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem calculating {SelectedEmployee.Name}'s data. Details:\r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = "";
            this.IsBusy = false;
        }
    }

    public void CalculateCompensationData(double salary)
    {
        var rand = new Random();

        var bonusPercentage = (double)rand.Next(10, 20) / 100;
        var benefitsPercentage = (double)rand.Next(5, 15) / 100;
        var baseSalaryPercentage = 1 - bonusPercentage - benefitsPercentage;

        if (this.CompensationData.Any())
        {
            this.CompensationData.Clear();
        }

        this.CompensationData.Add(new ChartDataPoint { Title = "Base Salary", Value = salary * baseSalaryPercentage });
        this.CompensationData.Add(new ChartDataPoint { Title = "Benefits", Value = salary * benefitsPercentage });
        this.CompensationData.Add(new ChartDataPoint { Title = "Bonus", Value = salary * bonusPercentage });
    }

    public void CalculateSalesData(IReadOnlyList<Order> orders)
    {
        // Set company values
        this.CompanySalesCount = orders.Count;
        this.CompanySalesRevenue = Math.Floor(orders.Sum(o => o.TotalPrice));

        // Get the orders associated with the employee
        var employeeSales = orders.Where(o => o.EmployeeId == this.SelectedEmployee.Id).OrderBy(o => o.OrderDate.Date).ToList();

        // Set employee values
        this.EmployeeSalesCount = employeeSales.Count;
        this.EmployeeSalesRevenue = Math.Floor(employeeSales.Sum(o => o.TotalPrice));

        // Set derived KPIs
        this.AverageOrderValue = this.EmployeeSalesCount > 0
            ? Math.Round(this.EmployeeSalesRevenue / this.EmployeeSalesCount, 2)
            : 0;
        this.SalesContributionPercent = this.CompanySalesRevenue > 0
            ? Math.Round(this.EmployeeSalesRevenue / this.CompanySalesRevenue * 100, 1)
            : 0;

        // Create Sales History chart data
        foreach (var order in employeeSales)
        {
            this.SalesHistory.Add(new ChartDataPoint
            {
                Value = order.TotalPrice,
                Date = new DateTime(order.OrderDate.Year, order.OrderDate.Month, order.OrderDate.Day)
            });
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
            // this.SelectedEmployee?.CopyFrom(this.editViewModel.SelectedEmployee);
            await this.editViewModel?.UpdateDatabaseAsync();
        }
        else if (this.IsOrderPopupOpen)
        {
            // NOTE: Commented code is applicable for when app is not in read-only mode
            // this.IsOrderPopupOpen = false;
            this.orderViewModel?.SaveCommand?.Execute(null);
        }
    }

    private void CloseModalCommandExecuted()
    {
        if (this.IsEditPopupOpen)
        {
            this.IsEditPopupOpen = false;
        }
        else if (this.IsOrderPopupOpen)
        {
            this.IsOrderPopupOpen = false;
        }
    }
#endif

    private async void ToolbarItemTapped(object obj)
    {
        if (this.SelectedEmployee == null)
        {
            return;
        }

        if (obj.ToString().Equals("edit"))
        {
            var editedEmployee = this.SelectedEmployee.Copy();
#if MACCATALYST || WINDOWS
            this.editViewModel.SelectedEmployee = editedEmployee;
            this.Title = "Edit Employee";
            this.DeleteContextName = "Employee";
            this.IsEditPopupOpen = true;
#else
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<EmployeeEditViewModel>(editedEmployee, this);
#endif
        }
        else if (obj.ToString().Equals("order"))
        {
#if MACCATALYST || WINDOWS
            this.orderViewModel.SelectedOrder = new Order();
            this.orderViewModel.SelectedEmployee = this.SelectedEmployee;
            this.Title = "Create Order";
            this.IsOrderPopupOpen = true;
#else
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<OrderEditViewModel>(this.SelectedEmployee);
#endif
        }
    }

#if MACCATALYST || WINDOWS
    private void CreateNewCommandExecuted()
    {
        this.editViewModel.SelectedEmployee = new Employee();
        this.Title = "Create Employee";
        this.IsEditPopupOpen = true;
    }
#endif
}