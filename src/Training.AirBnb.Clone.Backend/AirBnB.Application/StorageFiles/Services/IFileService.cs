using AirBnB.Domain.Entities;

namespace AirBnB.Application.StorageFiles.Services;

/// <summary>
/// Represents an interface that defines operations related to file management within a storage system.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Uploads a file from the provided source stream to the specified StorageFile.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="storageFile"></param>
    /// <returns></returns>
    bool UploadFile(Stream source, StorageFile storageFile);

    /// <summary>
    /// Deletes the file specified in the given StorageFile.
    /// </summary>
    /// <param name="storageFile"></param>
    /// <returns></returns>
    bool DeleteFile(StorageFile storageFile);
}