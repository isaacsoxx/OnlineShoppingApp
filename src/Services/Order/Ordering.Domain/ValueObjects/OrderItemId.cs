namespace Order.Domain.ValueObjects;

/// <summary>
/// Represents identity for [OrderItem] entity as identifier value object.
/// Helps us avoid primitive obsession.
/// </summary>
public record OrderItemId
{
  public Guid Value { get; }

  private OrderItemId(Guid value) => Value = value;

  /// <summary>
  /// Provides a clear and domain specific way to create OrderItemId instance.
  /// </summary>
  /// <param name="value">Identification for a new Order Item Entity</param>
  /// <returns>OrderItemId instance representing the identity of a OrderItem entity.</returns>
  public static OrderItemId Of(Guid value)
  {
    ArgumentNullException.ThrowIfNull(value);
    if (value == Guid.Empty) throw new DomainException("OrderItemId cannot be empty.");
    return new OrderItemId(value);
  }
}
