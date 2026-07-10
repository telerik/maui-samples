using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class ProductDetailPage
{
    public ProductDetailPage(ProductDetailViewModel viewModel)
    {
        this.InitializeComponent();
        this.BindingContext = viewModel;
    }
}