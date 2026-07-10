using Microsoft.AspNetCore.Datasync.EFCore;

namespace TelerikCRM.Datasync.Server.Models;

public class Product : EntityTableData
{
    public string Title { get; set; }

    public double Price { get; set; }

    public string PhotoUri { get; set; }

    public int InventoryCount { get; set; }

    public bool IsDiscontinued { get; set; }

    public string Description { get; set; }
}