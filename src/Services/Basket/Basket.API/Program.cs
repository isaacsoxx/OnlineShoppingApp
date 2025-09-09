
using Discount.GRPC;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

#region adding services to container.

/* Register core services. */
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(assembly);
  config.AddOpenBehavior(typeof(ValidationBehavior<,>));
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

/* Register data services and decorator using Scrutor library. */
builder.Services.AddMarten(options =>
{
  options.Connection(builder.Configuration.GetConnectionString("Database")!);
  options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();
/* Main data service. */
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
/* Decorator data service. */
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();

/* Register distributed cache services. */
builder.Services.AddStackExchangeRedisCache(options =>
{
  options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

/* Register gRPC services. */
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
  options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
/* Bypass secure certification validation. <** DO NOT USE IN PRODUCTION ENVS. **> */
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
  {
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
  });

/* Register health checks, validators and exception handlers. */
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
  .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
  .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
builder.Services.AddValidatorsFromAssembly(assembly);

#endregion

var app = builder.Build();

#region configure HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
  new HealthCheckOptions
  {
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
  });
#endregion

app.Run();
