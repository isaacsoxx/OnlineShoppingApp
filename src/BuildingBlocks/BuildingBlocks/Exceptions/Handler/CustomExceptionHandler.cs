using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

/// <summary>
/// Custom generic exception handler.
/// </summary>
public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
  {
    /* Log the error form the exception object. */
    logger.LogError("Error message: {exceptionMessage}, Time of ocurrence {time}", exception.Message, DateTime.UtcNow);

    /* Use pattern matching to identify exception type. */
    /* Define anonymous object to match any of below depending on its type. */
    (string Detail, string Title, int StatusCode) details = exception switch
    {
      InternalServerException => (
        exception.Message,
        exception.GetType().Name,
        context.Response.StatusCode = StatusCodes.Status500InternalServerError
      ),
      ValidationException => (
        exception.Message,
        exception.GetType().Name,
        context.Response.StatusCode = StatusCodes.Status400BadRequest
      ),
      BadRequestException => (
        exception.Message,
        exception.GetType().Name,
        context.Response.StatusCode = StatusCodes.Status400BadRequest
      ),
      NotFoundException => (
        exception.Message,
        exception.GetType().Name,
        context.Response.StatusCode = StatusCodes.Status404NotFound
      ),
       _ => (
        exception.Message,
        exception.GetType().Name,
        context.Response.StatusCode = StatusCodes.Status500InternalServerError
       )
    };

    /* Create JSON formatted response using the values on the anonymous object <details>. */
    var errorResponse = new ProblemDetails
    {
      Title = details.Title,
      Detail = details.Detail,
      Status = details.StatusCode,
      Instance = context.Request.Path
    };

    /* Provide trace details is if desired, */
    errorResponse.Extensions.Add("traceId", context.TraceIdentifier);

    /* If the error is caused due to validation error we can also provide that along the formatted JSON. */
    if (exception is ValidationException validationException) errorResponse.Extensions.Add("ValidationErrors", validationException.Errors);

    /* Return error response. */
    await context.Response.WriteAsJsonAsync(errorResponse, cancellationToken: cancellationToken);
    return true;
  }
}
