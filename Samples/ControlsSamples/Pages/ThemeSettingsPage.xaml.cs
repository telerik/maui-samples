using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class ThemeSettingsPage : ContentPage
{
    public ThemeSettingsPage()
	{
		this.InitializeComponent();
	}

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ThemeSettingsViewModel vm = (ThemeSettingsViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}