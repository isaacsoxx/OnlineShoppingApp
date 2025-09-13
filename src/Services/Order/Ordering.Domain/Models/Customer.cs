namespace Order.Domain.Models;

/// <summary>
/// Represents an individual item within a [RootOrder], containing order business domain properties.
/// </summary>
public class Customer : Entity<CustomerId>
{
  public string Name { get; private set; } = default!;
  public string Email { get; private set; } = default!;

  /// <summary>
  /// Creates instance for itself, ensuring all necessary validations are applied during creation.
  /// </summary>
  /// <returns>Instance of [Customer] entity, based on business domain rules.</returns>
  public static Customer Create(CustomerId id, string name, string email)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(name);
    ArgumentException.ThrowIfNullOrWhiteSpace(email);

    return new Customer { Id = id, Name = name, Email = email };
  }
}
