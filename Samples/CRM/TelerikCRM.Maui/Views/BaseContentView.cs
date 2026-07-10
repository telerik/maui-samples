using Telerik.Maui.Controls;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views;

public class BaseContentView : RadContentView
{
    public BaseContentView()
    {
        this.Loaded += this.OnLoaded;
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        if (this.BindingContext is ViewModelBase viewModel)
        {
            viewModel.OnAppearing();
        }

        this.Loaded -= this.OnLoaded;
    }
}