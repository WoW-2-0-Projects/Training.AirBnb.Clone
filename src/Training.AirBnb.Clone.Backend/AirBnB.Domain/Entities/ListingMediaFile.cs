using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

public class ListingMediaFile : SoftDeletedEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier of the associated listing.
    /// </summary>
    public Guid ListingId { get; set; }
    
    /// <summary>
    ///  Gets or sets the unique identifier of the associated storage file.
    /// </summary>
    public Guid StorageFileId { get; set; }

    /// <summary>
    /// Gets or sets the order number of an image
    /// </summary>
    public byte OrderNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the reference to the associated listing.
    /// </summary>
    public virtual Listing Listing { get; set; }
    
    /// <summary>
    /// Gets or sets the reference to the associated storage file.
    /// </summary>
    public StorageFile StorageFile { get; set; }
}