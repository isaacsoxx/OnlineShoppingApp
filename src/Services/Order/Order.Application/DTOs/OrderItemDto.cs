namespace Order.Application.DTOs;

/// <summary>
/// Represents essential information of each item in a [RootOrder].
/// </summary>
public record OrderItemDto(
  Guid OrderId,
  Guid ProductId,
  int Quantity,
  decimal Price
);