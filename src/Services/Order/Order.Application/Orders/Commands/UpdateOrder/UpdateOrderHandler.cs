namespace Order.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
  public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
  {
    var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.RootOrders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

    if (order is null) throw new OrderNotFoundException(command.Order.Id);

    UpdateOrderWithNewValues(order, command.Order);

    dbContext.RootOrders.Update(order);
    await dbContext.SaveChangesAsync(cancellationToken);

    return new UpdateOrderResult(true);
  }

  private void UpdateOrderWithNewValues(RootOrder order, RootOrderDto orderDto)
  {
    order.Update(
      orderName: OrderName.Of(orderDto.OrderName),
      shippingAddress: orderDto.ShippingAddress.ToAddressDomain(),
      billingAddress: orderDto.BillingAddress.ToAddressDomain(),
      payment: orderDto.Payment.ToPaymentDomain(),
      status: orderDto.Status
    );
  }
}
