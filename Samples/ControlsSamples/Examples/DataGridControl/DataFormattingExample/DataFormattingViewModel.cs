using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QSF.ViewModels;

namespace QSF.Examples.DataGridControl.DataFormattingExample;

public class DataFormattingViewModel : ExampleViewModel
{
    private List<string> products;
    private List<DateTime> dates;

    public DataFormattingViewModel()
    {
        this.Products = new ObservableCollection<Product>()
        {
            new Product{ Id = 1, Name = "Côte de Blaye", UnitPrice = 44, DateAdded = new DateTime(2022, 3, 10), IsAvailable = true },
            new Product{ Id = 2, Name = "Boston Crab Meat", UnitPrice = 64, DateAdded = new DateTime(2022, 6, 15), IsAvailable = true },
            new Product{ Id = 3, Name = "Singaporean Hokkien Fried Mee", UnitPrice= 92, DateAdded = new DateTime(2023, 1, 10), IsAvailable = false },
            new Product{ Id = 4, Name = "Gula Malacca", UnitPrice = 98.5, DateAdded = new DateTime(2023, 7, 20), IsAvailable = true },
            new Product{ Id = 5, Name =  "Rogede sild", UnitPrice = 81.9, DateAdded = new DateTime(2023, 9, 16), IsAvailable = true },
            new Product{ Id = 6, Name = "Spegesild", UnitPrice = 78, DateAdded = new DateTime(2023, 8, 15), IsAvailable = false },
            new Product{ Id = 7, Name = "Zaanse koeken", UnitPrice = 61, DateAdded = new DateTime(2022, 4, 15), IsAvailable = true },
            new Product{ Id = 8, Name = "Chocolade", UnitPrice = 39.9, DateAdded = new DateTime(2022, 1, 21), IsAvailable = true },
            new Product{ Id = 9, Name = "Maxilaku", UnitPrice = 64, DateAdded = new DateTime(2023, 2, 14), IsAvailable = false },
            new Product{ Id = 10, Name = "Valkoinen suklaa", UnitPrice = 54, DateAdded = new DateTime(2023, 12, 12), IsAvailable = true }
        };
    }

    public ObservableCollection<Product> Products { get; set; }
}
