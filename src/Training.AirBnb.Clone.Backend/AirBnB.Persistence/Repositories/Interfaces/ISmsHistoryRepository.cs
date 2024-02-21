using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents an interface for managin sms history data
/// </summary>
public interface ISmsHistoryRepository
{
    /// <summary>
    ///  Retrieves a queryable collection of SmsHistory objects based on the provided predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    IQueryable<SmsHistory> Get(
       Expression<Func<SmsHistory, bool>>? predicate = default,
       bool asNoTracking = false);

    /// <summary>
    /// Creates a new SmsHistory entity and adds it to the repository
    /// </summary>
    /// <param name="smsHistory"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<SmsHistory> CreateAsync(
      SmsHistory smsHistory,
      bool saveChanges = true,
      CancellationToken cancellationToken = default);
}
