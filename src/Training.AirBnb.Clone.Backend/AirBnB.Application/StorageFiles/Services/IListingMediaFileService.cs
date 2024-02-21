using System.Linq.Expressions;
using AirBnB.Application.StorageFiles.Models;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.StorageFiles.Services;

/// <summary>
/// Service interface for listing media files.
/// </summary>
public interface IListingMediaFileService
{
    /// <summary>
    /// /// <summary>
    /// Gets a queryable collection of listing media files based on the specified predicate.
    /// </summary>
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    IQueryable<ListingMediaFile> Get(Expression<Func<ListingMediaFile, bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Gets a queryable collection of listing media files for a specific listing.
    /// </summary>
    /// <param name="listingId">The unique identifier of the listing.</param>
    /// <param name="asNoTracking">Whether to track entities or not.</param>
    /// <param name="isOrdered">Indicates if the media files should be ordered.</param>
    /// <returns>A queryable collection of listing media files.</returns>
    public IQueryable<ListingMediaFile> GetListingMediaFilesByListingId(
        Guid listingId, 
        bool asNoTracking = false,
        bool isOrdered = true);

    /// <summary>
    /// Gets a listing media file by its unique identifier asynchronously.
    /// </summary>
    /// <param name="mediaFileId">The unique identifier of the listing media file.</param>
    /// <param name="asNoTracking">Whether to track entities or not.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation and containing the listing media file.</returns>
    ValueTask<ListingMediaFile?> GetByIdAsync(Guid mediaFileId, bool asNoTracking = false, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Creates a new listing media file asynchronously based on the provided upload file information.
    /// </summary>
    /// <param name="uploadFileInfo">Information about the uploaded file.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the database.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation and containing the created listing media file.</returns>
    ValueTask<ListingMediaFile> CreateAsync(
        UploadFileInfoDto uploadFileInfo, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reorders a collection of listing media files asynchronously.
    /// </summary>
    /// <param name="listingMediaFiles">The collection of listing media files to reorder.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the database.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask ReorderListingMediaFiles(List<ListingMediaFile> listingMediaFiles,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a listing media file by its unique identifier asynchronously.
    /// </summary>
    /// <param name="listingMediaFileId">The unique identifier of the listing media file to delete.</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the database.</param>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation and containing the deleted listing media file.</returns>
    ValueTask<ListingMediaFile?> DeleteByIdAsync(Guid listingMediaFileId, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}