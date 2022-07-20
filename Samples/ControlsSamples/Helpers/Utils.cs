using Microsoft.Maui.Controls;
using QSF.Common;
using QSF.Services;
using QSF.ViewModels;
using QSF.Views;
using System;
using System.Globalization;
using System.Reflection;

namespace QSF.Helpers;

public static class Utils
{
    public static Type GetExampleViewModelType(string controlName, string exampleName)
    {
        AssemblyName assemblyName = GetAssemblyName();
        string typeName = string.Format("QSF.Examples.{0}Control.{1}Example.{2}ViewModel", controlName, exampleName, exampleName);
        Type type = GetTypeFromTypeName(assemblyName, typeName);
        return type;
    }

    public static object CreateView(Example example, bool createBindingContext)
    {
        IExampleService exampleService = DependencyService.Get<IExampleService>();
        View exampleView = exampleService.CreateExample(example) as View;

        if (createBindingContext && exampleView != null && exampleView.BindingContext == null)
        {
            Type viewModelType = GetExampleViewModelType(example.ControlName, example.Name);

            if (viewModelType == null)
            {
                viewModelType = typeof(ViewModels.ExampleViewModel);
            }

            object viewModel = Activator.CreateInstance(viewModelType);
            exampleView.BindingContext = viewModel;
        }

        return exampleView;
    }

    public static object CreateView(Control control)
    {
        ControlViewModel controlViewModel = new ControlViewModel(control);
        ControlViewDesktop controlView = new ControlViewDesktop { BindingContext = controlViewModel };
        return controlView;
    }

    public static View CreateConfigurationArea(Example example)
    {
        IConfigurationAreaService configurationAreaService = DependencyService.Get<IConfigurationAreaService>();
        View configurationArea = configurationAreaService.CreateConfigurationArea(example);
        return configurationArea;
    }

    public static string GetExampleCodeURL(Example example)
    {
        if (example is null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(example.CodeUrl))
        {
            return example.CodeUrl;
        }
        else
        {
            IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
            string url = configurationService.Configuration.ExampleCodeUrl + $"/{example.ControlName}Control/{example.Name}Example";
            return url;
        }
    }

    private static Type GetTypeFromTypeName(AssemblyName assemblyName, string typeName)
    {
        string fullTypeName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", typeName, assemblyName.FullName);
        Type type = Type.GetType(fullTypeName);
        return type;
    }

    private static AssemblyName GetAssemblyName()
    {
        AssemblyName assemblyName = typeof(Utils).GetTypeInfo().Assembly.GetName();
        return assemblyName;
    }
}
