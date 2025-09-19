namespace Order.Application.Orders.EventHandlers;

/// <summary>
/// Represents the handler of [OrderCreatedEvent] to run logic after event is triggered.
/// </summary>
public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
  public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
  {
    logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
    return Task.CompletedTask;
  }
}
