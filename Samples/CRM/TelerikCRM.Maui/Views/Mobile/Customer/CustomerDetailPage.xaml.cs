using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class CustomerDetailPage
{
    public CustomerDetailPage(CustomerDetailViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}