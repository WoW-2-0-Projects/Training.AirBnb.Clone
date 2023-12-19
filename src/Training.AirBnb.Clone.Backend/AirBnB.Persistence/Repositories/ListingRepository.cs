using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class ListingRepository(IdentityDbContext dbContext) : EntityRepositoryBase<Listing, IdentityDbContext>(dbContext), IListingRepository
{
    public IQueryable<Listing> Get(Expression<Func<Listing, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<Listing?> GetByIdAsync(Guid listingId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(listingId, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<Listing>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public ValueTask<Listing> CreateAsync(Listing listing, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(listing, saveChanges, cancellationToken);
    }

    public ValueTask<Listing> UpdateAsync(Listing listing, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(listing, saveChanges, cancellationToken);
    }

    public ValueTask<Listing?> DeleteAsync(Listing listing, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(listing, saveChanges, cancellationToken);
    }

    public ValueTask<Listing?> DeleteByIdAsync(Guid listingId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(listingId, saveChanges, cancellationToken);
    }
}