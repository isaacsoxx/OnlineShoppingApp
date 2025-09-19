namespace Order.Application.DTOs;

/// <summary>
/// Represents [Address] descriptive value object.
/// </summary>
public record AddressDto(
  string FirstName,
  string LastName,
  string EmailAddress,
  string AddressLine,
  string Country,
  string State,
  string ZipCode
);
