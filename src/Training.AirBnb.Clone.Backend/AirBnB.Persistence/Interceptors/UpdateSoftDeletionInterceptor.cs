using AirBnB.Domain.Brokers;
using AirBnB.Domain.Common.Entities;
using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AirBnB.Persistence.Interceptors;

/// <summary>
/// Represents a custom interceptor that automatically handles
/// soft deletion audit for entities before saving changes to the database.
/// </summary>
/// <param name="userContextProvider"></param>
public class UpdateSoftDeletionInterceptor(IRequestUserContextProvider userContextProvider) 
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var softDeletedEntries = 
            eventData.Context!.ChangeTracker.Entries<ISoftDeletedEntity>().ToList();

        var deletionAuditableEntries = 
            eventData.Context!.ChangeTracker.Entries<IDeletionAuditableEntity>().ToList();
        
        // Set DeletedByUserId property for deleted entities implementing IDeletionAuditableEntity
        deletionAuditableEntries.ForEach(entry =>
        {
            if (entry.State != EntityState.Deleted) return;
            
            entry.State = EntityState.Modified;
                
            entry.Property(nameof(IDeletionAuditableEntity.DeletedByUserId)).CurrentValue =
                userContextProvider.GetUserId();
        });
        
        // Set IsDeleted and DeletedTime properties for deleted entities implementing ISoftDeletedEntity interface
        softDeletedEntries.ForEach(entry =>
        {
            if (entry.State != EntityState.Deleted) return;
            
            entry.State = EntityState.Modified;
            
            var ownedTypes = entry.References.Where(entity => entity.Metadata.TargetEntityType.IsOwned()).ToList();
            ownedTypes.ForEach(entity => entity.TargetEntry.State = EntityState.Modified);

            entry.Property(nameof(ISoftDeletedEntity.IsDeleted)).CurrentValue = true;
            entry.Property(nameof(ISoftDeletedEntity.DeletedTime)).CurrentValue = DateTimeOffset.UtcNow;
        });
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}