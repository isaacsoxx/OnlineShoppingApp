namespace Order.API;

public static class DependencyInjection
{
  /// <summary>
  /// Extension method that adds required services for the API layer through dependency injection.
  /// </summary>
  /// <param name="services">Services configuration to add required dependencies.</param>
  /// <returns></returns>
  public static IServiceCollection AddApiServices(this IServiceCollection services)
  {
    // ToDo: Register Carter services.

    return services;
  }

  /// <summary>
  /// Extension method called after building the application. Will configure api endpoints to the http pipeline .
  /// </summary>
  /// <param name="app">Main application state.</param>
  /// <returns>Main application state with configured api endpoints.</returns>
  public static WebApplication UseApiServices(this WebApplication app)
  {
    // ToDo: Map carter api methods to app.
    return app;
  }
  
}
