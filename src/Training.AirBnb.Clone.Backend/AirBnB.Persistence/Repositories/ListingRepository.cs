using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using EFCoreSecondLevelCacheInterceptor;

namespace AirBnB.Persistence.Repositories;

public class ListingRepository(AppDbContext dbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<Listing, AppDbContext>(
        dbContext, cacheBroker,
        new CacheEntryOptions()
        ), IListingRepository
{
    public new IQueryable<Listing> Get(
        Expression<Func<Listing, bool>>? predicate = default, 
        bool asNoTracking = false)
        => base.Get(predicate, asNoTracking)
            .Cacheable(CacheExpirationMode.Sliding, TimeSpan.FromMinutes(5));
    
    public new ValueTask<Listing?> GetByIdAsync(
        Guid listingId, bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
        => base.GetByIdAsync(listingId, asNoTracking, cancellationToken);

    public new ValueTask<IList<Listing>> GetByIdsAsync(
        IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public new ValueTask<Listing> CreateAsync(
        Listing listing, bool saveChanges = true, 
        CancellationToken cancellationToken = default)
        => base.CreateAsync(listing, saveChanges, cancellationToken);

    public new ValueTask<Listing> UpdateAsync(
        Listing listing, bool saveChanges = true,
        CancellationToken cancellationToken = default)
        => base.UpdateAsync(listing, saveChanges, cancellationToken);
    
    public new ValueTask<Listing?> DeleteAsync(
        Listing listing, bool saveChanges = true,
        CancellationToken cancellationToken = default)
        => base.DeleteAsync(listing, saveChanges, cancellationToken);
    
    public new ValueTask<Listing?> DeleteByIdAsync(
        Guid listingId, bool saveChanges = true,
        CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(listingId, saveChanges, cancellationToken);
}