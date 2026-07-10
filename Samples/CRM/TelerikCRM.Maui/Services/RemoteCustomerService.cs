using Microsoft.Datasync.Client;
using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.Services;

public class RemoteCustomerService(DatasyncClientService clientService) : RemoteServiceBase<Customer>(clientService)
{
    public override async Task<string> GetIdAsync(Customer customer)
    {
        var customers = await this.table.Where(item => item.Name == customer.Name).ToListAsync();
        return customers.Count == 0 ? null : customers[0].Id;
    }
}