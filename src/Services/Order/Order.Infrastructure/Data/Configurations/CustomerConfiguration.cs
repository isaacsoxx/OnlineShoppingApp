using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Models;
using Order.Domain.ValueObjects;

namespace Order.Infrastructure.Data.Configurations;

/// <summary>
/// Enforces business domain rules for [Customer] into the DB and specifies how DB entities should be mapped back into the domain.
/// </summary>
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    /* Sets the primary key for the entity. */
    builder.HasKey(c => c.Id);

    /* Defines how the primary key will be stored in the db, and how to read it back from db to identity value object. */
    builder.Property(c => c.Id).HasConversion(
      customerId => customerId.Value,
      dbId => CustomerId.Of(dbId)
    );

    /* Defines business rules for other properties. */
    builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
    builder.Property(c => c.Email).HasMaxLength(255);
    builder.HasIndex(c => c.Email).IsUnique();
  }
}
