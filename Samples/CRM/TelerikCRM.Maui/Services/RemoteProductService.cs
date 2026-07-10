using Microsoft.Datasync.Client;
using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.Services;

public class RemoteProductService(DatasyncClientService clientService) : RemoteServiceBase<Product>(clientService)
{
    public override async Task<string> GetIdAsync(Product employee)
    {
        var products = await this.table.Where(item => item.Title == employee.Title).ToListAsync();
        return products.Count == 0 ? null : products[0].Id;
    }
}