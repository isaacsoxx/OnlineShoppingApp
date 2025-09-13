namespace Order.Domain.ValueObjects;

/// <summary>
/// Represents identity for [Product] entity as identifier value object.
/// Helps us avoid primitive obsession.
/// </summary>
public class ProductId
{
  public Guid Value { get; }

  private ProductId(Guid value) => Value = value;

  /// <summary>
  /// Provides a clear and domain specific way to create ProductId instance.
  /// </summary>
  /// <param name="value">Identification for a new Order Item Entity</param>
  /// <returns>ProductId instance representing the identity of a Product entity.</returns>
  public static ProductId Of(Guid value)
  {
    ArgumentNullException.ThrowIfNull(value);
    if (value == Guid.Empty) throw new DomainException("ProductId cannot be empty.");
    return new ProductId(value);
  }
}
