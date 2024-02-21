using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for accessing city-related data from a repository.
/// </summary>
public interface ICityRepository
{
    /// <summary>
    /// Retrieves cities based on an optional predicate expression and optional tracking settings.
    /// </summary>
    /// <param name="predicate">Optional predicate expression to filter cities.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of cities.</returns>
    IQueryable<City> Get(Expression<Func<City, bool>>? predicate = default, bool asNoTracking = false);
}
