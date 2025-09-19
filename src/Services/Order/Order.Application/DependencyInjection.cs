using System.Reflection;
using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Application;

public static class DependencyInjection
{
  /// <summary>
  /// Extension method that adds required services for the application layer through dependency injection.
  /// </summary>
  /// <param name="services">Services configuration to add required dependencies.</param>
  /// <returns>Main service configuration received, modified with new dependencies.</returns>
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(configuration =>
    {
      configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

      configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
      configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    return services;
  } 
}
