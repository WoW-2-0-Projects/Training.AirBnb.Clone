using System.Linq.Expressions;
using AirBnB.Application.Locations.Models;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Locations.Services;

/// <summary>
/// Interface for accessing country-related data.
/// </summary>
public interface ICountryService
{
    /// <summary>
    /// Retrieves countries based on an optional predicate expression.
    /// </summary>
    /// <param name="predicate">Optional predicate expression to filter countries.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of countries.</returns>
    IQueryable<Country> Get(Expression<Func<Country, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Retrieves countries based on pagination and optional tracking settings.
    /// </summary>
    /// <param name="filterPagination">Pagination and filtering settings.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of countries.</returns>
    IQueryable<Country> Get(FilterPagination filterPagination, bool asNoTracking = false);
}
