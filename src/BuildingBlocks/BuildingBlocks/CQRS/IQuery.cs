using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Abstraction trigger for any database read operation.
/// </summary>
/// <typeparam name="TResponse">Generic response type.</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{

}
