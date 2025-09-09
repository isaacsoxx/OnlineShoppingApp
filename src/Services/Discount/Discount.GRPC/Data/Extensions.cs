using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public static class Extensions
{

  /// <summary>
  /// Define database migrations automation.
  /// </summary>
  /// <param name="app">Application builder helps us to get an app initial scope.</param>
  /// <returns></returns>
  public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
  {
    /* Scope needed to reach our db context from application builder. */
    using var scope = app.ApplicationServices.CreateScope();

    /* Reach dbContext (based on the defined class provided inside <<GetRequiredService>>) using above line defined scope. */
    using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();

    /* Perform migrate/auto-migrate operations on our dbContext. */
    /* Allows us to apply any pending migrations for the defined context and create the database if doesn't exists. */
    dbContext.Database.MigrateAsync();

    /* Return app for continuing configuration of our app on Program.cs */
    return app;
  }
}
