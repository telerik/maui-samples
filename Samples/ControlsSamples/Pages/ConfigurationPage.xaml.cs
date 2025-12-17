using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class ConfigurationPage : ContentPage
{
    public ConfigurationPage()
    {
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
        this.BindingContext = null;
        await vm.NavigationService.NavigateBackAsync();
    }
}
