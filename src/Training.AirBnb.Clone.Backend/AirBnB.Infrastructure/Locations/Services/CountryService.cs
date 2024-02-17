using System.Linq.Expressions;
using AirBnB.Application.Locations.Models;
using AirBnB.Application.Locations.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Extensions;
using AirBnB.Persistence.Repositories;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Locations.Services;

public class CountryService(ICountryRepository countryRepository) : ICountryService
{
    public IQueryable<Country> Get(Expression<Func<Country, bool>>? predicate = default, bool asNoTracking = false)
        => countryRepository.Get(predicate, asNoTracking);
    
    public IQueryable<Country> Get(FilterPagination filterPagination, bool asNoTracking = false)
    => countryRepository.Get(asNoTracking: asNoTracking)
            .Skip((int)((filterPagination.PageToken - 1) * filterPagination.PageSize))
            .Take((int)filterPagination.PageSize);
}