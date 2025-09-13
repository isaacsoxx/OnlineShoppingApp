namespace Order.Domain.Abstractions;

/// <summary>
/// Generic version for an aggregate.
/// </summary>
/// <typeparam name="T">Generic typing for variety of entities on the domain.</typeparam>
public interface IAggregate<T> : IAggregate, IEntity<T>
{

}

/// <summary>
/// Agregate is defined from an entity, being able to handle domain events. 
/// </summary>
public interface IAggregate : IEntity
{
  /// <summary>
  /// Domain events the aggregate can handle.
  /// </summary>
  IReadOnlyList<IDomainEvent> DomainEvents { get; }

  /// <summary>
  /// Allows us to clear domain events.
  /// </summary>
  IDomainEvent[] ClearDomainEvents();
}
