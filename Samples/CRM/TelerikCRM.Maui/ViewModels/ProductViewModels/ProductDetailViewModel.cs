#if !(MACCATALYST || WINDOWS)
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class ProductDetailViewModel : ViewModelBase
{
    private Product selectedProduct;

    public ProductDetailViewModel(Product selectedProduct)
        : this()
    {
        this.SelectedProduct = selectedProduct;
    }

    public ProductDetailViewModel()
    {
        this.CanNavigateBack = true;
        this.NavigateBackContextName = "Products";

        this.ToolbarCommand = new Command(this.ToolbarItemTapped);
    }

    public Product SelectedProduct
    {
        get => this.selectedProduct;
        set
        {
            if (this.UpdateValue(ref this.selectedProduct, value))
            {
                this.Title = this.selectedProduct.Title;
            }
        }
    }

    public Command ToolbarCommand { get; set; }

    private async void ToolbarItemTapped(object obj)
    {
        if (this.SelectedProduct == null)
        {
            return;
        }

        var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
        if (obj.ToString().Equals("order"))
        {
            await service.NavigateToAsync<OrderEditViewModel>(this.SelectedProduct);
        }
        else if (obj.ToString().Equals("edit"))
        {
            await service.NavigateToAsync<ProductEditViewModel>(this.selectedProduct, this);
        }
    }
}
#endif