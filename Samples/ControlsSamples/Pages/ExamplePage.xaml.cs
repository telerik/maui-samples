using Microsoft.Maui.Controls;
using QSF.ViewModels;
using QSF.Services;

namespace QSF.Pages;

public partial class ExamplePage : ContentPage
{
    public ExamplePage()
    {
        this.InitializeComponent();
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
        await vm.NavigationService.NavigateBackAsync();
    }

    private async void OnExampleSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        await DependencyService.Get<INavigationService>().NavigateCommand(e.NewTextValue);
    }
}
