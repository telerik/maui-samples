using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views;

public class BasePage : ContentPage
{
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (this.BindingContext is ViewModelBase viewModel)
        {
            viewModel.OnAppearing();
        }
    }
}