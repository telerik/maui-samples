using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataGridControl.SearchCategory.SemanticSearchExample;

// >> datagrid-semantic-search-viewmodel
public class ViewModel
{
    public ViewModel()
    {
        this.Products = new ObservableCollection<Product>
        {
            new Product { CategoryId = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" },
            new Product { CategoryId = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" },
            new Product { CategoryId = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet breads" },
            new Product { CategoryId = 4, CategoryName = "Dairy Products", Description = "Cheeses" },
            new Product { CategoryId = 5, CategoryName = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal" },
            new Product { CategoryId = 6, CategoryName = "Meat/Poultry", Description = "Prepared meats" },
            new Product { CategoryId = 7, CategoryName = "Produce", Description = "Dried fruit and bean curd" },
            new Product { CategoryId = 8, CategoryName = "Seafood", Description = "Seaweed and fish" },
            new Product { CategoryId = 9, CategoryName = "Snacks", Description = "Chips, pretzels, and popcorn" },
            new Product { CategoryId = 10, CategoryName = "Frozen Foods", Description = "Frozen vegetables, ice cream, and frozen dinners" },
            new Product { CategoryId = 11, CategoryName = "Household", Description = "Cleaning products, paper goods, and other household items" },
            new Product { CategoryId = 12, CategoryName = "Personal Care", Description = "Toiletries and personal care products" },
            new Product { CategoryId = 13, CategoryName = "Health", Description = "Health and wellness products" },
            new Product { CategoryId = 14, CategoryName = "Baby", Description = "Baby food, diapers, and other baby products" },
            new Product { CategoryId = 15, CategoryName = "Pet Supplies", Description = "Food and supplies for dogs, cats, and other pets" },
            new Product { CategoryId = 16, CategoryName = "Office Supplies", Description = "Office and school supplies" },
            new Product { CategoryId = 17, CategoryName = "Automotive", Description = "Automotive parts and accessories" },
            new Product { CategoryId = 18, CategoryName = "Books", Description = "Books across various genres and topics" },
            new Product { CategoryId = 19, CategoryName = "Music", Description = "CDs, vinyl records, and music accessories" },
            new Product { CategoryId = 20, CategoryName = "Movies", Description = "DVDs, Blu-rays, and streaming media" },
            new Product { CategoryId = 21, CategoryName = "Electronics", Description = "Computers, phones, and other electronic devices" },
            new Product { CategoryId = 22, CategoryName = "Clothing", Description = "Men's, women's, and children's apparel" },
        };
    }

    public ObservableCollection<Product> Products { get; set; }
}
// << datagrid-semantic-search-viewmodel
