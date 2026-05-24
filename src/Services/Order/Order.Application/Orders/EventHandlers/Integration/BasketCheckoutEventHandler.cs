using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Order.Application.Orders.EventHandlers.Integration;

/// <summary>
/// Initiates the order creation process upon receiving basket-checkout-event.
/// </summary>
public class BasketCheckoutEventHandler : IConsumer<CheckoutBasketEvent>
{
  public Task Consume(ConsumeContext<CheckoutBasketEvent> context)
  {
    /* ToDo: Create new order and start order fullfillment process. */
    throw new NotImplementedException();
  }
}
