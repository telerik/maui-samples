using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class SettingsPageMobile : ContentPage
{
    public SettingsPageMobile()
    {
        this.InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        SettingsViewModel vm = (SettingsViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
