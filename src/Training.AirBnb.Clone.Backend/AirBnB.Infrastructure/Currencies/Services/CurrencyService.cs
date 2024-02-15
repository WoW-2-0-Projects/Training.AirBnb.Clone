using AirBnB.Application.Currencies.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Infrastructure.Currencies.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrencyService
    {
        public IQueryable<Currency> Get(FilterPagination filePagination, bool asNoTracking = false)
        => currencyRepository.Get(asNoTracking: asNoTracking)
            .Skip((int)((filePagination.PageToken - 1) * filePagination.PageSize))
            .Take((int)filePagination.PageSize);
    }
}
