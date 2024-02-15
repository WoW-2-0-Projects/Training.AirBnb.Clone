using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for accessing country-related data from a repository.
/// </summary>
public interface ICountryRepository
{
    /// <summary>
    /// Retrieves countries based on an optional predicate expression and optional tracking settings.
    /// </summary>
    /// <param name="predicate">Optional predicate expression to filter countries.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of countries.</returns>
    IQueryable<Country> Get(Expression<Func<Country, bool>>? predicate = default, bool asNoTracking = false);
}
