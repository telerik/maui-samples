using Microsoft.Datasync.Client;
using Telerik.Maui.Controls;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class SearchViewModel : NotifyPropertyChangedBase
{
    private readonly IServiceProvider services;
    private readonly IAlertService alertService;
    private readonly RemoteEmployeeService employeeService;
    private readonly RemoteCustomerService customerService;
    private readonly RemoteProductService productService;
    private string searchText;
    private string noResultsMessage;
    private bool isSeachOpen;
    private List<DatasyncClientData> searchResults;
    private DatasyncClientData selectedSearchResult;
    private Command navigateBackCommand;
    private bool isInternalPropertyChange = false;

    public SearchViewModel()
    {
        this.services = IPlatformApplication.Current!.Services;
        this.employeeService = services.GetService<RemoteEmployeeService>();
        this.customerService = services.GetService<RemoteCustomerService>();
        this.productService = services.GetService<RemoteProductService>();
        this.alertService = DependencyService.Get<IAlertService>();

        this.NavigateBackCommand = new Command(this.NavigateBackCommandExecuted);
    }

    public List<DatasyncClientData> SearchSource { get; set; } = new();

    public string SearchText
    {
        get => this.searchText;
        set
        {
            if (this.UpdateValue(ref this.searchText, value))
            {
                this.OnSearchTextChanged();
                this.IsSeachOpen = !string.IsNullOrEmpty(this.searchText);
            }
        }
    }

    public string NoResultsMessage
    {
        get => this.noResultsMessage;
        set => this.UpdateValue(ref this.noResultsMessage, value);
    }

    public bool IsSeachOpen
    {
        get => this.isSeachOpen;
        set => this.UpdateValue(ref this.isSeachOpen, value);
    }

    public List<DatasyncClientData> SearchResults
    {
        get => this.searchResults;
        set
        {
            if (this.UpdateValue(ref this.searchResults, value))
            {
                this.NoResultsMessage = (this.searchResults != null && this.searchResults.Count > 0) || string.IsNullOrEmpty(this.searchText)
                    ? string.Empty
                    : "No results found";
            }
        }
    }

    public DatasyncClientData SelectedSearchResult
    {
        get => this.selectedSearchResult;
        set
        {
            if (this.UpdateValue(ref this.selectedSearchResult, value))
            {
                this.OnSelectedSearchResultChanged();
            }
        }
    }

    public Command NavigateBackCommand
    {
        get => this.navigateBackCommand;
        set => this.UpdateValue(ref this.navigateBackCommand, value);
    }

    private async Task PrepareSearchSourceAsync()
    {
        if (this.SearchSource.Count > 0)
        {
            return;
        }

        try
        {
            var employeesTask = this.employeeService.GetItemsAsync();
            var customersTask = this.customerService.GetItemsAsync();
            var productsTask = this.productService.GetItemsAsync();

            Task.WaitAll(employeesTask, customersTask, productsTask);

            foreach (var employee in employeesTask.Result)
            {
                this.SearchSource.Add(employee);
            }

            foreach (var customer in customersTask.Result)
            {
                this.SearchSource.Add(customer);
            }

            foreach (var product in productsTask.Result)
            {
                this.SearchSource.Add(product);
            }
        }
        catch (Exception ex)
        {
            await this.alertService.DisplayAlertAsync("Error", $"There was a problem preparing search source. Details: \r\n\n{ex.Message}", "OK");
        }
    }

    private async void OnSelectedSearchResultChanged()
    {
        if (this.isInternalPropertyChange)
        {
            return;
        }

#if ANDROID || IOS
        await this.NavigateToSearchResult();
#elif MACCATALYST || WINDOWS
        this.ShowSearchResultRelatedView();
#endif
    }

#if ANDROID || IOS
    private async Task NavigateToSearchResult()
    {
        var searchResult = this.selectedSearchResult;
        var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();

        if (searchResult == null || service == null)
        {
            return;
        }

        if (searchResult is Employee employee)
        {
            await service.NavigateToAsync<EmployeeDetailViewModel>(employee);
        }
        else if (searchResult is Customer customer)
        {
            await service.NavigateToAsync<CustomerDetailViewModel>(customer);
        }
        else if (searchResult is Product product)
        {
            await service.NavigateToAsync<ProductDetailViewModel>(product);
        }
    }
#endif

#if MACCATALYST || WINDOWS
    private async void ShowSearchResultRelatedView()
    {
        var searchResult = this.selectedSearchResult;
        if (searchResult == null || this.services == null)
        {
            return;
        }

        MainPageViewModel mainPageViewModel = this.services.GetService<MainPageViewModel>();
        if (searchResult is Employee employee)
        {
            var viewModel = this.services.GetService<EmployeesViewModel>();
            await viewModel.LoadEmployeesAsync();

            viewModel.SelectedEmployee = viewModel.Employees.First(e => e.Equals(employee));
            mainPageViewModel.SelectedPage = mainPageViewModel.Pages.First(model => model.Title == "Employees");

            viewModel.InvokeSearchCompleted();
        }
        else if (searchResult is Customer customer)
        {
            var viewModel = this.services.GetService<CustomersViewModel>();
            await viewModel.LoadCustomersAsync();

            viewModel.SelectedCustomer = viewModel.Customers.First(c => c.Equals(customer));
            mainPageViewModel.SelectedPage = mainPageViewModel.Pages.First(model => model.Title == "Customers");

            viewModel.InvokeSearchCompleted();
        }
        else if (searchResult is Product product)
        {
            var viewModel = this.services.GetService<ProductsViewModel>();
            await viewModel.LoadProductsAsync();

            viewModel.SelectedProduct = viewModel.Products.First(p => p.Equals(product));
            mainPageViewModel.SelectedPage = mainPageViewModel.Pages.First(model => model.Title == "Products");;

            viewModel.InvokeSearchCompleted();
        }

        this.isInternalPropertyChange = true;
        this.SelectedSearchResult = null;
        this.SearchText = null;
        this.isInternalPropertyChange = false;
    }
#endif

    private async void OnSearchTextChanged()
    {
        await Task.Run(this.PrepareSearchSourceAsync);
        this.SearchResults = await Task.Run(this.GetSearchResults);
    }

    private List<DatasyncClientData> GetSearchResults()
    {
        List<DatasyncClientData> newResults = new();
        string searchTerm = this.searchText?.ToLowerInvariant();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            foreach (var item in this.SearchSource)
            {
                if (item is Employee employee)
                {
                    if (employee.Name.ToLowerInvariant().IndexOf(searchTerm) >= 0)
                    {
                        newResults.Add(employee);
                    }
                }
                else if (item is Customer customer)
                {
                    if (customer.Name.ToLowerInvariant().IndexOf(searchTerm) >= 0)
                    {
                        newResults.Add(customer);
                    }
                }
                else if (item is Product product)
                {
                    if (product.Title.ToLowerInvariant().IndexOf(searchTerm) >= 0)
                    {
                        newResults.Add(product);
                    }
                }
            }
        }

        return newResults;
    }

    private void NavigateBackCommandExecuted(object obj)
        => this.services.GetService<INavigationService>().NavigateBackAsync();
}