using Telerik.Maui.Controls;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public abstract class ViewModelBase : NotifyPropertyChangedBase
{
    internal static readonly bool IsMobile = DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS;

    internal readonly IServiceProvider services;
    private readonly IAlertService alertService;
    private bool isBusy;
#if !(MACCATALYST || WINDOWS)
    private bool canNavigateBack;
    private bool canCreateNew;
    private bool canSave;
    private string navigateBackContextName;
    private object saveCommandParameter;
    private Command navigateBackCommand;
    private Command searchCommand;
#endif
    private string title;
    private string isBusyMessage;
    private string deleteContextName;
    private Command createNewCommand;
    private Command saveCommand;
    private Command deleteCommand;

    public ViewModelBase()
    {
        this.services = IPlatformApplication.Current!.Services;
        this.alertService = services.GetService<IAlertService>();
#if !(MACCATALYST || WINDOWS)
        this.NavigateBackCommand = new Command(this.NavigateBackCommandExecuted);
#endif
        this.DeleteCommand = new Command(this.DeleteCommandExecuted);
    }

    public bool IsBusy
    {
        get => this.isBusy;
        set => this.UpdateValue(ref this.isBusy, value);
    }

#if !(MACCATALYST || WINDOWS)
    public bool CanNavigateBack
    {
        get => this.canNavigateBack;
        set => this.UpdateValue(ref this.canNavigateBack, value);
    }

    public bool CanCreateNew
    {
        get => this.canCreateNew;
        set => this.UpdateValue(ref this.canCreateNew, value);
    }

    public bool CanSave
    {
        get => this.canSave;
        set => this.UpdateValue(ref this.canSave, value);
    }

    public string NavigateBackContextName
    {
        get => this.navigateBackContextName;
        set => this.UpdateValue(ref this.navigateBackContextName, value);
    }

    public object SaveCommandParameter
    {
        get => this.saveCommandParameter;
        set => this.UpdateValue(ref this.saveCommandParameter, value);
    }

    public Command NavigateBackCommand
    {
        get => this.navigateBackCommand;
        set => this.UpdateValue(ref this.navigateBackCommand, value);
    }

    public Command SearchCommand
    {
        get => this.searchCommand;
        set => this.UpdateValue(ref this.searchCommand, value);
    }
#endif

    public string Title
    {
        get => this.title;
        set => this.UpdateValue(ref this.title, value);
    }

    public string IsBusyMessage
    {
        get => this.isBusyMessage;
        set => this.UpdateValue(ref this.isBusyMessage, value);
    }

    public string DeleteContextName
    {
        get => this.deleteContextName;
        set => this.UpdateValue(ref this.deleteContextName, value);
    }

    public Command CreateNewCommand
    {
        get => this.createNewCommand;
        set => this.UpdateValue(ref this.createNewCommand, value);
    }

    public Command SaveCommand
    {
        get => this.saveCommand;
        set => this.UpdateValue(ref this.saveCommand, value);
    }

    public Command DeleteCommand
    {
        get => this.deleteCommand;
        set => this.UpdateValue(ref this.deleteCommand, value);
    }

    public virtual void OnAppearing() { }

#if !(MACCATALYST || WINDOWS)
    internal async void SearchCommandExecuted(object obj)
    {
        var service = this.services.GetService<INavigationService>();
        await service.NavigateToAsync<SearchViewModel>();
    }
#endif

    public async Task DisplayAlertAsync(string title, string message, string cancel)
        => await this.alertService.DisplayAlertAsync(title, message, cancel);

    public async Task DisplayReadOnlyAlertAsync()
        => await this.alertService.DisplayReadOnlyAlertAsync();

#if !(MACCATALYST || WINDOWS)
    private void NavigateBackCommandExecuted(object obj)
        => this.services.GetService<INavigationService>().NavigateBackAsync();
#endif

    private async void DeleteCommandExecuted()
        => await this.DisplayReadOnlyAlertAsync();
}