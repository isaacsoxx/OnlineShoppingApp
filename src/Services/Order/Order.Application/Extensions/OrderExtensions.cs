namespace Order.Application.Extensions;

public static class OrderExtensions
{
  public static IEnumerable<RootOrderDto> ToRootOrderDtoList(this IEnumerable<RootOrder> rootOrders)
  {
    return rootOrders.Select(rootOrder => new RootOrderDto(
        Id: rootOrder.Id.Value,
        CustomerId: rootOrder.CustomerId.Value,
        OrderName: rootOrder.OrderName.Value,
        ShippingAddress: rootOrder.ShippingAddress.ToAddressDto(),
        BillingAddress: rootOrder.BillingAddress.ToAddressDto(),
        Payment: rootOrder.Payment.ToPaymentDto(),
        Status: rootOrder.Status,
        OrderItems: rootOrder.OrderItems.ToOrderItemDtoList()
      ));
  }

  public static Address ToAddressDomain(this AddressDto addressDto)
  {
    return Address.Of(
      addressDto.FirstName,
      addressDto.LastName,
      addressDto.EmailAddress,
      addressDto.AddressLine,
      addressDto.Country,
      addressDto.State,
      addressDto.ZipCode);
  }

  public static AddressDto ToAddressDto(this Address address)
  {
    return new AddressDto(
      address.FirstName,
      address.LastName,
      address.EmailAddress ?? default!,
      address.AddressLine,
      address.Country,
      address.State,
      address.ZipCode
    );
  }

  public static Payment ToPaymentDomain(this PaymentDto paymentDto)
  {
    return Payment.Of(paymentDto.CardName, paymentDto.CardNumber, paymentDto.Expiration, paymentDto.Cvv, paymentDto.PaymentMethod);
  }

  public static PaymentDto ToPaymentDto(this Payment payment)
  {
    return new PaymentDto(payment.CardName ?? default!, payment.CardNumber, payment.Expiration, payment.CVV, payment.PaymentMethod);
  }

  public static List<OrderItemDto> ToOrderItemDtoList(this IReadOnlyList<OrderItem> orderItems)
  {
    return orderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList();
  }
}
