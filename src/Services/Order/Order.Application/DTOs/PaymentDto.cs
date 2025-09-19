namespace Order.Application.DTOs;

/// <summary>
/// Represents [Payment] descriptive value object.
/// </summary>
public record PaymentDto(
  string CardName,
  string CardNumber,
  string Expiration,
  string Cvv,
  int PaymentMethod
);
