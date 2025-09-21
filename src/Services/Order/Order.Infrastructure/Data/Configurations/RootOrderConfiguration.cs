using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Enums;
using Order.Domain.Models;
using Order.Domain.ValueObjects;

namespace Order.Infrastructure.Data.Configurations;

/// <summary>
/// Enforces business domain rules for [RootOrder] into the DB and specifies how DB entities should be mapped back into the domain.
/// </summary>
public class RootOrderConfiguration : IEntityTypeConfiguration<RootOrder>
{
  public void Configure(EntityTypeBuilder<RootOrder> builder)
  {
    /* Sets the primary key for the entity. */
    builder.HasKey(ro => ro.Id);

    /* Defines how the primary key will be stored in the db, and how to read it back from db to identity value object. */
    builder.Property(ro => ro.Id).HasConversion(
      orderId => orderId.Value,
      dbId => OrderId.Of(dbId)
    );

    /* Defines how a foreign key with [Customer] will be stored in the db. */
    builder.HasOne<Customer>()
      .WithMany()
      .HasForeignKey(ro => ro.CustomerId)
      .IsRequired();

    /* Defines how a foreign key with [OrderItem] will be stored in the db. */
    builder.HasMany(ro => ro.OrderItems)
      .WithOne()
      .HasForeignKey(oi => oi.OrderId);

    /* Defines how the DB can store descriptive value objects. */
    builder.ComplexProperty(
      ro => ro.OrderName,
      nameBuilder =>
      {
        nameBuilder.Property(n => n.Value)
          .HasColumnName(nameof(RootOrder.OrderName))
          .HasMaxLength(100)
          .IsRequired();
      });

    builder.ComplexProperty(ro => ro.ShippingAddress, addressBuilder =>
    {
      /* Defines how business domain rules should be mapped into the DB for the shipping address descriptive value object. */
      addressBuilder.Property(a => a.FirstName)
        .HasMaxLength(50)
        .IsRequired();

      addressBuilder.Property(a => a.LastName)
        .HasMaxLength(50)
        .IsRequired();

      addressBuilder.Property(a => a.EmailAddress)
        .HasMaxLength(50);

      addressBuilder.Property(a => a.AddressLine)
        .HasMaxLength(180)
        .IsRequired();

      addressBuilder.Property(a => a.Country)
        .HasMaxLength(50);

      addressBuilder.Property(a => a.State)
        .HasMaxLength(50);

      addressBuilder.Property(a => a.ZipCode)
        .HasMaxLength(5)
        .IsRequired();
    });

    builder.ComplexProperty(ro => ro.BillingAddress, addressBuilder =>
    {
      /* Defines how business domain rules should be mapped into the DB for the billing address descriptive value object. */
      addressBuilder.Property(a => a.FirstName)
        .HasMaxLength(50)
        .IsRequired();

      addressBuilder.Property(a => a.LastName)
        .HasMaxLength(50)
        .IsRequired();

      addressBuilder.Property(a => a.EmailAddress)
        .HasMaxLength(50);

      addressBuilder.Property(a => a.AddressLine)
        .HasMaxLength(180)
        .IsRequired();

      addressBuilder.Property(a => a.Country)
        .HasMaxLength(50);

      addressBuilder.Property(a => a.State)
        .HasMaxLength(50);

      addressBuilder.Property(a => a.ZipCode)
        .HasMaxLength(5)
        .IsRequired();
    });

    builder.ComplexProperty(ro => ro.Payment, paymentBuilder =>
    {
      /* Defines how business domain rules should be mapped into the DB for the payment descriptive value object. */
      paymentBuilder.Property(p => p.CardName)
        .HasMaxLength(50);

      paymentBuilder.Property(p => p.CardNumber)
        .HasMaxLength(24)
        .IsRequired();

      paymentBuilder.Property(p => p.Expiration)
        .HasMaxLength(10);

      paymentBuilder.Property(p => p.CVV)
        .HasMaxLength(3);

      paymentBuilder.Property(p => p.PaymentMethod);
    });

    /* Defines how the status enum should be mapped into the DB. */
    builder.Property(ro => ro.Status)
      .HasDefaultValue(OrderStatus.Draft)
      .HasConversion(s =>
        s.ToString(),
        dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
      );

    /* Defines business rules for other properties. */
    builder.Property(ro => ro.TotalPrice);
  }
}
