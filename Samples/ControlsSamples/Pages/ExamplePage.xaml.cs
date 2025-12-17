using Microsoft.Maui.Controls;
using QSF.ViewModels;
using QSF.Services;
using QSF.Helpers;

namespace QSF.Pages;

public partial class ExamplePage : ContentPage
{
    public ExamplePage()
    {
#if ANDROID && NET10_0_OR_GREATER
        this.SafeAreaEdges = new Microsoft.Maui.SafeAreaEdges(Microsoft.Maui.SafeAreaRegions.Container);
#endif
        this.InitializeComponent();
        ThemingViewModel.Instance.ThemeChangedCallback = (oldTheme, newTheme) => this.Refresh();
    }

    private void Refresh()
    {
        if (this.BindingContext is ExampleViewModel vm && vm.Example != null)
        {
            Type exampleViewModelType = Utils.GetExampleViewModelType(vm.Example.ControlName, vm.Example.Name);
            if (exampleViewModelType != null)
            {
                var newViewModel = (ExampleViewModel)Activator.CreateInstance(exampleViewModelType);
                newViewModel.Example = vm.Example;
                newViewModel.HeaderTitle = vm.HeaderTitle;
                
                this.BindingContext = newViewModel;
            }
            else
            {
                vm.RaisePropertyChanged("Example");
                vm.RaisePropertyChanged("HeaderTitle");
            }
        }
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
