using Microsoft.Maui.Controls;
using Telerik.AppUtils.Services;
using QSF.ViewModels;
#if IOS
using UIKit;
#endif

namespace QSF.Pages;

public partial class MainPageMobile : ContentPage
{
    private ITestingService testingService;

    public MainPageMobile(ITestingService testingService)
    {
        this.testingService = testingService;
        this.BindingContext = new HomeViewModel(testingService);
        this.InitializeComponent();

        this.Loaded += this.OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs args)
    {
        this.Loaded -= OnLoaded;

        if (DependencyService.Get<ITestingService>().IsAppUnderTest)
        {
            HomeViewModel vm = (HomeViewModel)this.BindingContext;
#if IOS
            // TRICKY: Pushing a non-animated page on startup will screw up the page navigation in iOS.
            vm.NavigationService.NavigateToExampleAsync(vm.Examples[0], animated: true);
#else
            vm.NavigationService.NavigateToExampleAsync(vm.Examples[0], animated: false);
#endif
        }
    }

    private void Settings_Clicked(object sender, EventArgs e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm.NavigateToSettings();
    }

    private void Search_Clicked(object sender, EventArgs e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm.NavigateToSearch();
    }

    private void controlsCollectionView_ItemTapped(object sender, Telerik.Maui.RadTappedEventArgs<object> e)
    {
        HomeViewModel vm = (HomeViewModel)this.BindingContext;
        vm?.SelectControlCommand?.Execute(e.Data);
    }
}
