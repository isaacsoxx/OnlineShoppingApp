using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Abstraction handler for any database state modification operation with no response.
/// </summary>
/// <typeparam name="TCommand">Command including objects to handle request.</typeparam>
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
where TCommand : ICommand<Unit>
{

}

/// <summary>
/// Abstraction handler for any database state modification operation with response details.
/// </summary>
/// <typeparam name="TCommand">Command including objects to handle request.</typeparam>
/// <typeparam name="TResponse">Response type indicating operation result.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
where TCommand : ICommand<TResponse>
where TResponse : notnull
{

}
