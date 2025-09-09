using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public class DiscountContext : DbContext
{
  /// <summary>
  /// Defining table to our database codefirst approach.
  /// </summary>
  /// <value></value>
  public DbSet<Coupon> Coupons { get; set; } = default!;

  public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
  { }

  /// <summary>
  /// Helps us to define entries to our dbContext when creating database models.
  /// </summary>
  /// <param name="modelBuilder">EF Core helper.</param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Coupon>().HasData(
      new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
      new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
    );
  }
}
