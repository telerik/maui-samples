using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Helpers;
using QSF.Pages;
using QSF.ViewModels;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace QSF.Services;

/// <summary>
/// The NavigationService is responsible for all the navigation functionality of the application.
/// </summary>
public class NavigationService : INavigationService
{
    private readonly INavigation navigation;

    public NavigationService()
    {
        this.navigation = Application.Current.MainPage.Navigation;
    }

    public Task NavigateToAsync<TViewModel>(params object[] arguments)
    {
        var page = this.CreatePage<TViewModel>(arguments);

        return this.navigation.PushAsync(page);
    }

    public Task NavigateToExampleAsync(Example example)
    {
        Type viewModelType = typeof(ExampleViewModel);
        Page view = this.CreatePage(viewModelType);

        Type exampleViewModelType = Utils.GetExampleViewModelType(example.ControlName, example.Name);
        if (exampleViewModelType != null)
        {
            viewModelType = exampleViewModelType;
        }

        var viewModel = (ExampleViewModel)Activator.CreateInstance(viewModelType);
        viewModel.Example = example;
        viewModel.HeaderTitle = example.DisplayName;
        view.BindingContext = viewModel;

        return this.navigation.PushAsync(view);
    }

    public Task NavigateToConfigurationPageAsync(ExampleViewModel viewModel)
    {
        ConfigurationPage configurationPage = new ConfigurationPage();
        configurationPage.BindingContext = viewModel;
        return this.navigation.PushAsync(configurationPage);
    }

    public Task NavigateToDescriptionPageAsync(DescriptionViewModel descriptionViewModel)
    {
        DescriptionPage descriptionPage = new DescriptionPage();
        descriptionPage.BindingContext = descriptionViewModel;
        return this.navigation.PushAsync(descriptionPage);
    }

    public Task NavigateToSettingsPageAsync(SettingsViewModel settingsViewModel)
    {
        SettingsPageMobile settingsPage = new SettingsPageMobile();
        settingsPage.BindingContext = settingsViewModel;
        return this.navigation.PushAsync(settingsPage);
    }

    public Task NavigateToRootAsync()
    {
        return this.navigation.PopToRootAsync();
    }

    public Task NavigateBackAsync()
    {
        return this.navigation.PopAsync();
    }

    public Type GetViewTypeForViewModel(Type viewModelType)
    {
        var viewName = viewModelType.FullName.Replace("ViewModel", "Page");
        var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
        var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
        var viewType = Type.GetType(viewAssemblyName);
        return viewType;
    }

    private Page CreatePage<TViewModel>(params object[] arguments)
    {
        var viewModelType = typeof(TViewModel);
        var viewModelName = viewModelType.FullName;
        var viewName = viewModelName.Replace("ViewModel", "Page");
        var viewType = Type.GetType(viewName);
        var viewModel = Activator.CreateInstance(viewModelType, arguments);
        Page view = (Page)Activator.CreateInstance(viewType);
        view.BindingContext = viewModel;
        return view;
    }

    private Page CreatePage(Type viewModelType)
    {
        Type pageType = GetViewTypeForViewModel(viewModelType);
        if (pageType == null)
        {
            throw new Exception($"Cannot locate page type for {viewModelType}");
        }

        return Activator.CreateInstance(pageType) as Page;
    }
}

