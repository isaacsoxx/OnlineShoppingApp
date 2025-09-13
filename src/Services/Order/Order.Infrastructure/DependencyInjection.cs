using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Infrastructure;

public static class DependencyInjection
{
  /// <summary>
  /// Extension method that adds required services for the infrastructure layer through dependency injection.
  /// </summary>
  /// <param name="services">Services configuration to add required dependencies.</param>
  /// <param name="configuration">Class that provides values from appsettings in use.</param>
  /// <returns>Main service configuration received, modified with new dependencies.</returns>
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("Database");
    // ToDo: Add SQLServer connection to the main services.
    // ToDo: Register EFCore application db context
    return services;
  }
}
