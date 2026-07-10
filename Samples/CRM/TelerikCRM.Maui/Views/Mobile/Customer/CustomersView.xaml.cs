using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class CustomersView
{
    public CustomersView(CustomersViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}