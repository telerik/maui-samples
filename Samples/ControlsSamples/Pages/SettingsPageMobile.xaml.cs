using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;

namespace QSF.Pages;

public partial class SettingsPageMobile : ContentPage
{
    public SettingsPageMobile()
    {
        InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        SettingsViewModel vm = (SettingsViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
