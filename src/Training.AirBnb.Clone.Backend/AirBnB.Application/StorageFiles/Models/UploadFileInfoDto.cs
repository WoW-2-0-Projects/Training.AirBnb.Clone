using AirBnB.Domain.Enums;

namespace AirBnB.Application.StorageFiles.Models;

/// <summary>
/// Data transfer object (DTO) for uploading file information.
/// </summary>
public class UploadFileInfoDto
{
    /// <summary>
    /// Gets or sets the content type of the file.
    /// </summary>
    public string ContentType { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the size of the file.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Gets or sets the stream containing the file data.
    /// </summary>
    public Stream Source { get; set; } = default!;

    /// <summary>
    /// Gets or sets the storage file type.
    /// </summary>
    public StorageFileType StorageFileType { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the owner.
    /// </summary>
    public Guid OwnerId { get; set; } 
}