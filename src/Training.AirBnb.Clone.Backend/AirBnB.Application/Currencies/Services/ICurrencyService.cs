using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Currencies.Services;

/// <summary>
/// Interface for accessing currency-related data services.
/// </summary>
public interface ICurrencyService
{
    /// <summary>
    /// Retrieves currencies based on pagination options and optional tracking settings.
    /// </summary>
    /// <param name="filePagination">Pagination settings for querying currencies.</param>
    /// <param name="asNoTracking">Flag indicating whether to disable change tracking (default: false).</param>
    /// <returns>An IQueryable collection of currencies.</returns>
    IQueryable<Currency> Get(FilterPagination filePagination, bool asNoTracking = false);
}

