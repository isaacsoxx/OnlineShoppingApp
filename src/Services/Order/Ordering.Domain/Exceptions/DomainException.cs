namespace Order.Domain.Exceptions;


/// <summary>
/// Custom exception for domain specific rules validation.
/// </summary>
public class DomainException : Exception
{
  public DomainException(string message) : base($"Domain Exception: \"{message}\" thrown from Domain Layer")
  {

  }
}
