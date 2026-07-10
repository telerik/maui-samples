using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class ProductsView
{
    public ProductsView(ProductsViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}