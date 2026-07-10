using Microsoft.Datasync.Client;
using TelerikCRM.Maui.Models.DataService;

namespace TelerikCRM.Maui.Services;

public class RemoteEmployeeService(DatasyncClientService clientService) : RemoteServiceBase<Employee>(clientService)
{
    public override async Task<string> GetIdAsync(Employee employee)
    {
        var employees = await this.table.Where(item => item.Name == employee.Name).ToListAsync();
        return employees.Count == 0 ? null : employees[0].Id;
    }
}