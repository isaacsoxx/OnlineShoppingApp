namespace Order.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginatedOrders) : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginatedResult<RootOrderDto> Orders);
