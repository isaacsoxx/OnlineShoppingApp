
var builder = WebApplication.CreateBuilder(args);
/* Define assembly as this program root. */
var assembly = typeof(Program).Assembly;

#region adding services to container.
/* Through MediatR add as many services and behaviors you need to be called along the request pipeline. */
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(assembly);
  config.AddOpenBehavior(typeof(ValidationBehavior<,>));
  config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
/* Add concrete validator created from the assembly (this project). */
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
  options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

/* Before seeding the data to the db, check if the env is development to we can proceed. */
if (builder.Environment.IsDevelopment())
{
  builder.Services.InitializeMartenWith<CatalogSeedData>();
}

/*Register global exception handler through CustomExceptionHandler. */
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
#endregion

var app = builder.Build();

#region configure HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
  ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
#endregion

app.Run();
