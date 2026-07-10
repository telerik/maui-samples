using Microsoft.AspNetCore.Datasync.EFCore;

namespace TelerikCRM.Datasync.Server.Models;

public class Employee : EntityTableData
{
    public string Name { get; set; }

    public string PhotoUri { get; set; }

    public string OfficeLocation { get; set; }

    public DateTime HireDate { get; set; }

    public double Salary { get; set; }

    public int VacationBalance { get; set; }

    public int VacationUsed { get; set; }

    public string Notes { get; set; }
}