using Order.Application.Orders.Queries.GetOrdersByCustomer;

namespace Order.API.Endpoints;

public record GetOrderByCustomerResponse(IEnumerable<RootOrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
    {
      var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
      var response = result.Adapt<GetOrdersByNameResponse>();

      return Results.Ok(response);
    })
    .WithName("GetOrdersByCustomer")
    .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .WithSummary("Get orders by customer Id.")
    .WithDescription("Get orders by customer Id.");
  }
}
