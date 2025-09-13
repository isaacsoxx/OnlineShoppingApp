namespace Order.Domain.ValueObjects;

/// <summary>
/// Represents identity for [RootOrder] entity as identifier value object.
/// Helps us avoid primitive obsession.
/// </summary>
public record OrderName
{
  /// <summary>
  /// Defines business rule of minimun characters required for OrderName.
  /// </summary>
  public const int DefaultLength = 5;
  public string Value { get; }

  private OrderName(string value) => Value = value;

  /// <summary>
  /// Provides a clear and domain specific way to create OrderName instance.
  /// </summary>
  /// <param name="value">Name to provide to a new Order Item Entity</param>
  /// <returns>OrderName instance representing the identity of a Order entity.</returns>
  public static OrderName Of(string value)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(value);
    ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
    return new OrderName(value);
  }
}
