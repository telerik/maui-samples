using System.ComponentModel.DataAnnotations;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.Models.DataService;

public class Product : ServiceModelBase<Product>
{
    private string title;
    private double price;
    private string photoUri = "art_placeholder.png";
    private int inventoryCount;
    private bool isDiscontinued;
    private string description;

    [Required]
    [Display(Name = "Title", Description = "Product Title", Order = 0, Prompt = "Enter artwork title")]
    [DataType(DataType.Text)]
    public string Title
    {
        get => this.title;
        set => this.SetProperty(ref this.title, value);
    }

    [Display(Name = "Price", Description = "Product price in dollars", Order = 1, Prompt = "Enter product price (USD)")]
    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "C2")]
    [Range(0, 1000000000)]
    public double Price
    {
        get => this.price;
        set => this.SetProperty(ref this.price, value);
    }

    public string PhotoUri
    {
        get => this.photoUri;
        set
        {
            if (this.SetProperty(ref this.photoUri, value))
            {
                ImageCache.Invalidate(value);
                this.OnPropertyChanged(nameof(this.PhotoImageSource));
            }
        }
    }

    public ImageSource PhotoImageSource => ImageCache.GetImageSource(this.photoUri) ?? ImageSource.FromFile("art_placeholder.png");

    [Display(Name = "InventoryCount", Description = "Inventory Count", Order = 2, Prompt = "Enter the available quantity")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "C0")]
    [Range(0, 100000)]
    public int InventoryCount
    {
        get => this.inventoryCount;
        set => this.SetProperty(ref this.inventoryCount, value);
    }

    public bool IsDiscontinued
    {
        get => this.isDiscontinued;
        set => this.SetProperty(ref this.isDiscontinued, value);
    }

    [Display(Name = "Description", Description = "Product Description", Order = 3, Prompt = "Enter description of artwork")]
    [DataType(DataType.Text)]
    public string Description
    {
        get => this.description;
        set => this.SetProperty(ref this.description, value);
    }

    public override bool Equals(Product other)
        => other != null &&
           other.Id == this.Id &&
           other.Title == this.Title;

    public override Product Copy()
    {
        var product = new Product();
        product.CopyFrom(this);

        return product;
    }

    public override void CopyFrom(Product other)
    {
        this.Title = other.title;
        this.Price = other.price;
        this.PhotoUri = other.photoUri;
        this.InventoryCount = other.inventoryCount;
        this.IsDiscontinued = other.isDiscontinued;
        this.Description = other.description;
    }
}