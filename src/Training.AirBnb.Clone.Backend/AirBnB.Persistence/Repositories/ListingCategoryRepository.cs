using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class ListingCategoryRepository(AppDbContext dbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<ListingCategory, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()),
        IListingCategoryRepository
{
    public new IQueryable<ListingCategory> Get(Expression<Func<ListingCategory, bool>>? predicate = default,
        bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
}