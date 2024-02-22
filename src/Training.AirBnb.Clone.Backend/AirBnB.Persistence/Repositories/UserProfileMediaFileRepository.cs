using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Repository class for handling operations related to user profile media files.
/// </summary>
/// <param name="dbContext"></param>
/// <param name="cacheBroker"></param>
public class UserProfileMediaFileRepository(AppDbContext dbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<UserProfileMediaFile, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()),
        IUserProfileMediaFileRepository
{
    public new IQueryable<UserProfileMediaFile> Get(Expression<Func<UserProfileMediaFile, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new ValueTask<UserProfileMediaFile?> GetByIdAsync(Guid userProfileMediaId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userProfileMediaId, asNoTracking, cancellationToken);
    }

    public new ValueTask<UserProfileMediaFile> CreateAsync(UserProfileMediaFile userProfileMedia, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(userProfileMedia, saveChanges, cancellationToken);
    }

    public new ValueTask<UserProfileMediaFile?> DeleteAsync(UserProfileMediaFile userProfileMedia, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(userProfileMedia, saveChanges, cancellationToken);
    }
}