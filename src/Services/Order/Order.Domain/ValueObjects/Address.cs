namespace Order.Domain.ValueObjects;

/// <summary>
/// Represents a customer's address information as a descriptive value object.
/// Defined as record to enforce inmutability and help to compare equality based on values.
/// </summary>
public record Address
{
  public string FirstName { get; } = default!;
  public string LastName { get; } = default!;
  public string? EmailAddress { get; } = default!;
  public string AddressLine { get; } = default!;
  public string Country { get; } = default!;
  public string State { get; } = default!;
  public string ZipCode { get; } = default!;

  protected Address() { }

  private Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
  {
    FirstName = firstName;
    LastName = lastName;
    EmailAddress = emailAddress;
    AddressLine = addressLine;
    Country = country;
    State = state;
    ZipCode = zipCode;
  }

  /// <summary>
  /// Provides a clear and domain specific way to create Address instance.
  /// </summary>
  /// <returns>Address value object instance.</returns>
  public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
    ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

    return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
  }

}
