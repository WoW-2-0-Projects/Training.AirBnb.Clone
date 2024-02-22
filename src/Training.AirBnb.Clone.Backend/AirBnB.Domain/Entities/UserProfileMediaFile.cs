using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents users Profile Media File
/// </summary>
public class UserProfileMediaFile : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier of the user associated with this media file.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the storage file related to this media file.
    /// </summary>
    public Guid StorageFileId { get; set; }

    /// <summary>
    /// Navigation property representing the user associated with this media file.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Navigation property representing the storage file related to this media file.
    /// </summary>
    public virtual StorageFile StorageFile { get; set; }
}