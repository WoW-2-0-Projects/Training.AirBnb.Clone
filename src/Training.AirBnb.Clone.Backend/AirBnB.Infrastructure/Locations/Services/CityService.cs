using AirBnB.Application.Locations.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Infrastructure.Locations.Services;

public class CityService(ICityRepository cityRepository) : ICityService
{
    public IQueryable<City> Get(FilterPagination paginationOptions, bool asNoTracking = false)
    => cityRepository.Get(asNoTracking: asNoTracking)
            .Skip((int)((paginationOptions.PageToken - 1) * paginationOptions.PageSize))
            .Take((int)paginationOptions.PageSize);
}