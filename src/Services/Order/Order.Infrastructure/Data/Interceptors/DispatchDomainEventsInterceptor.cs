using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Order.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{

  /// <summary>
  /// Intercepts the SaveChanges operation to dispatch domain events.
  /// </summary>
  /// <param name="eventData">Contextual information about the SaveChanges event.</param>
  /// <param name="result">The result of the SaveChanges operation.</param>
  /// <returns>The potentially modified result of the SaveChanges operation.</returns>
  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
  {
    DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
    return base.SavingChanges(eventData, result);
  }


  /// <summary>
  /// Asynchronously intercepts the SaveChanges operation to dispatch domain events.
  /// </summary>
  /// <param name="eventData">Contextual information about the SaveChanges event.</param>
  /// <param name="result">The result of the SaveChanges operation.</param>
  /// <returns>The potentially modified result of the SaveChanges operation.</returns>
  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
  {
    await DispatchDomainEvents(eventData.Context);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  /// <summary>
  /// Retrieves any entity from the [DBContext] that includes any domain event, to dispatch them using MediatR.
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public async Task DispatchDomainEvents(DbContext? context)
  {
    if (context is null) return;

    var aggregates = context.ChangeTracker
      .Entries<IAggregate>()
      .Where(a => a.Entity.DomainEvents.Any())
      .Select(a => a.Entity);

    var domainEvents = aggregates
      .SelectMany(a => a.DomainEvents)
      .ToList();

    aggregates.ToList().ForEach(a => a.ClearDomainEvents());
    foreach (var domainEvent in domainEvents) await mediator.Publish(domainEvent);
  }
}
