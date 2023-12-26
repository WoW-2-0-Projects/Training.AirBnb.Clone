using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.Listings;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class ListingCategoryRepository(ListingsDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<ListingCategory, ListingsDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), IListingCategoryRepository
{
    public new ValueTask<IList<ListingCategory>> GetAsync(
        QuerySpecification<ListingCategory> querySpecification, 
        CancellationToken cancellationToken = default)
    {
        return base.GetAsync(querySpecification, cancellationToken);
    }
}