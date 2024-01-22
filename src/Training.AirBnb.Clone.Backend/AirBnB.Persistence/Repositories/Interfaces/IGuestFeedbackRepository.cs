using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents Guest Feedback repository
/// </summary>
public interface IGuestFeedbackRepository
{
    /// <summary>
    /// Retrieves a collection of GuestFeedback entities based on a specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    IQueryable<GuestFeedback> Get(
        Expression<Func<GuestFeedback, bool>>? predicate = default, 
        bool asNoTracking = false);

    /// <summary>
    /// Retrieves a single GuestFeedback entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<GuestFeedback?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new GuestFeedback entity asynchronously.
    /// </summary>
    /// <param name="guestFeedback"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<GuestFeedback> CreateAsync(
        GuestFeedback guestFeedback, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default);
}