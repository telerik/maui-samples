using Microsoft.AspNetCore.Datasync.EFCore;

namespace TelerikCRM.Datasync.Server.Models;

public class Customer : EntityTableData
{
    public string Name { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string ZipCode { get; set; }

    public string Notes { get; set; }
}