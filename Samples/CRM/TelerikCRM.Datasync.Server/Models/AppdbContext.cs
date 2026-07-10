using Microsoft.EntityFrameworkCore;

namespace TelerikCRM.Datasync.Server.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public async Task InitializeDatabaseAsync()
    {
        await this.Database.EnsureCreatedAsync().ConfigureAwait(false);
    }
}
