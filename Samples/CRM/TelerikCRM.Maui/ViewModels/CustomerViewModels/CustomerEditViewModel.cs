using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.ViewModels;

public class CustomerEditViewModel : ViewModelBase
{
    private Customer selectedCustomer = new();

    public CustomerEditViewModel(Customer selectedCustomer, object saveCommandParameter)
        : this()
    {
        this.SelectedCustomer = selectedCustomer;
#if !(MACCATALYST || WINDOWS)
        this.SaveCommandParameter = saveCommandParameter;
#endif
        this.Title = "Edit Customer";
    }

    public CustomerEditViewModel()
    {
#if !(MACCATALYST || WINDOWS)
        this.CanSave = true;
        this.CanNavigateBack = true;
        this.DeleteContextName = "Customer";
#endif

        this.Title = "Create Customer";
    }

    public Customer SelectedCustomer
    {
        get => this.selectedCustomer;
        set
        {
            if (this.UpdateValue(ref this.selectedCustomer, value))
            {
                this.Title = this.selectedCustomer == new Customer() ? "Create Customer" : "Edit Customer";
            }
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

        //     if (this.SelectedCustomer == new Customer())
        //     {
        //         await DependencyService.Get<Interfaces.IDataStore<Customer>>().AddItemAsync(this.SelectedCustomer);
        //     }
        //     else
        //     {
        //         await DependencyService.Get<Interfaces.IDataStore<Customer>>().UpdateItemAsync(this.SelectedCustomer);
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
}