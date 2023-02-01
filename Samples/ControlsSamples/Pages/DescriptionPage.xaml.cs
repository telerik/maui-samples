using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;

namespace QSF.Pages;

public partial class DescriptionPage : ContentPage
{
    public DescriptionPage()
    {
        InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        DescriptionViewModel vm = (DescriptionViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
