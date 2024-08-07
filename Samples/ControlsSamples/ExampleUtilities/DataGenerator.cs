using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Telerik.Maui;
using Telerik.AppUtils.Services;
using QSF.Services;
using QSF.Examples.DataGridControl.Common;

namespace QSF.ExampleUtilities;

public static class DataGenerator
{
    public static T GetItems<T>(string path)
    {
        var resourceService = DependencyService.Get<IResourceService>();
        var serializationService = DependencyService.Get<ISerializationService>();

        var stream = resourceService.GetResourceStream(path);
        T items = serializationService.XmlDeserializeFromStream<T>(stream);

        return items;
    }

    public static ObservableItemCollection<SalesPerson> GetSalesPersonItemCollection()
    {
        var sales = DataGenerator.GetItems<ObservableItemCollection<SalesPerson>>(ResourcePaths.PeoplePath);
        ApplySalesPersonAdditionalRandomizedFields(sales);
        return sales;
    }

    public static ObservableCollection<SalesPerson> GetSalesPersonCollection()
    {
        var sales = DataGenerator.GetItems<ObservableCollection<SalesPerson>>(ResourcePaths.PeoplePath);
        ApplySalesPersonAdditionalRandomizedFields(sales);
        return sales;
    }

    public static void ApplySalesPersonAdditionalRandomizedFields(IEnumerable<SalesPerson> sales)
    {
        var regions = new List<string>
        {
            "North",
            "South",
            "Central",
            "East",
            "West"
        };

        var images = new List<string>
        {
            "person_1.png",
            "person_2.png",
            "person_3.png",
            "person_4.png",
            "person_5.png",
            "person_6.png",
            "person_7.png",
            "person_8.png",
        };

        var flags = new List<string>
        {
            "flag_1.png",
            "flag_2.png",
            "flag_3.png",
            "flag_4.png",
            "flag_5.png",
            "flag_6.png",
        };

        var countriesAndFlags = new Dictionary<string, string>
        {
            {"Australia", flags[0]},
            {"United Kingdom", flags[1]},
            {"Canada", flags[2]},
            {"United States", flags[3]},
            {"France", flags[4]},
            {"Germany", flags[5]},
        };

        var random = DependencyService
            .Get<ITestingService>()
            .Random(742983);

        foreach(var salesperson in sales)
        {
            salesperson.RegionName = regions[random.Next(0, regions.Count)];
            salesperson.Sales = random.Next(2, 100) * random.Next(100, 999);
            salesperson.Image = images[random.Next(0, images.Count)];
        }
    }
}
