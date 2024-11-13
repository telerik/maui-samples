using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class ConfigurationPage : ContentPage
{
    public ConfigurationPage()
    {
        this.InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
        this.BindingContext = null;
        await vm.NavigationService.NavigateBackAsync();
    }
}
