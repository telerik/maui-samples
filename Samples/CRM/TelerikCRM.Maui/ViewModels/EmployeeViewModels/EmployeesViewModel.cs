using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class EmployeesViewModel : ViewModelBase
{
    private readonly RemoteEmployeeService employeeService;
    private IReadOnlyList<Employee> allEmployees;
    private Employee selectedEmployee;
#if MACCATALYST || WINDOWS
    private EmployeeDetailViewModel detailViewModel;
#endif

    public EmployeesViewModel(IServiceProvider services, RemoteEmployeeService service)
    {
        this.employeeService = service;
        this.ItemTapCommand = new Command(this.ItemTapped);

#if MACCATALYST || WINDOWS
        this.DetailViewModel = services.GetService<EmployeeDetailViewModel>();
#else
        this.CanCreateNew = true;
        this.CreateNewCommand = new Command(this.CreateNewCommandExecuted);
        this.SearchCommand = new Command(this.SearchCommandExecuted);
#endif
    }

#if MACCATALYST || WINDOWS
    public event EventHandler SearchCompleted;

    public EmployeeDetailViewModel DetailViewModel
    {
        get => this.detailViewModel;
        set => this.UpdateValue(ref this.detailViewModel, value);
    }
#endif

    public ObservableCollection<Employee> Employees { get; } = new();

    public Employee SelectedEmployee
    {
        get => this.selectedEmployee;
        set
        {
            if (this.UpdateValue(ref this.selectedEmployee, value))
            {
#if MACCATALYST || WINDOWS
                if (this.detailViewModel != null)
                {
                    this.detailViewModel.SelectedEmployee = this.selectedEmployee;
                }
#endif
            }
        }
    }

    public Command ItemTapCommand { get; set; }

    public override async void OnAppearing()
    {
        await this.LoadEmployeesAsync();
    }

    public async Task LoadEmployeesAsync()
    {
        if (this.IsBusy)
            return;

        try
        {
            if (this.Employees.Count == 0)
            {
                this.IsBusy = true;
                this.IsBusyMessage = "loading employees...";

                this.allEmployees = await this.employeeService.GetItemsAsync();

                foreach (var employee in this.allEmployees)
                {
                    this.Employees.Add(employee);
                }

#if MACCATALYST || WINDOWS
                this.SelectedEmployee = this.allEmployees.FirstOrDefault();
                this.detailViewModel.SelectedEmployee = this.SelectedEmployee;
#endif
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading employees, check your network connection and try again. Details: \r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = "";
            this.IsBusy = false;
        }
    }

#if MACCATALYST || WINDOWS
    public void InvokeSearchCompleted()
        => this.SearchCompleted?.Invoke(this, EventArgs.Empty);
#endif

    private async void ItemTapped(object item)
    {
        if (item is Employee employee)
        {
#if MACCATALYST || WINDOWS
            if (this.detailViewModel != null)
            {
                this.detailViewModel.SelectedEmployee = employee;
            }
#else
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<EmployeeDetailViewModel>(employee);
#endif
        }
    }

#if !(MACCATALYST || WINDOWS)
    private async void CreateNewCommandExecuted()
    {
        var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
        await service.NavigateToAsync<EmployeeEditViewModel>();
    }
#endif
}