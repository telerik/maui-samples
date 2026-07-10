using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Services;

/// <summary>
/// The NavigationService is responsible for all the navigation functionality of the application.
/// </summary>
public class NavigationService : INavigationService
{
    private readonly INavigation navigation = Application.Current!.Windows[0].Page!.Navigation;

    public Task NavigateToAsync<TViewModel>(params object[] arguments)
    {
        var page = CreatePage<TViewModel>(arguments);
        return this.navigation.PushAsync(page);
    }

    public Task NavigateToRootAsync()
        => this.navigation.PopToRootAsync();

    public Task NavigateBackAsync()
        => this.navigation.PopAsync();

    private static Page CreatePage<TViewModel>(params object[] arguments)
    {
        var viewModelType = typeof(TViewModel);
        var viewModelName = viewModelType.FullName;
        var viewName = viewModelName.Replace("ViewModels", ViewModelBase.IsMobile ? "Views.Mobile" : "Views.Desktop").Replace("ViewModel", "Page");
        var viewType = Type.GetType(viewName);
        var viewModel = Activator.CreateInstance(viewModelType, arguments);
        Page view = (Page)Activator.CreateInstance(viewType, viewModel);
        return view;
    }
}
