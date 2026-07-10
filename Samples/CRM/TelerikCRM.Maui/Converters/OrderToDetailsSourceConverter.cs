using System.Globalization;
using TelerikCRM.Maui.Models;
using TelerikCRM.Maui.Models.DataService;
using TelerikCRM.Maui.Services;

namespace TelerikCRM.Maui.Converters;

public class OrderToDetailsSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Order order)
        {
            return null;
        }

        List<OrderDetailsModel> orderDetails = new();

        var services = Application.Current!.Windows[0].Page!.Handler!.MauiContext!.Services;

        var productService = services.GetService<RemoteProductService>();
        var employeeService = services.GetService<RemoteEmployeeService>();
        var customerService = services.GetService<RemoteCustomerService>();

        if (productService == null || employeeService == null || customerService == null)
        {
            return orderDetails;
        }

        Product relatedProduct = productService.GetItemAsync(order.ProductId).Result;

        orderDetails.Add(new OrderDetailsModel()
        {
            ProductName = relatedProduct.Title,
            ProductPrice = relatedProduct.Price.ToString(CultureInfo.InvariantCulture),
            OrderedBy = customerService.GetItemAsync(order.CustomerId).Result?.Name,
            SoldBy = employeeService.GetItemAsync(order.EmployeeId).Result?.Name,
            DeliveredBy = order.DeliveryService
        });

        return orderDetails;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}