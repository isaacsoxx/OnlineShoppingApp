
var builder = WebApplication.CreateBuilder(args);

#region adding services to container.
var assembly = typeof(Program).Assembly;

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(assembly);
  config.AddOpenBehavior(typeof(ValidationBehavior<,>));
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
  options.Connection(builder.Configuration.GetConnectionString("Database")!);
  options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
#endregion

var app = builder.Build();

#region configure HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });
#endregion

app.Run();
