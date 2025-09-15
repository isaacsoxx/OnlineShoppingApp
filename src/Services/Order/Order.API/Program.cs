using Order.API;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
#region Add services to the container.

/* Use the defined extension methods to add service per layer. */
builder.Services
  .AddApplicationServices()
  .AddInfrastructureServices(builder.Configuration)
  .AddApiServices();

#endregion
var app = builder.Build();

#region Configure HTTP request pipeline

/* Call the defined extension method for http request pipeline configuration per layer */
app.UseApiServices();
if (app.Environment.IsDevelopment()) await app.InitializeDatabaseAsync();

#endregion

app.Run();
