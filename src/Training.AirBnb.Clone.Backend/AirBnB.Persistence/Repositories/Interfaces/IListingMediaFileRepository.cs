using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Repository interface for accessing and managing listing media files.
/// </summary>
public interface IListingMediaFileRepository
{
    /// <summary>
    /// Gets a queryable collection of listing media files based on the provided predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns>A queryable collection of listing media files.</returns>
    IQueryable<ListingMediaFile> Get(Expression<Func<ListingMediaFile, bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Gets a listing media file by its unique identifier asynchronously.
    /// </summary>
    /// <param name="mediaFileId">The unique identifier of the listing media file.</param>
    /// <param name="asNoTracking">Indicates whether to use tracking or not (default is false).</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the retrieved listing media file.</returns>
    ValueTask<ListingMediaFile?> GetByIdAsync(Guid mediaFileId, bool asNoTracking = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new listing media file asynchronously.
    /// </summary>
    /// <param name="listingMediaFile">The listing media file to create.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the database (default is true).</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the created listing media file.</returns>
    ValueTask<ListingMediaFile> CreateAsync(
        ListingMediaFile listingMediaFile, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a range of listing media files asynchronously.
    /// </summary>
    /// <param name="listingMediaFiles">The list of listing media files to update.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the database (default is true).</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations (optional).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask UpdateRangeAsync(List<ListingMediaFile> listingMediaFiles,
        bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a listing media file asynchronously.
    /// </summary>
    /// <param name="listingMediaFile">The listing media file to delete.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the database (default is true).</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted listing media file (if applicable).</returns>
    ValueTask<ListingMediaFile?> DeleteAsync(
        ListingMediaFile listingMediaFile, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a listing media file by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the listing media file to delete.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the database (default is true).</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations (optional).</param>
    /// <returns>A task representing the asynchronous operation, containing the deleted listing media file (if applicable).</returns>
    ValueTask<ListingMediaFile?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}