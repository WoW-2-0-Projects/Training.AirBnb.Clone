using AirBnB.Application.StorageFiles.Models;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.StorageFiles.Services;

/// <summary>
/// Represents an interface which defines operations related to processing and managing files,
/// specifically designed for image-related tasks within a storage system.
/// </summary>
public interface IFileProcessingService
{
    /// <summary>
    /// Asynchronously uploads file based on the provided UploadFileInfoDto.
    /// </summary>
    /// <param name="uploadFileInfo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<StorageFile> UploadImageAsync(UploadFileInfoDto uploadFileInfo, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes file based on the provided StorageFile.
    /// </summary>
    /// <param name="storageFile"></param>
    /// <returns></returns>
    public bool RemoveImage(StorageFile storageFile);
}