using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class ThemeSettingsPage : ContentPage
{
    public ThemeSettingsPage()
    {
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.InitializeComponent();
    }

    protected override void OnAppearing()
    {
        var vm = (ThemingViewModel)this.BindingContext;
        if (!vm.PrefetchStarted)
        {
            _ = vm.PrefetchAsync();
        }

        base.OnAppearing();
    }

    protected override bool OnBackButtonPressed()
    {
        var vm = (ThemingViewModel)this.BindingContext;
        return vm.Loading;
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        var vm = (ThemingViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}