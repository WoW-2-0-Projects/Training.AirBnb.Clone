using AirBnB.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AirBnB.Persistence.Interceptors;

/// <summary>
/// Represents a custom interceptor that automatically sets the primary key of the entities
/// before they are added to the database
/// </summary>
public class UpdatePrimaryKeyInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var entities = eventData.Context!.ChangeTracker.Entries<IEntity>().ToList();
        
        // Set Primary keys of newly added entities.
        entities.ForEach(entry =>
        {
            if (entry.State == EntityState.Added)
                entry.Property(nameof(IEntity.Id)).CurrentValue = Guid.NewGuid();
        });
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}