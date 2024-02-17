using System.Data.Common;
using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Locations.Services;

/// <summary>
/// Interface for accessing city-related data.
/// </summary>
public interface ICityService
{
    /// <summary>
    /// Retrieves cities based on pagination options and optional tracking settings.
    /// </summary>
    /// <param name="paginationOptions">Pagination settings for querying cities.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of cities.</returns>
    IQueryable<City> Get(FilterPagination paginationOptions, bool asNoTracking = false);
}
