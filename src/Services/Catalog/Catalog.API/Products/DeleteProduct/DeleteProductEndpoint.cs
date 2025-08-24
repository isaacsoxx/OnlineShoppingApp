
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new DeleteProductCommand(id));
      var response = result.Adapt<DeleteProductResponse>();
      return Results.Ok(response);
    })
    .WithName("DeleteProducts")
    .Produces<DeleteProductResponse>()
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Deletes a product based on their Id.")
    .WithDescription("Deletes a product based on their Id.");
  }
}
