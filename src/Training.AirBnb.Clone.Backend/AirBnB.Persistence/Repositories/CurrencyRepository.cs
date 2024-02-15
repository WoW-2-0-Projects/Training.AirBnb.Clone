using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Repository implementation for accessing currency-related data.
///  /// Initializes a new instance of the <see cref="CurrencyRepository"/> class.
/// </summary>
/// 
/// <param name="appDbContext">The application database context.</param>
/// <param name="cacheBroker">The cache broker.</param>
public class CurrencyRepository(AppDbContext appDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<Currency, AppDbContext>(appDbContext, cacheBroker, new CacheEntryOptions()), ICurrencyRepository
{
    /// <summary>
    /// Retrieves currencies based on an optional predicate expression and optional tracking settings.
    /// </summary>
    /// <param name="predicate">Optional predicate expression to filter currencies.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of currencies.</returns>
    IQueryable<Currency> ICurrencyRepository.Get(Expression<Func<Currency, bool>>? predicate, bool asNoTracking)
        => base.Get(predicate, asNoTracking);
}
