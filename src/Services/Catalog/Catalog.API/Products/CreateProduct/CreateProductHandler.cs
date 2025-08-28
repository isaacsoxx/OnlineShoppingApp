namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Command and state modification data that is called from an outer layer (API).
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Category"></param>
/// <param name="Description"></param>
/// <param name="ImageFile"></param>
/// <param name="Price"></param>
/// <returns>Result that is sent back to the outer layer containing information about the operation.</returns>
public record CreateProductCommand
(
  Guid Id,
  string Name,
  List<string> Category,
  string Description,
  string ImageFile,
  decimal Price
) : ICommand<CreateProductResult>;

/// <summary>
/// Defines what information should be sent within the result of the command operation.
/// </summary>
/// <param name="Id"></param>
/// <returns>Id of the created product.</returns>
public record CreateProductResult(Guid Id);

/// <summary>
/// Defines validation rules that will be applied to any incoming command type <CreateProductCommand>
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required.");
    RuleFor(command => command.Category).NotEmpty().WithMessage("Category is required.");
    RuleFor(command => command.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
    RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
  }
}

/// <summary>
/// Defines handler class from any incoming `CreateProductCommand` request type.
/// </summary>
/// <param name="session">Abstraction for database operation handler.</param>
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
  /// <summary>
  /// Handles business logic to create a product.
  /// </summary>
  /// <param name="request"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
  {
    /* First, create the product entity from command object. */
    var product = new Product
    {
      Name = command.Name,
      Category = command.Category,
      Description = command.Description,
      ImageFile = command.ImageFile,
      Price = command.Price
    };

    /* Second, save the object into the database. */
    session.Store(product);
    await session.SaveChangesAsync(cancellationToken);

    /* Third, return the result of the operation. <CreateProductResult> */
    return new CreateProductResult(product.Id);
  }
}
