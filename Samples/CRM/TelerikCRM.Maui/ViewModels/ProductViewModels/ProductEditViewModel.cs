using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.ViewModels;

public class ProductEditViewModel : ViewModelBase
{
    private Product selectedProduct = new();

    public ProductEditViewModel(Product selectedProduct, object saveCommandParameter)
        : this()
    {
        this.SelectedProduct = selectedProduct;
#if !(MACCATALYST || WINDOWS)
        this.SaveCommandParameter = saveCommandParameter;
#endif
        this.Title = "Edit Product";
    }

    public ProductEditViewModel()
    {
        this.SetPhotoCommand = new Command(this.OpenImageEditor);

#if !(MACCATALYST || WINDOWS)
        this.CanSave = true;
        this.CanNavigateBack = true;
        this.DeleteContextName = "Product";
#endif

        this.Title = "Create Product";
    }

    public Product SelectedProduct
    {
        get => this.selectedProduct;
        set
        {
            if (this.UpdateValue(ref this.selectedProduct, value))
            {
                this.Title = this.selectedProduct == new Product() ? "Create Product" : "Edit Product";
            }
        }
    }

    public Command SetPhotoCommand { get; set; }

    public Command ToolbarCommand { get; set; }

    private async void OpenImageEditor()
    {
        if (this.SelectedProduct == null)
        {
            return;
        }

#if MACCATALYST || WINDOWS
        await this.DisplayReadOnlyAlertAsync();
#else
        // var service = IPlatformApplication.Current!.Services.GetService<INavigationService>();
        // await service.NavigateToAsync<ImageEditorViewModel>(this.SelectedProduct.PhotoUri);

        await this.DisplayReadOnlyAlertAsync();
#endif
    }

    public async Task<bool> UpdateDatabaseAsync()
    {
        await this.DisplayReadOnlyAlertAsync();
        return true;

        // NOTE: Commented code is applicable for when app is not in read-only mode

        // try
        // {
        //    this.IsBusy = true;

        //    if (string.IsNullOrEmpty(this.SelectedProduct.PhotoUri) || this.SelectedProduct.PhotoUri == "art_placeholder.png")
        //    {
        //        this.OpenImageEditor();
        //    }

        //    if (this.SelectedProduct == new Product())
        //    {
        //        await DependencyService.Get<Interfaces.IDataStore<Product>>().AddItemAsync(this.SelectedProduct);
        //    }
        //    else
        //    {
        //        await DependencyService.Get<Interfaces.IDataStore<Product>>().UpdateItemAsync(this.SelectedProduct);
        //    }

        //    return true;
        // }
        // catch (Exception ex)
        // {
        //    await this.DisplayAlertAsync("Error", $"There was a problem updating the database. Details:\r\n\n{ex.Message}", "OK");
        //    return false;
        // }
        // finally
        // {
        //    this.IsBusy = false;
        // }
    }
}