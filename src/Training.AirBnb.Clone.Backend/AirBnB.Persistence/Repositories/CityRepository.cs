using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class CityRepository(AppDbContext appDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<City, AppDbContext>(appDbContext, cacheBroker, new CacheEntryOptions()), ICityRepository
{
    public new IQueryable<City> Get(Expression<Func<City, bool>>? predicate = default, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
}