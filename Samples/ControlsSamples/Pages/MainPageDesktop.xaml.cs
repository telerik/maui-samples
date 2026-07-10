using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Telerik.AppUtils.Services;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class MainPageDesktop : ContentPage
{
    public MainPageDesktop(ITestingService testingService)
    {
        this.InitializeComponent();
        this.BindingContext = new HomeViewModel(testingService);

#if WINDOWS
#if NET10_0_OR_GREATER
        this.Resources["DefaultItemGridMargin"] = new Thickness(0, 0, 3, 3);
#else
        this.Resources["DefaultItemGridMargin"] = new Thickness(0);
#endif
#endif
    }
}
