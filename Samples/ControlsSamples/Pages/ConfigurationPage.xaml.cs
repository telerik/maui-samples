using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;

namespace QSF.Pages;

public partial class ConfigurationPage : ContentPage
{
    public ConfigurationPage()
    {
        InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
