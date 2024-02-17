using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

public class CurrencyRepository(AppDbContext appDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<Currency, AppDbContext>(appDbContext, cacheBroker, new CacheEntryOptions()), ICurrencyRepository
{
    IQueryable<Currency> ICurrencyRepository.Get(Expression<Func<Currency, bool>>? predicate, bool asNoTracking)
        => base.Get(predicate, asNoTracking);
}
