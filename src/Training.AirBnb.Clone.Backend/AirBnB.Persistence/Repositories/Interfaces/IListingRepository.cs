using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing listings.
/// </summary>
public interface IListingRepository
{
    /// <summary>
    /// Gets a queryable collection of listings based on the specified predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    IQueryable<Listing> Get(Expression<Func<Listing, bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Asynchronously retrieves a list of listings based on the specified typed query specification.
    /// </summary>
    /// <param name="querySpecification">The typed query specification used to filter and retrieve listings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.
    /// The result contains a list of <see cref="Listing"/> objects that match the specified typed query.
    /// </returns>
    ValueTask<IList<Listing>> GetAsync(QuerySpecification<Listing> querySpecification,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a list of listings based on the specified query specification.
    /// </summary>
    /// <param name="querySpecification">The query specification used to filter and retrieve listings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.
    /// The result contains a list of <see cref="Listing"/> objects that match the specified query.
    /// </returns>
    ValueTask<IList<Listing>> GetAsync(QuerySpecification querySpecification,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets a listing by its unique identifier asynchronously.
    /// </summary>
    /// <param name="listingId"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Listing?> GetByIdAsync(Guid listingId, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets listings by a collection of unique identifiers asynchronously.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IList<Listing>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new listing asynchronously.
    /// </summary>
    /// <param name="listing"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Listing> CreateAsync(Listing listing, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing listing asynchronously.
    /// </summary>
    /// <param name="listing"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Listing> UpdateAsync(Listing listing, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an existing listing asynchronously.
    /// </summary>
    /// <param name="listing"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Listing?> DeleteAsync(Listing listing, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a listing by its unique identifier asynchronously.
    /// </summary>
    /// <param name="listingId"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Listing?> DeleteByIdAsync(Guid listingId, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}