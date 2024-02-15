using AirBnB.Domain.Entities;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories.Interfaces;
/// <summary>
/// Interface for accessing currency-related data from a repository.
/// </summary>
public interface ICurrencyRepository
{
    /// <summary>
    /// Retrieves currencies based on an optional predicate expression and optional tracking settings.
    /// </summary>
    /// <param name="predicate">Optional predicate expression to filter currencies.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of currencies.</returns>
    IQueryable<Currency> Get(Expression<Func<Currency, bool>>? predicate = default, bool asNoTracking = false);
}
