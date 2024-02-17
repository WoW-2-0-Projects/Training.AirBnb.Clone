using System.Linq.Expressions;
using System.Threading.Tasks;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents an interface for managin email history data in repository
/// </summary>
public interface IEmailHistoryRepository
{
    /// <summary>
    /// Retrieves a queryable collection of EmailHistory objects based on the provided predicate.
    /// </summary>
    /// <param name="predicate">An optional expression to filter the EmailHistory entities</param>
    /// <param name="asNoTracking">Specifies whether to track entities for change detection</param>
    /// <returns>An IQueryable collection of EmailHistory objects</returns>
    IQueryable<EmailHistory> Get(
        Expression<Func<EmailHistory, bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Creates a new EmailHistory entity and adds it to the repository.
    /// </summary>
    /// <param name="emailHistory">The EmailHistory object to be created and added.</param>
    /// <param name="saveChanges">Specifies whether to save changes immediately after creating the entity.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation with the created EmailHistory object.</returns>
    ValueTask<EmailHistory> CreateAsync(
        EmailHistory emailHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
}
