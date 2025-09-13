namespace Order.Domain.Models;

/// <summary>
/// Represents an individual item within an [RootOrder], containing order business domain properties.
/// </summary>
public class Product : Entity<ProductId>
{
  public string Name { get; private set; } = default!;
  public decimal Price { get; private set; } = default!;

  /// <summary>
  /// Creates instance for itself, ensuring all necessary validations are applied during creation.
  /// </summary>
  /// <returns>Instance of [Product] entity, based on business domain rules.
  public static Product Create(ProductId id, string name, decimal price)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(name);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

    return new Product { Id = id, Name = name, Price = price };
  }
}
