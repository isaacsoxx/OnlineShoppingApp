namespace Order.Application.Orders.Commands.CreateOrder;

/// <summary>
/// Handles responsability to process the [CreateOrderCommand] and interact with the domain model and DB.
/// </summary>
public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
  /// <summary>
  /// Represents a mediator between the incoming command and domain model.
  /// It will be triggered from the MediatR command defined in [CreateOrderCommand].
  /// </summary>
  public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
  {
    var order = CreateNewOrder(command.Order);

    dbContext.RootOrders.Add(order);
    await dbContext.SaveChangesAsync(cancellationToken);

    return new CreateOrderResult(order.Id.Value);
  }

  private RootOrder CreateNewOrder(RootOrderDto orderDto)
  {
    var newOrder = RootOrder.Create(
      id: OrderId.Of(Guid.NewGuid()),
      customerId: CustomerId.Of(orderDto.CustomerId),
      orderName: OrderName.Of(orderDto.OrderName),
      shippingAddress: orderDto.ShippingAddress.ToAddressDomain(),
      billingAddress: orderDto.BillingAddress.ToAddressDomain(),
      payment: orderDto.Payment.ToPaymentDomain()
    );

    foreach (var orderItemDto in orderDto.OrderItems) newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);

    return newOrder;
  }
}
