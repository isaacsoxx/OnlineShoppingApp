namespace Order.Domain.Events;

/// <summary>
/// Signals that an existing order has been updated.
/// Helps us to record its creation for other processes can react.
/// </summary>
public record OrderUpdatedEvent(RootOrder order) : IDomainEvent;
