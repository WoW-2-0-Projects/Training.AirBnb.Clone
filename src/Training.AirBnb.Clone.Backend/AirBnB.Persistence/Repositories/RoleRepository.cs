using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

public class RoleRepository(IdentityDbContext dbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<Role, IdentityDbContext>(dbContext, cacheBroker), IRoleRepository
{
    public new IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate, bool asNoTracking)
    {
        return base.Get(predicate, asNoTracking);
    }
}
