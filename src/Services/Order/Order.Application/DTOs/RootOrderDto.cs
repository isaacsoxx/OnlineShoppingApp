using Order.Domain.Enums;

namespace Order.Application.DTOs;

/// <summary>
/// Represents complete [RootOrder].
/// </summary>
public record RootOrderDto(
  Guid Id,
  Guid CustomerId,
  string OrderName,
  AddressDto ShippingAddress,
  AddressDto BillingAddress,
  PaymentDto Payment,
  OrderStatus Status,
  List<OrderItemDto> OrderItems
);