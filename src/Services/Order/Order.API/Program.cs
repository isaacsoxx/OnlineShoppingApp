using Order.API;
using Order.Application;
using Order.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
#region Add services to the container.

/* Use the defined extension methods for each layer. */
builder.Services
  .AddApplicationServices()
  .AddInfrastructureServices(builder.Configuration)
  .AddApiServices();

#endregion
var app = builder.Build();

#region Configure HTTP request pipeline

#endregion

app.Run();
