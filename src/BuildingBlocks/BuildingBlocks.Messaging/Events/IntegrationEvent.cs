namespace BuildingBlocks.Messaging.Events;

/// <summary>
/// Provides common properties that all integration events will inherit.
/// </summary>
public record IntegrationEvent
{
  public Guid Id => Guid.NewGuid();
  public DateTime OcurredOn => DateTime.Now;
  public string EventType => GetType().AssemblyQualifiedName;
}
