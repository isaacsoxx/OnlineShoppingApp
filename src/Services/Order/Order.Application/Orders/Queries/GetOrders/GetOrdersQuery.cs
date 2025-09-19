namespace Order.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginatedRequest PaginatedOrders) : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginatedResult<RootOrderDto> Orders);
