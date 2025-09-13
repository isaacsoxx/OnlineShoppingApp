namespace Order.Domain.ValueObjects;

/// <summary>
/// Represents identity for [Customer] entity as identifier value object.
/// Helps us avoid primitive obsession.
/// </summary>
public record CustomerId
{
  public Guid Value { get; }

  private CustomerId(Guid value) => Value = value;

  /// <summary>
  /// Provides a clear and domain specific way to create CustomerId instance.
  /// </summary>
  /// <param name="value">Identification for a new Customer Entity</param>
  /// <returns>CustomerId instance representing the identity of a Customer entity.</returns>
  public static CustomerId Of(Guid value)
  {
    ArgumentNullException.ThrowIfNull(value);
    if (value == Guid.Empty) throw new DomainException("CustomerId cannot be empty.");
    return new CustomerId(value);
  }
}
