using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class ThemeSettingsPage : ContentPage
{
    public ThemeSettingsPage()
    {
        this.InitializeComponent();
    }

    protected override void OnAppearing()
    {
        ThemeSettingsViewModel vm = (ThemeSettingsViewModel)this.BindingContext;
        if (!vm.PrefetchStarted)
        {
            _ = vm.PrefetchAsync();
        }
        base.OnAppearing();
    }

    protected override bool OnBackButtonPressed()
    {
        ThemeSettingsViewModel vm = (ThemeSettingsViewModel)this.BindingContext;
        return vm.Loading;
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ThemeSettingsViewModel vm = (ThemeSettingsViewModel)this.BindingContext;
        vm.ResetExampleIfNeeded();
        await vm.NavigationService.NavigateBackAsync();
    }
}