using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class ListingMediaFileRepository(AppDbContext dbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<ListingMediaFile, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), 
        IListingMediaFileRepository
{
    public new IQueryable<ListingMediaFile> Get(Expression<Func<ListingMediaFile, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new ValueTask<ListingMediaFile?> GetByIdAsync(Guid mediaFileId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(mediaFileId, asNoTracking, cancellationToken);
    }

    public new ValueTask<ListingMediaFile> CreateAsync(ListingMediaFile listingMediaFile, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(listingMediaFile, saveChanges, cancellationToken);
    }

    public new ValueTask<ListingMediaFile> UpdateAsync(ListingMediaFile listingMediaFile, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(listingMediaFile, saveChanges, cancellationToken);
    }

    public async ValueTask UpdateRangeAsync(List<ListingMediaFile> listingMediaFiles, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        foreach (var media in listingMediaFiles[..^1])
            await UpdateAsync(media, false, cancellationToken);

        if (listingMediaFiles.Count > 0)
            await UpdateAsync(listingMediaFiles[^1], saveChanges, cancellationToken);
    }

    public new ValueTask<ListingMediaFile?> DeleteAsync(ListingMediaFile listingMediaFile, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(listingMediaFile, saveChanges, cancellationToken);
    }

    public new ValueTask<ListingMediaFile?> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }
}