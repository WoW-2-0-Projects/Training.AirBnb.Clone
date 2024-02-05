using System.Linq.Expressions;

namespace AirBnB.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for managing sms history
/// </summary>
public interface ISmsHistoryService
{
    /// <summary>
    /// Retrieves a queryable collection of sms history objects based on the provided predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    IQueryable<SmsHistory> Get(Expression<Func<SmsHistory, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Creates a new sms history entity
    /// </summary>
    /// <param name="smsHistory"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<SmsHistory> CreateAsync(SmsHistory smsHistory, bool saveChanges = true, CancellationToken cancellationToken = default);
}
