using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities.StorageFiles;

/// <summary>
/// Represents a storage file.
/// </summary>
public class StorageFile : Entity
{
    /// <summary>
    /// Gets or sets file name
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of storage file.
    /// </summary>
    public StorageFileType Type { get; set; }
}