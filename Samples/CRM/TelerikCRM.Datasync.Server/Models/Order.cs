using Microsoft.AspNetCore.Datasync.EFCore;
using System.ComponentModel.DataAnnotations;

namespace TelerikCRM.Datasync.Server.Models;

public class Order : EntityTableData
{
    [Required, MinLength(1)]
    public string CustomerId { get; set; }

    [Required, MinLength(1)]
    public string EmployeeId { get; set; }

    [Required, MinLength(1)]
    public string ProductId { get; set; }

    public double TotalPrice { get; set; }

    public int Quantity { get; set; }

    public DateTime OrderDate { get; set; }

    public string DeliveryService { get; set; }

    public string Street { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string ZipCode { get; set; }

    public string Notes { get; set; }
}