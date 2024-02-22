using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for the repository handling operations related to user profile media files.
/// </summary>
public interface IUserProfileMediaFileRepository
{
    /// <summary>
    /// Retrieves a queryable collection of user profile media files based on the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the results.</param>
    /// <param name="asNoTracking">Indicates whether to use no-tracking queries. Default is false.</param>
    /// <returns>An IQueryable of UserProfileMediaFile based on the provided predicate.</returns>
    IQueryable<UserProfileMediaFile> Get(Expression<Func<UserProfileMediaFile, bool>>? predicate = default,
        bool asNoTracking = false);
    
    /// <summary>
    /// Asynchronously retrieves a user profile media file by its unique identifier.
    /// </summary>
    /// <param name="userProfileMediaId">The unique identifier of the user profile media file.</param>
    /// <param name="asNoTracking">Indicates whether to use no-tracking queries. Default is false.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>A task representing the asynchronous operation, returning the UserProfileMediaFile or null if not found.</returns>
    ValueTask<UserProfileMediaFile?> GetByIdAsync(Guid userProfileMediaId, bool asNoTracking = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously creates a new user profile media file.
    /// </summary>
    /// <param name="userProfileMedia">The UserProfileMediaFile object to create.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the database. Default is true.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>A task representing the asynchronous operation, returning the created UserProfileMediaFile.</returns>
    ValueTask<UserProfileMediaFile> CreateAsync(UserProfileMediaFile userProfileMedia, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes a user profile media file.
    /// </summary>
    /// <param name="userProfileMedia">The UserProfileMediaFile object to delete.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the database. Default is true.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>A task representing the asynchronous operation, returning the deleted UserProfileMediaFile or null if not found.</returns>
    ValueTask<UserProfileMediaFile?> DeleteAsync(UserProfileMediaFile userProfileMedia, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}