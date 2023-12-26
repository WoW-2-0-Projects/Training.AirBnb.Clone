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
    /// Gets the folder path
    /// </summary>
    public string FolderPath { get; init; } = default!;
}