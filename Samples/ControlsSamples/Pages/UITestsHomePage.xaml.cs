using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Telerik.AppUtils.Services;
using QSF.ViewModels;
using QSF.Common;
using QSF.Services;

namespace QSF.Pages;

public partial class UITestsHomePage : ContentPage
{
    private HomeViewModel viewModel;

    public UITestsHomePage(ITestingService testingService)
    {
        this.viewModel = new HomeViewModel(testingService);
        InitializeComponent();
        this.BindingContext = this.viewModel;
    }

    private void OnSearchTestTextChanged(object sender, TextChangedEventArgs e)
    {
        DependencyService.Get<INavigationService>().NavigateCommand(e.NewTextValue);
    }
}