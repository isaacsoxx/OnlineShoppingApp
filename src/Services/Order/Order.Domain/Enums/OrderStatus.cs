namespace Order.Domain.Enums;

/// <summary>
/// Represents the various possibles states for an [RootOrder].
/// </summary>
public enum OrderStatus
{
  Draft = 1,
  Pending = 2,
  Completed = 3,
  Cancelled = 4,
}
