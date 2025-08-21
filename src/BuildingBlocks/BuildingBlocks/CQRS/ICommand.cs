using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Abstraction trigger for any database state modification operation with no response.
/// </summary>
public interface ICommand : ICommand<Unit>
{

}

/// <summary>
/// Abstraction trigger for any database state modification operation.
/// </summary>
/// <typeparam name="TResponse">Response details of the operation result.</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
