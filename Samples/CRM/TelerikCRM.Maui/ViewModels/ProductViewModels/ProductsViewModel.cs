using System.Collections.ObjectModel;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.ViewModels;

public class ProductsViewModel : ViewModelBase
{
    private readonly RemoteProductService productService;
    private bool isLoaded;
    private IReadOnlyList<Product> allProducts;
#if MACCATALYST || WINDOWS
    private ProductEditViewModel productViewModel;
    private bool isEditPopupOpen;
#endif
    private Product selectedProduct;

    public ProductsViewModel(IServiceProvider services, RemoteProductService service)
    {
        this.productService = service;

        this.CreateNewCommand = new Command(this.CreateNewCommandExecuted);
        this.ItemTapCommand = new Command(this.ItemTapped);

#if MACCATALYST || WINDOWS
        this.SaveModalCommand = new Command(this.SaveModalCommandExecuted);
        this.CloseModalCommand = new Command(this.CloseModalCommandExecuted);
        this.ProductViewModel = services.GetService(typeof(ProductEditViewModel)) as ProductEditViewModel;
#else
        this.CanCreateNew = true;
        this.SearchCommand = new Command(this.SearchCommandExecuted);
#endif
    }

#if MACCATALYST || WINDOWS
    public event EventHandler SearchCompleted;

    public ProductEditViewModel ProductViewModel
    {
        get => this.productViewModel;
        set => this.UpdateValue(ref this.productViewModel, value);
    }

    public bool IsEditPopupOpen
    {
        get => this.isEditPopupOpen;
        set => this.UpdateValue(ref this.isEditPopupOpen, value);
    }
#endif

    public ObservableCollection<Product> Products { get; } = new();

    public Product SelectedProduct
    {
        get => this.selectedProduct;
        set
        {
            if (this.UpdateValue(ref this.selectedProduct, value))
            {
#if MACCATALYST || WINDOWS
                if (this.productViewModel != null)
                {
                    this.productViewModel.SelectedProduct = this.selectedProduct;
                }
#endif
            }
        }
    }

    public Command ItemTapCommand { get; set; }

    public Command SaveModalCommand { get; set; }

    public Command CloseModalCommand { get; set; }

    public override async void OnAppearing()
    {
        await this.LoadProductsAsync();
    }

    public async Task LoadProductsAsync()
    {
        if (this.IsBusy || this.isLoaded)
        {
            return;
        }

        try
        {
            if (this.Products.Count == 0)
            {
                this.IsBusy = true;
                this.IsBusyMessage = "loading products...";

                this.allProducts = await this.productService.GetItemsAsync();

                foreach (var product in this.allProducts)
                {
                    this.Products.Add(product);
                }
            }
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", $"There was a problem loading products, check network connection and try again. Details: \r\n\n{ex.Message}", "OK");
        }
        finally
        {
            this.IsBusyMessage = "";
            this.IsBusy = false;
            this.isLoaded = true;
        }
    }

#if MACCATALYST || WINDOWS
    public void InvokeSearchCompleted()
        => this.SearchCompleted?.Invoke(this, EventArgs.Empty);
#endif

    private async void ItemTapped(object item)
    {
        if (item is Product product)
        {
#if MACCATALYST || WINDOWS
            this.SelectedProduct = null;
            this.SelectedProduct = product;

            this.productViewModel.SelectedProduct = this.SelectedProduct.Copy();
            this.Title = "Edit Product";
            this.DeleteContextName = "Product";
            this.IsEditPopupOpen = true;
#else
            var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
            await service.NavigateToAsync<ProductDetailViewModel>(product);
#endif
        }
    }

    private async void CreateNewCommandExecuted()
    {
#if MACCATALYST || WINDOWS
        this.productViewModel.SelectedProduct = new Product();
        this.Title = "Create Product";
        this.IsEditPopupOpen = true;
#else
        var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
        await service.NavigateToAsync<ProductEditViewModel>();
#endif
    }

#if MACCATALYST || WINDOWS
    private async void SaveModalCommandExecuted()
    {
        if (this.IsEditPopupOpen)
        {
            this.productViewModel?.SaveCommand?.Execute(null);

            // NOTE: Commented code is applicable for when app is not in read-only mode
            // this.IsEditPopupOpen = false;
            // this.selectedProduct?.CopyFrom(this.productViewModel.SelectedProduct);
            await this.productViewModel?.UpdateDatabaseAsync();
        }
    }

    private void CloseModalCommandExecuted()
    {
        if (this.IsEditPopupOpen)
        {
            this.IsEditPopupOpen = false;
        }
    }
#endif
}