using Microsoft.Datasync.Client;
using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.Services;

public class RemoteOrderService(DatasyncClientService clientService) : RemoteServiceBase<Order>(clientService)
{
    public override async Task<string> GetIdAsync(Order order)
    {
        var orders = await this.table.Where(item =>
            item.CustomerId == order.CustomerId ||
            item.EmployeeId == order.EmployeeId ||
            item.ProductId == order.ProductId ||
            item.OrderDate == order.OrderDate).ToListAsync();

        return orders.Count == 0 ? null : orders[0].Id;
    }
}