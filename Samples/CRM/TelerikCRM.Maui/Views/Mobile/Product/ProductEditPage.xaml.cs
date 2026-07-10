using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class ProductEditPage
{
    public ProductEditPage(ProductEditViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}