using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class SettingsPageMobile : ContentPage
{
    public SettingsPageMobile()
    {
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        SettingsViewModel vm = (SettingsViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
