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
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.testingService = testingService;
        this.BindingContext = new HomeViewModel(testingService);
        this.InitializeComponent();
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
