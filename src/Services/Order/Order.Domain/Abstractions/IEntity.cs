namespace Order.Domain.Abstractions;

/// <summary>
/// Generic abstraction with unique identifier with the type <T>.
/// </summary>
/// <typeparam name="T">Generic typing for variety of entities on the domain.</typeparam>
public interface IEntity<T> : IEntity
{
  public T Id { get; set; }
}

/// <summary>
/// Abstraction of common properties for each domain entity.
/// </summary>
public interface IEntity
{
  public DateTime? CreatedAt { get; set; }
  public string? CreatedBy { get; set; }
  public DateTime? LastModified { get; set; }
  public string? LastModifiedBy { get; set; }
}
