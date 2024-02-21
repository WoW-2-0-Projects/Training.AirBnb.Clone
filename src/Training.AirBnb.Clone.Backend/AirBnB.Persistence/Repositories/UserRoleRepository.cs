using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Provides user role repository functionalities
/// </summary>
public class UserRoleRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<UserRole, AppDbContext>(dbContext, cacheBroker), IUserRoleRepository
{
    public new ValueTask<UserRole> CreateAsync(UserRole userRole, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        base.CreateAsync(userRole, saveChanges, cancellationToken);

    public new ValueTask<UserRole?> DeleteAsync(UserRole userRole, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        base.DeleteAsync(userRole, saveChanges, cancellationToken);
}