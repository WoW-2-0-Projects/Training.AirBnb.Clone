using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Ratings.Services;

/// <summary>
/// Represents Guest Feedback foundation service
/// </summary>
public interface IGuestFeedbackService
{
    /// <summary>
    ///  Retrieves a collection of GuestFeedback entities based on a specified predicate.
    /// If no predicate is provided, retrieves all GuestFeedback entities.
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
    /// Retrieves a collection of GuestFeedback entities associated with a specific listing asynchronously.
    /// </summary>
    /// <param name="listingId"></param>
    /// <returns></returns>
    public ValueTask<IList<GuestFeedback>> GetByListingIdAsync(Guid listingId);
    
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
    
    /// <summary>
    /// Deletes a GuestFeedback entity asynchronously.
    /// </summary>
    /// <param name="guestFeedback"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<GuestFeedback?> DeleteAsync(
        GuestFeedback guestFeedback, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a GuestFeedback entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<GuestFeedback?> DeleteByIdAsync(
        Guid id, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
}