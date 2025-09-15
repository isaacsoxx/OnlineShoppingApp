using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Models;
using Order.Domain.ValueObjects;

namespace Order.Infrastructure.Data.Configurations;

/// <summary>
/// Enforces business domain rules for [OrderItem] into the DB and specifies how DB entities should be mapped back into the domain.
/// </summary>
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    /* Sets the primary key for the entity. */
    builder.HasKey(oi => oi.Id);

    /* Defines how the primary key will be stored in the db, and how to read it back from db to identity value object. */
    builder.Property(oi => oi.Id).HasConversion(
      orderItemId => orderItemId.Value,
      dbId => OrderItemId.Of(dbId)
    );

    /* Defines how a foreign key with [Product] will be stored in the db. */
    builder.HasOne<Product>()
      .WithMany()
      .HasForeignKey(oi => oi.ProductId);

    /* Defines business rules for other properties. */
    builder.Property(oi => oi.Quantity).IsRequired();
    builder.Property(oi => oi.Price).IsRequired();
  }
}
