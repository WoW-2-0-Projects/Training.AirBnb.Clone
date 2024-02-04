using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Defines a repository denationalisation
/// </summary>
public interface IUserRoleRepository
{
    /// <summary>
    /// Creates a new user role
    /// </summary>
    /// <param name="userRole">User role to create</param>
    /// <param name="saveChanges">Determines whether to send changes to the database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created user role</returns>
    ValueTask<UserRole> CreateAsync(UserRole userRole, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user role by its unique identifier asynchronously.
    /// </summary>
    /// <param name="userRole">User role to delete</param>
    /// <param name="saveChanges">Determines whether to send changes to the database</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Deleted user role or null</returns>
    ValueTask<UserRole?> DeleteAsync(UserRole userRole, bool saveChanges = true, CancellationToken cancellationToken = default);
}