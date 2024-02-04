using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Services;

/// <summary>
/// Service interface for managing user-related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves a collection of users based on the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns>Returning the User object</returns>
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the User object.</returns>
    ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a collection of users based on the specified predicate.
    /// </summary>
    /// <param name="querySpecification"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IList<User>> GetAsync(QuerySpecification<User> querySpecification,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a user entity with the specified email address from the data source.
    /// </summary>
    /// <param name="emailAddress">The email address of the user to retrieve.</param>
    /// <param name="asNoTracking">Optional flag indicating whether to retrieve the user without tracking changes. Defaults to false.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A ValueTask containing:
    /// - The retrieved user entity if found.
    /// - Null if no user with the specified email address exists.
    /// </returns>
    ValueTask<User> GetByEmailAddressAsync(string emailAddress, bool asNoTracking = false, CancellationToken cancellationToken = default);
    
     /// <summary>
    /// Retrieves a collection of users based on the specified predicate.
    /// </summary>
    /// <param name="querySpecification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IList<User>> GetAsync(QuerySpecification querySpecification,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves a list of users based on a collection of user IDs.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning a list of User objects.</returns>
    ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the created User object.</returns>
    ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the updated User object.</returns>
    ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the deleted User object.</returns>
    ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returning the deleted User object.</returns>
    ValueTask<User?> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);
}