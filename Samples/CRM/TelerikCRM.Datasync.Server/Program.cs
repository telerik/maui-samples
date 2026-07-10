using Microsoft.AspNetCore.Datasync;
using Microsoft.EntityFrameworkCore;
using TelerikCRM.Datasync.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// This is defined in the ConnectionStrings section of
// - In appsettings.json
//       and/or
// - In the Azure App Service's Configuration blade
// For more guidance, visit https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatasyncControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.InitializeDatabaseAsync().ConfigureAwait(false);
}

app.UseDefaultFiles(new DefaultFilesOptions{ DefaultFileNames = new List<string>{ "index.html" }});

app.UseStaticFiles();

app.MapControllers();

app.Run();