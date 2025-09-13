namespace Order.Domain.Events;

/// <summary>
/// Signals that a new order has been created.
/// Helps us to record its creation for other processes can react.
/// </summary>
public record OrderCreatedEvent(RootOrder order) : IDomainEvent;
