using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Order.API;

public static class DependencyInjection
{
  /// <summary>
  /// Extension method that adds required services for the API layer through dependency injection.
  /// </summary>
  /// <param name="services">Services configuration to add required dependencies.</param>
  /// <returns></returns>
  public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddCarter();
    services.AddExceptionHandler<CustomExceptionHandler>();
    services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

    return services;
  }

  /// <summary>
  /// Extension method called after adding services to the application. Will configure api endpoints to the http pipeline.
  /// </summary>
  /// <param name="app">Main application state.</param>
  /// <returns>Main application state with configured api endpoints.</returns>
  public static WebApplication UseApiServices(this WebApplication app)
  {
    app.MapCarter();
    app.UseExceptionHandler(options => { });
    app.UseHealthChecks("/health",
      new HealthCheckOptions
      {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });
    return app;
  }
  
}
