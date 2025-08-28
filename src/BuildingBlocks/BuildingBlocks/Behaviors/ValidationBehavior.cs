using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// Generic validator pipeline for any product request.
/// </summary>
/// <typeparam name="TRequest">Abstraction of the request.</typeparam>
/// <typeparam name="TResponse">Abstraction of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
  /// <summary>
  /// Core logic of validation step of the pipeline, avoids invalid requests to reach business logic for command handlers.
  /// </summary>
  /// <param name="request">Incoming request of the client.</param>
  /// <param name="next">Next request handle delegate (Next pipeline behavior).</param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    /* Creates a validations context based on the request. */
    var context = new ValidationContext<TRequest>(request);

    /* Runs validation for each validator. */
    var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

    /* Select all the validation errors found. */
    var failures = validationResults
      .Where(r => r.Errors.Any())
      .SelectMany(r => r.Errors)
      .ToList();

    /* Throws exception in case any failure on the validation was found. */
    if (failures.Any()) throw new ValidationException(failures);

    /* If not, then it will continue the pipeline execution through the next pipeline behavior. */
    return await next();
  }
}
