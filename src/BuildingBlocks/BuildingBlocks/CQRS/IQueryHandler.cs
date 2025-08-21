using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Abstraction handler for any database read operation with no response.
/// </summary>
/// <typeparam name="TQuery">Query including details to handle the request.</typeparam>
/// <typeparam name="TResponse">Response type indicating operation result.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
where TQuery : IQuery<TResponse>
where TResponse : notnull
{

}
