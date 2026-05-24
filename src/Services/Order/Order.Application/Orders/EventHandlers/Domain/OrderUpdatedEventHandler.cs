namespace Order.Application.Orders.EventHandlers.Domain;

/// <summary>
/// Represents the handler of [OrderUpdatedEvent] to run logic after event is triggered.
/// </summary>
public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
  public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
  {
    logger.LogInformation("Domain Event handled {DomainEvent}", notification.GetType().Name);
    return Task.CompletedTask;
  }
}
