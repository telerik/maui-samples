using Microsoft.AspNetCore.Mvc;

namespace TelerikCRM.Datasync.Server.Controllers;

[Route("api/values")]
public class ValuesController : Controller
{
    public string Get()
    {
        var version = typeof(ValuesController).Assembly.GetName().Version;
        
        var host = this.Request.Host.Host ?? "localhost";
        
        var greeting = $"You are using Telerik CRM Data Services from {host} (v.{version?.Major}.{version?.Minor}.{version?.Build}.{version?.Revision}).";
        
        Console.WriteLine(greeting);

        return greeting;
    }
}
