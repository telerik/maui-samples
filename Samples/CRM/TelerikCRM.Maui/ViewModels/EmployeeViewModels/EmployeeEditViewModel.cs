using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.ViewModels;

public class EmployeeEditViewModel : ViewModelBase
{
    private Employee selectedEmployee = new();

    public EmployeeEditViewModel(Employee selectedEmployee, object saveCommandParameter)
        : this()
    {
        this.SelectedEmployee = selectedEmployee;
#if !(MACCATALYST || WINDOWS)
        this.SaveCommandParameter = saveCommandParameter;
#endif
        this.Title = "Edit Employee";
    }

    public EmployeeEditViewModel()
    {
#if !(MACCATALYST || WINDOWS)
        this.CanSave = true;
        this.CanNavigateBack = true;
        this.DeleteContextName = "Employee";
#endif

        this.Title = "Create Employee";

        this.SetPhotoCommand = new Command(this.OpenImageEditor);
    }

    public Employee SelectedEmployee
    {
        get => this.selectedEmployee;
        set
        {
            if (this.UpdateValue(ref this.selectedEmployee, value))
            {
                this.Title = this.selectedEmployee == new Employee() ? "Create Employee" : "Edit Employee";
            }
        }
    }

    public Command SetPhotoCommand { get; set; }

    private async void OpenImageEditor()
    {
        if (this.SelectedEmployee == null)
        {
            return;
        }

#if MACCATALYST || WINDOWS
        await this.DisplayReadOnlyAlertAsync();
#else
        // var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
        // await service.NavigateToAsync<ImageEditorViewModel>(this.SelectedEmployee.PhotoUri);

        await this.DisplayReadOnlyAlertAsync();
#endif
    }

    public async Task<bool> UpdateDatabaseAsync()
    {
        await this.DisplayReadOnlyAlertAsync();
        return true;

        // NOTE: Commented code is applicable for when app is not in read-only mode

        // try
        // {
        //     this.IsBusy = true;

        //     if (string.IsNullOrEmpty(this.SelectedEmployee.PhotoUri) || this.SelectedEmployee.PhotoUri == "profile_photo.png")
        //     {
        //         this.OpenImageEditor();
        //     }

        //     if (this.SelectedEmployee == new Employee())
        //     {
        //         await DependencyService.Get<Interfaces.IDataStore<Employee>>().AddItemAsync(this.SelectedEmployee);
        //     }
        //     else
        //     {
        //         await DependencyService.Get<Interfaces.IDataStore<Employee>>().UpdateItemAsync(this.SelectedEmployee);
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