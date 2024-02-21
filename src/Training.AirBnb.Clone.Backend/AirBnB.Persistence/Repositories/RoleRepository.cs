using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;
/// <summary>
/// This class represents a repository for handling Role entities, implementing IRoleRepository.
/// </summary>
public class RoleRepository : EntityRepositoryBase<Role, AppDbContext>, IRoleRepository
{
    /// <summary>
    /// Constructor for RoleRepository, taking IdentityDbContext and ICacheBroker as dependencies.
    /// </summary>
    public RoleRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
        : base(dbContext, cacheBroker)
    {

    }

    /// <summary>
    /// Overrides the Get method from the base class to retrieve Role entities.
    /// </summary>
    public new IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate = default, bool asNoTracking = false)
    {
        // Calls the base class's Get method to retrieve Role entities based on provided parameters.
        return base.Get(predicate, asNoTracking);
    }
}
