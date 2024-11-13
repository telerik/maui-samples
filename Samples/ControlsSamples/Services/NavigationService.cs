using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Telerik.AppUtils.Services;
using QSF.Common;
using QSF.Helpers;
using QSF.Pages;
using QSF.ViewModels;

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

    public async Task NavigateToExampleAsync(Example example, bool popToMain = false, bool? animated = null)
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop ||
            DeviceInfo.Platform == DevicePlatform.WinUI ||
            DeviceInfo.Platform == DevicePlatform.MacCatalyst)
        {
            // Desktop navigation, MVVM
            var homeViewModel = (HomeViewModel)((NavigationPage)Application.Current.MainPage).RootPage.BindingContext;

            Control control = homeViewModel.Controls.First(c => c.Name == example.ControlName);
            control.StartupExample = control.Examples.FirstOrDefault(e => e.Name == example.Name);

            homeViewModel.SelectedControl = null;
            homeViewModel.SelectedControl = control;
        }
        else
        {
            // Mobile navigation, goes through INavigation
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

            if (!animated.HasValue)
            {
                animated = !DependencyService.Get<ITestingService>().IsAppUnderTest;
            }

            await this.navigation.PushAsync(view, animated.Value);

            if (popToMain)
            {
                // Purge the navigation stack...
                while (this.navigation.NavigationStack.Count > 2)
                {
                    this.navigation.RemovePage(this.navigation.NavigationStack[1]);
                }
            }
        }
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

    public Task NavigateToThemeSettingsPageAsync(ThemeSettingsViewModel themeSettingsViewModel)
    {
        ThemeSettingsPage themeSettingsPage = new ThemeSettingsPage();
        themeSettingsPage.BindingContext = themeSettingsViewModel;
        return this.navigation.PushAsync(themeSettingsPage);
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

    public async Task NavigateCommand(string cmd)
    {
        if (cmd.Length > 10 && cmd.FirstOrDefault() == '#' && cmd.LastOrDefault() == '#')
        {
            var examples = ((Application.Current.MainPage as NavigationPage).RootPage.BindingContext as HomeViewModel)?.Examples;

            var splittedText = cmd.Replace("#", string.Empty).Split('.');
            string controlText = splittedText.First();
            string exampleText = splittedText.Last();

            Example example = examples.FirstOrDefault(e => e.ControlName == controlText && e.Name == exampleText);

            await this.NavigateToExampleAsync(example, popToMain: true);
        }

        // Quick type - DataGrid.FirstLook goes "DGFL!" ComboBox.FirstLook goes "CmbFL!" (because CheckBox would go CB and mismatch with CB for combo)
        if (cmd.Length >= 3 && cmd[cmd.Length - 1] == '!')
        {
            var examples = ((Application.Current.MainPage as NavigationPage).RootPage.BindingContext as HomeViewModel)?.Examples;
            var example = examples.FirstOrDefault(e => Abbreviate(e.ControlName) + Abbreviate(e.Name) + '!' == cmd);
            string Abbreviate(string text)
            {
                if (new Dictionary<string, string>{
                        { "Button", "Btn" },
                        { "Border", "Brd" },
                        { "ComboBox", "Cmb" },
                        { "Customization", "Cu" },
                        { "Configuration", "Co" },
                        { "Chart", "Crt"},
                        { "Chat", "Ch"},
                        { "Expander", "Ex"},
                        { "Path", "Ph"},
                        { "PdfViewer", "PdV"},
                        { "PdfProcessing", "PdP"},
                        { "SpreadProcessing", "SpP"},
                        { "Slider", "Sl"},
                        { "TreeView", "Trv" }
                    }.TryGetValue(text, out var lookup))
                {
                    return lookup;
                }

                string result = "";
                for(var i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    if (c >= 'A' && c <= 'Z')
                    {
                        result += c;
                    }
                }
                return result;
            }

            if (example != null)
            {
                await this.NavigateToExampleAsync(example, popToMain: true);
            }
        }
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

