using AirBnB.Domain.Brokers;
using AirBnB.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AirBnB.Persistence.Interceptors;

/// <summary>
/// Represents a custom interceptor that automatically updates
/// auditable properties of entities before saving changes to the database.
/// </summary>
/// <param name="userContextProvider"></param>
public class UpdateAuditableInterceptor(IRequestUserContextProvider userContextProvider) 
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var auditableEntries = 
            eventData.Context!.ChangeTracker.Entries<IAuditableEntity>().ToList();
        
        var creationAuditableEntries = 
            eventData.Context!.ChangeTracker.Entries<ICreationAuditableEntity>().ToList();

        var modificationAuditableEntries =
            eventData.Context!.ChangeTracker.Entries<IModificationAuditableEntity>().ToList();
        
        // Set CreatedTime and ModifiedTime values for entities implementing IAuditableEntity.
        auditableEntries.ForEach(entry =>
        {
            if (entry.State == EntityState.Modified)
                entry.Property(nameof(IAuditableEntity.ModifiedTime)).CurrentValue = DateTimeOffset.UtcNow;

            if (entry.State == EntityState.Added)
                entry.Property(nameof(IAuditableEntity.CreatedTime)).CurrentValue = DateTimeOffset.UtcNow;
        });
        
        // Set CreatedByUserId property for entities implementing ICreationAuditableEntity
        creationAuditableEntries.ForEach(entry =>
        {
            if (entry.State == EntityState.Added)
                entry.Property(nameof(ICreationAuditableEntity.CreatedByUserId)).CurrentValue =
                    userContextProvider.GetUserId();
        });
        
        // Set ModifiedBUserId property for entities implementing ICreationAuditableEntity
        modificationAuditableEntries.ForEach(entry =>
        {
            if (entry.State == EntityState.Modified)
                entry.Property(nameof(IModificationAuditableEntity.ModifiedBUserId)).CurrentValue =
                    userContextProvider.GetUserId();
        });
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}