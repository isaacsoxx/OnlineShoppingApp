namespace Catalog.API.Models;

public class Product
{
  public Guid Id { get; set; }
  public string Name { get; set; } = String.Empty;
  public List<string> Category { get; set; } = new();
  public string Description { get; set; } = String.Empty;
  public string ImageFile { get; set; } = string.Empty;
  public decimal Price { get; set; }
  
}
