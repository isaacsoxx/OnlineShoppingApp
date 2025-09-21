using BuildingBlocks.Pagination;
using Order.Application.Orders.Queries.GetOrders;

namespace Order.API.Endpoints;

public record GetOrdersResponse(PaginatedResult<RootOrderDto> Orders);

public class GetOrders : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
    {
      var result = await sender.Send(new GetOrdersQuery(request));
      var response = result.Adapt<GetOrdersResponse>();
      return Results.Ok(response);
    })
    .WithName("GetOrders")
    .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get paginated orders")
    .WithDescription("Get paginated orders");
  }
}
