using System.Linq.Expressions;

namespace AirBnB.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for managing EmailHistory
/// </summary>
public interface IEmailHistoryService
{
    /// <summary>
    ///  Retrieves a queryable collection of EmailHistory objects based on the provided predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    IQueryable<EmailHistory> Get(Expression<Func<EmailHistory, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Creates a new EmailHistory entity 
    /// </summary>
    /// <param name="emailHistory"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true, CancellationToken cancellationToken = default);
}
