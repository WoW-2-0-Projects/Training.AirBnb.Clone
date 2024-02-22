using AirBnB.Domain.Enums;

namespace AirBnB.Infrastructure.StorageFiles.Settings;

/// <summary>
/// Represents the settings for storage files.
/// </summary>
public class StorageFileLocationSettings
{
    /// <summary>
    /// Gets the storage file location
    /// </summary>
    public StorageFileType StorageFileType { get; init; }
    
    /// <summary>
    ///  Gets the allowed image extensions for the storage file type
    /// </summary>
    public List<string> AllowedImageExtensions { get; init; } = default!;

    /// <summary>
    ///  Gets the allowed minimum image size in bytes for the storage file type
    /// </summary>
    public long MinimumImageSizeInBytes { get; init; }

    /// <summary>
    /// Gets the allowed maximum image size in bytes for the storage file type
    /// </summary>
    public long MaximumImageSizeInBytes { get; init; }

    /// <summary>
    /// Gets the folder path
    /// </summary>
    public string FolderPath { get; init; } = default!;
}