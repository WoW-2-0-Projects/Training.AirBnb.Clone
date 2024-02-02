using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories.Interfaces;

public interface IUserSettingsRepository
{

    /// <summary>
    /// Retrieves a collection of user settings based on the specified predicate. 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns>An IQueryable collection of User objects.</returns>
    IQueryable<UserSettings> Get(Expression<Func<UserSettings, bool>>? predicate, bool asNoTracking = false);

    /// <summary>
    /// Retrieves a user settings by their unique identifier 
    /// </summary>
    /// <param name="userSettingsId"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the User object if found, or null if not found.</returns>
    ValueTask<UserSettings?> GetByIdAsync(Guid userSettingsId, bool asNoTracking = false, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a list of user settings based on a collection of user IDs.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning a list of User objects.</returns>
    ValueTask<IList<UserSettings>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user settings.
    /// </summary>
    /// <param name="userSettings"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the created User object.</returns>
    ValueTask<UserSettings> CreateAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user settings.
    /// </summary>
    /// <param name="userSettings"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the created User object.</returns>
    ValueTask<UserSettings> UpdateAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a user settings.
    /// </summary>
    /// <param name="userSettings"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the deleted User object.</returns>
    ValueTask<UserSettings?> DeleteAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="userSettingsId"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the deleted User object.</returns>
    ValueTask<UserSettings?> DeleteByIdAsync(Guid userSettingsId, bool saveChanges = true, CancellationToken cancellationToken = default);
}