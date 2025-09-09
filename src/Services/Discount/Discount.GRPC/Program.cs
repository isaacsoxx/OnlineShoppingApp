
using Discount.GRPC.Data;
using Discount.GRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Database")));
#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.
/* Calls our static override method found in Data/Extensions.cs to configure database migrations automation. */
app.UseMigration();

/* Mapping defined gRPC services. */
app.MapGrpcService<DiscountService>();

#endregion

app.Run();
