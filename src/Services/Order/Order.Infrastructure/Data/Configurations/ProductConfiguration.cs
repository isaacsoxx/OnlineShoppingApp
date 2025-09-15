using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Models;
using Order.Domain.ValueObjects;

namespace Order.Infrastructure.Data.Configurations;

/// <summary>
/// Enforces business domain rules for [Product] into the DB and specifies how DB entities should be mapped back into the domain.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    /* Sets the primary key for the entity. */
    builder.HasKey(p => p.Id);

    /* Defines how the primary key will be stored in the db, and how to read it back from db to identity value object. */
    builder.Property(p => p.Id).HasConversion(
      productId => productId.Value,
      dbId => ProductId.Of(dbId)
    );

    /* Defines business rules for other properties. */
    builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
  }
}
