using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Repository implementation for accessing city-related data.
/// Initializes a new instance of the <see cref="CityRepository"/> class.
/// </summary>
/// <param name="appDbContext">The application database context.</param>
/// <param name="cacheBroker">The cache broker.</param>
public class CityRepository(AppDbContext appDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<City, AppDbContext>(appDbContext, cacheBroker, new CacheEntryOptions()), ICityRepository
{
    /// <summary>
    /// Retrieves cities based on an optional predicate expression and optional tracking settings.
    /// </summary>
    /// <param name="predicate">Optional predicate expression to filter cities.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of cities.</returns>
    public new IQueryable<City> Get(Expression<Func<City, bool>>? predicate = default, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
}