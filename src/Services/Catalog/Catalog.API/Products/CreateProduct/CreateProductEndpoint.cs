namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Model that captures all the details needed for the operation.
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Category"></param>
/// <param name="Description"></param>
/// <param name="ImageFile"></param>
/// <param name="Price"></param>
/// <returns></returns>
public record CreateProductRequest(
  Guid Id,
  string Name,
  List<string> Category,
  string Description,
  string ImageFile,
  decimal Price
);

/// <summary>
/// Model that returns details needed of the result of the operation.
/// </summary>
/// <param name="Id">Identifier of the newly created product.</param>
/// <returns></returns>
public record CreateProductResponse(Guid Id);

/// <summary>
/// Contains API definitions for Create Product slice.
/// </summary>
public class CreateProductEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    /// <summary>
    /// Definition of the API route for creating a product.
    /// </summary>
    /// <param name="request">Containing details of the product to be created.</param>
    /// <param name="sender">Containing details of the operation result.</param>
    /// <returns></returns>
    app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
    {
      // First, convert the request to a command.
      var command = request.Adapt<CreateProductCommand>();

      // Second, send the command to trigger the handler containing the business logic.
      var result = await sender.Send(command);

      // Third, convert the result from the handler to a response that can be sent back to the client.
      var response = result.Adapt<CreateProductResponse>();

      // Fourth, return the response with specific status code for clarity and any relevant details back to the client.
      return Results.Created($"/products/{response.Id}", response);
    })
    .WithName("CreateProduct")
    .Produces<CreateProductResponse>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Create product inside catalog.")
    .WithDescription("Create product inside catalog");
  }
}
