using Microsoft.Maui.Controls;
using QSF.ViewModels;

namespace QSF.Pages;

public partial class DescriptionPage : ContentPage
{
    public DescriptionPage()
    {
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.InitializeComponent();
    }
    private async void Back_Clicked(object sender, EventArgs e)
    {
        DescriptionViewModel vm = (DescriptionViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }
}
