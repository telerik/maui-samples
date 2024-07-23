using Microsoft.Maui.Controls;
using Telerik.AppUtils.Services;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class MainPageDesktop : ContentPage
{
    public MainPageDesktop(ITestingService testingService)
    {
        this.BindingContext = new HomeViewModel(testingService);
        this.InitializeComponent();
    }
}
