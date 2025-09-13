
namespace Order.Domain.Abstractions;

/// <summary>
/// Base aggregate class related operations.
/// </summary>
/// <typeparam name="TId">Implementation identifier type variation.</typeparam>
public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
  /// <summary>
  /// Readonly variation of domain events for each base class inheritance
  /// </summary>
  private readonly List<IDomainEvent> _domainEvents = new();

  /// <summary>
  /// Getter for domain events as Readonly.
  /// </summary>
  public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

  /// <summary>
  /// Add single domain event to the list of events aggregate can handle.
  /// </summary>
  /// <param name="domainEvent">Event to be added.</param>
  public void AddDomainEvent(IDomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  /// <summary>
  /// Method to clear domain events once they've been de queued.
  /// </summary>
  public IDomainEvent[] ClearDomainEvents()
  {
    IDomainEvent[] deQueuedEvents = _domainEvents.ToArray();
    _domainEvents.Clear();
    return deQueuedEvents;
  }
}
