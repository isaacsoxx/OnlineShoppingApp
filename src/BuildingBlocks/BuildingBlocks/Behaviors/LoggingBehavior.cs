using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// Generic handler for logging details of incoming operation through the request pipeline.
/// </summary>
/// <typeparam name="TRequest">Abstraction of the incoming request.</typeparam>
/// <typeparam name="TResponse">Abstraction of the response that will be returned.</typeparam>
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull, IRequest<TResponse>
where TResponse : notnull
{
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    /* Log details of any request at the start */
    logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestData={Request}", typeof(TRequest).Name, typeof(TResponse).Name, request);

    /* Instantiate an stopwatch for tracking the time length of the operation. */
    Stopwatch timer = new Stopwatch();
    timer.Start();

    /* Continue the pipeline execution through the next pipeline behavior and wait for a response to be returned later. */
    var response = await next();

    /* Stop the stopwatch and get the seconds elapsed. */
    timer.Stop();
    var timeTaken = timer.Elapsed;

    /* If the operation took longer than 3 seconds then log a warning with request, response and tim taken details */
    if (timeTaken.Seconds > 3) logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.", typeof(TRequest).Name, timeTaken.Seconds);

    /* Log details of the response once it has finished execution. */
    logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

    /* Return stored response. */
    return response;
  }
}
