using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class CustomerEditPage
{
    public CustomerEditPage(CustomerEditViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}