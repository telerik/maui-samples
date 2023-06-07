using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System;

namespace QSF.Examples.TreeViewControl.FirstLookExample;

public partial class EmailsPage : ContentPage
{
    public EmailsPage()
    {
        InitializeComponent();
    }

    private async void BackBtnClicked(object sender, EventArgs e)
    {
        ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
