using MediatR;

namespace Order.Domain.Abstractions;

/// <summary>
/// Abstraction to perform and exposing domain events operations relying on MediatR,
/// allowing domain events to be triggered though MediatR handlers.
/// </summary>
public interface IDomainEvent : INotification
{
  /// <summary>
  /// Event identifier.
  /// </summary>
  Guid EventId => Guid.NewGuid();

  /// <summary>
  /// Timestamp indicating when the event is triggered.
  /// </summary>
  public DateTime OcurredOn => DateTime.Now;

  /// <summary>
  /// Identifier of the class name triggering this event.
  /// </summary>
  public string EventType => GetType().AssemblyQualifiedName;
}
