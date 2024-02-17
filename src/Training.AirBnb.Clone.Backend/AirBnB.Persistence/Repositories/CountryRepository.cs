using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Repository implementation for accessing country-related data.
/// Initializes a new instance of the <see cref="CountryRepository"/> class.
/// </summary>
/// 
/// <param name="appDbContext">The application database context.</param>
/// <param name="cacheBroker">The cache broker.</param>
public class CountryRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<Country, AppDbContext>(appDbContext, cacheBroker, new CacheEntryOptions()), ICountryRepository
{
    public new IQueryable<Country> Get(Expression<Func<Country, bool>>? predicate = default, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
}