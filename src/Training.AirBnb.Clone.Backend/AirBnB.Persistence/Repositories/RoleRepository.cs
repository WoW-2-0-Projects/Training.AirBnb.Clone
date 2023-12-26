using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;
/// <summary>
/// This class represents a repository for handling Role entities, implementing IRoleRepository.
/// </summary>
public class RoleRepository : EntityRepositoryBase<Role, IdentityDbContext>, IRoleRepository
{
    // Constructor for RoleRepository, taking IdentityDbContext and ICacheBroker as dependencies.
    public RoleRepository(IdentityDbContext dbContext, ICacheBroker cacheBroker)
        : base(dbContext, cacheBroker)
    {
        // Initializes the RoleRepository with required dependencies.
    }

    // Overrides the Get method from the base class to retrieve Role entities.
    public new IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate, bool asNoTracking)
    {
        // Calls the base class's Get method to retrieve Role entities based on provided parameters.
        return base.Get(predicate, asNoTracking);
    }
}
