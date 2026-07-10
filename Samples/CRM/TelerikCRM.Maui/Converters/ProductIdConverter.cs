using System.Globalization;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.Converters;

public class ProductIdConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string productId)
        {
            var productService = Application.Current?.Windows[0]?.Page?.Handler?.MauiContext?.Services.GetService<RemoteProductService>();

            if (productService == null)
            {
                return null;
            }

            Product product = productService.GetItemAsync(productId).Result;

            if (parameter is not string productProp || product == null)
            {
                return null;
            }

            switch (productProp)
            {
                case nameof(Product.PhotoUri):
                    return product.PhotoImageSource;
                case nameof(Product.Title):
                    return product.Title;
                default:
                    break;
            }
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}