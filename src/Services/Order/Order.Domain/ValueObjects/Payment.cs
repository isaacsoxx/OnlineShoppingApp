namespace Order.Domain.ValueObjects;

/// <summary>
/// Represents a customer's payment information as a value object.
/// Defined as record to enforce inmutability and help to compare equality based on values.
/// </summary>
public record Payment
{
  public string? CardName { get; } = default!;
  public string CardNumber { get; } = default!;
  public string Expiration { get; } = default!;
  public string CVV { get; } = default!;
  public int PaymentMethod { get; } = default!;

  protected Payment() { }

  private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
  {
    CardName = cardName;
    CardNumber = cardNumber;
    Expiration = expiration;
    CVV = cvv;
    PaymentMethod = paymentMethod;
  }

  /// <summary>
  /// Provides a clear and domain specific way to create Payment instance.
  /// </summary>
  /// <returns>Payment value object instance.</returns>
  public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
    ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
    ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
    ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

    return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
  }
}
