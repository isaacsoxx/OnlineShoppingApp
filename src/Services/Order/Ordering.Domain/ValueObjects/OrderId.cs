namespace Order.Domain.ValueObjects;

/// <summary>
/// Represents identity for [RootOrder] entity as identifier value object.
/// Helps us avoid primitive obsession.
/// </summary>
public record OrderId
{
  public Guid Value { get; }

  private OrderId(Guid value) => Value = value;

  /// <summary>
  /// Provides a clear and domain specific way to create OrderId instance.
  /// </summary>
  /// <param name="value">Identification for a new Order Entity</param>
  /// <returns>OrderId instance representing the identity of a Order entity.</returns>
  public static OrderId Of(Guid value)
  {
    ArgumentNullException.ThrowIfNull(value);
    if (value == Guid.Empty) throw new DomainException("OrderId cannot be empty.");
    return new OrderId(value);
  }
}
