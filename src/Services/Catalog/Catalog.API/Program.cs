var builder = WebApplication.CreateBuilder(args);

#region adding services to container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(typeof(Program).Assembly);

});
#endregion

var app = builder.Build();

#region configure HTTP request pipeline.
app.MapCarter();
#endregion

app.Run();
