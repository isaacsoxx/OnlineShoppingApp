using System.Reflection;
using Order.Domain.Models;

namespace Order.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  /* Defines DB tables based on the business domain entities. */
  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Product> Products => Set<Product>();
  public DbSet<RootOrder> RootOrders => Set<RootOrder>();
  public DbSet<OrderItem> OrderItems => Set<OrderItem>();

  /// <summary>
  /// Configures the entity mappings for the application's domain entities using the model builder.
  /// Applies all IEntityTypeConfiguration implementations from the current assembly.
  /// </summary>
  /// <param name="builder">
  /// The [ModelBuilder] instance used to configure the entity model for the context.
  /// </param>
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    base.OnModelCreating(builder);
  }
  
}
