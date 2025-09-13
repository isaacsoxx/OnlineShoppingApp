namespace Order.Domain.Abstractions;

/// <summary>
/// Base generic entity class.
/// </summary>
/// <typeparam name="T">Implementations type variation.</typeparam>
public abstract class Entity<T> : IEntity<T>
{
  public T Id { get; set; }
  public DateTime? CreatedAt { get; set; }
  public string? CreatedBy { get; set; }
  public DateTime? LastModified { get; set; }
  public string? LastModifiedBy { get; set; }
}
