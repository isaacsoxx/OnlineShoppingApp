using Order.Application.Orders.Queries.GetOrdersByName;

namespace Order.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<RootOrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
    {
      var result = await sender.Send(new GetOrdersByNameQuery(orderName));
      var response = result.Adapt<GetOrdersByNameResponse>();
      return Results.Ok(response);
    })
    .WithName("GetOrdersByName")
    .Produces<GetOrdersByName>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .WithSummary("Get orders by order name.")
    .WithDescription("Get orders by order name.");
  }
}
