using Microsoft.AspNetCore.Datasync;
using Microsoft.AspNetCore.Datasync.EFCore;
using Microsoft.AspNetCore.Mvc;
using TelerikCRM.Datasync.Server.Models;

namespace TelerikCRM.Datasync.Server.Controllers;

[Route("tables/customer")]
public class CustomerController : TableController<Customer>
{
    public CustomerController(AppDbContext context)
        : base(new EntityTableRepository<Customer>(context))
    {
    }

    public override Task<IActionResult> PatchAsync(string id, CancellationToken token = new ())
    {
        // Remove this before deploying to your Azure account.
        throw new HttpException(500, "Demo - Read-only mode");

        //return base.PatchAsync(id, token);
    }

    public override Task<IActionResult> ReplaceAsync(string id, Customer item, CancellationToken token = new ())
    {
        // Remove this before deploying to your Azure account.
        throw new HttpException(500, "Demo - Read-only mode");

        //return base.ReplaceAsync(id, item, token);
    }

    public override Task<IActionResult> DeleteAsync(string id, CancellationToken token = new ())
    {
        // Remove this before deploying to your Azure account.
        throw new HttpException(500, "Demo - Read-only mode");

        //return base.DeleteAsync(id, token);
    }
}