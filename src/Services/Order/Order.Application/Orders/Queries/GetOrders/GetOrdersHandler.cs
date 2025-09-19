namespace Order.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
  public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
  {
    var pageIndex = query.PaginatedOrders.PageIndex;
    var pageSize = query.PaginatedOrders.PageSize;
    var totalCount = await dbContext.RootOrders.LongCountAsync(cancellationToken);

    var orders = await dbContext.RootOrders
                  .Include(o => o.OrderItems)
                  .AsNoTracking()
                  .OrderBy(o => o.OrderName)
                  .Skip(pageSize * pageIndex)
                  .Take(pageSize)
                  .ToListAsync(cancellationToken);

    return new GetOrdersResult(
      new PaginatedResult<RootOrderDto>(
        pageIndex,
        pageSize,
        totalCount,
        orders.ToRootOrderDtoList()
      )
    );
  }
}
