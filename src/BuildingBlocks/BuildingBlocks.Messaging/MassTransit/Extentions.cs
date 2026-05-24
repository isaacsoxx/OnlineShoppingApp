using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

/// <summary>
/// Provides extension methods for RabbitMQ setup.
/// </summary>
public static class Extentions
{
  /// <summary>
  /// Configures Masstransit with RabbitMQ settings.
  /// </summary>
  /// <param name="services">Service collection.</param>
  /// <param name="configuration">Access to application secrets (for RabbitMQ host configuration fetching).</param>
  /// <param name="assembly">Not required by Publisher applications, required for Consumer / Subscriber applications.</param>
  public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
  {
    services.AddMassTransit(config =>
    {
      /* Adds Masstransit into the .net service collection. */
      config.SetKebabCaseEndpointNameFormatter();

      /* If assembly is provided, assembly will be scanned and automatically register discover consumers. (For Consumers or Subscribers, not Publishers.) */
      if (assembly != null) config.AddConsumers(assembly);

      /* RabbitMQ host configurations. Configures the bus to use RabbitMQ as transport.  */
      config.UsingRabbitMq((context, configurator) =>
      {
        /* Defines host settings for RabbitMQ. */
        configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
        {
          host.Username(configuration["MessageBroker:UserName"]);
          host.Password(configuration["MessageBroker:Password"]);
        });
        /* Configuires endpoints passing the context. Allows Masstransit to automatically configures the endpoints for the consumers. */
        configurator.ConfigureEndpoints(context);
      });
    });
    
    
    return services;
  }
}
