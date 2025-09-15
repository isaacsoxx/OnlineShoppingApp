using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Order.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
  /// <summary>
  /// Intercepts the SaveChanges operation to apply auditing logic before changes are persisted to the database.
  /// </summary>
  /// <param name="eventData">Contextual information about the SaveChanges event.</param>
  /// <param name="result">The result of the SaveChanges operation.</param>
  /// <returns>The potentially modified result of the SaveChanges operation.</returns>
  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
  {
    UpdateEntities(eventData.Context);
    return base.SavingChanges(eventData, result);
  }

  /// <summary>
  /// Asynchronously intercepts the SaveChanges operation to apply auditing logic before changes are persisted to the database.
  /// </summary>
  /// <param name="eventData">Contextual information about the SaveChanges event.</param>
  /// <param name="result">The result of the SaveChanges operation.</param>
  /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
  /// <returns>A task that represents the asynchronous interception operation, containing the potentially modified result of the SaveChanges operation.</returns>
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
  {
    UpdateEntities(eventData.Context);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }


  /// <summary>
  /// Validates and updates auditing members of the entity accordindly.
  /// </summary>
  /// <param name="context">Database context.</param>
  public void UpdateEntities(DbContext? context)
  {
    if (context is null) return;

    foreach (var entry in context.ChangeTracker.Entries<IEntity>())
    {
      if (entry.State == EntityState.Added)
      {
        entry.Entity.CreatedBy = "issx";
        entry.Entity.CreatedAt = DateTime.UtcNow;
      }

      if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
      {
        entry.Entity.LastModifiedBy = "issx";
        entry.Entity.LastModified = DateTime.UtcNow;
      }
    }
  }
}

/// <summary>
/// Extends functionality for entities changes validation.
/// </summary>
public static class Extensions
{
  /// <summary>
  /// Validates if any of the owned entities has updates or if one was added.
  /// </summary>
  /// <param name="entry">Entry to validate.</param>
  /// <returns></returns>
  public static bool HasChangedOwnedEntities(this EntityEntry entry) => entry.References.Any(r =>
    r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() && (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified)
  );
}
