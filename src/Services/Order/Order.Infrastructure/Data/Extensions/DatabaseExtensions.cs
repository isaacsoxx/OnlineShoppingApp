using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
  /// <summary>
  /// Applies all migrations into the database, and seeds data to all defined tables.
  /// </summary>
  /// <param name="app">Application we're configuring http request pipeline.</param>
  public static async Task InitializeDatabaseAsync(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    context.Database.MigrateAsync().GetAwaiter().GetResult();
    await SeedAsync(context);
  }

  /// <summary>
  /// Calls all methods to seed data into all DB defined tables.
  /// </summary>
  /// <param name="context">Database context to seed.</param>
  private static async Task SeedAsync(ApplicationDbContext context)
  {
    await SeedCustomerAsync(context);
    await SeedProductAsync(context);
    await SeedRootOrderWithItemsAsync(context);
  }

  /// <summary>
  /// Seeds [Customer] table with predefined initial data.
  /// </summary>
  /// <param name="context">Database context to seed.</param>
  private static async Task SeedCustomerAsync(ApplicationDbContext context)
  {
    if (!await context.Customers.AnyAsync())
    {
      await context.Customers.AddRangeAsync(InitialData.Customers);
      await context.SaveChangesAsync();
    }
  }

  /// <summary>
  /// Seeds [Product] table with predefined initial data.
  /// </summary>
  /// <param name="context">Database context to seed.</param>
  private static async Task SeedProductAsync(ApplicationDbContext context)
  {
    if (!await context.Products.AnyAsync())
    {
      await context.Products.AddRangeAsync(InitialData.Products);
      await context.SaveChangesAsync();
    }
  }

  /// <summary>
  /// Seeds [RootOrder] table with predefined initial data.
  /// </summary>
  /// <param name="context">Database context to seed.</param>
  private static async Task SeedRootOrderWithItemsAsync(ApplicationDbContext context)
  {
    if (!await context.RootOrders.AnyAsync())
    {
      await context.RootOrders.AddRangeAsync(InitialData.RootOrdersWithItems);
      await context.SaveChangesAsync();
    }
  }
}
