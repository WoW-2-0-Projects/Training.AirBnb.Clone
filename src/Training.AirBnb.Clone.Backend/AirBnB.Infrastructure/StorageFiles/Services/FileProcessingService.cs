using AirBnB.Application.StorageFiles.Models;
using AirBnB.Application.StorageFiles.Services;
using AirBnB.Domain.Entities;
using FluentValidation;

namespace AirBnB.Infrastructure.StorageFiles.Services;

/// <summary>
/// Implementation of the <see cref="IFileProcessingService"/> interface for processing file-related operations.
/// </summary>
/// <param name="fileService"></param>
/// <param name="uploadFileInfoValidator"></param>
public class FileProcessingService(
    IFileService fileService,
    IValidator<UploadFileInfoDto> uploadFileInfoValidator)
    : IFileProcessingService
{
    public async ValueTask<StorageFile> UploadImageAsync(UploadFileInfoDto uploadFileInfo, CancellationToken cancellationToken = default)
    {
        await uploadFileInfoValidator.ValidateAsync(uploadFileInfo, options => options.ThrowOnFailures(), cancellationToken);

        var storageFile = new StorageFile
        {
            Id = Guid.NewGuid(),
            Type = uploadFileInfo.StorageFileType,
        };

        storageFile.FileName = GetFileName(storageFile.Id, uploadFileInfo.ContentType);
        
        fileService.UploadFile(uploadFileInfo.Source, storageFile);

        return storageFile;
    }

    public bool RemoveImage(StorageFile storageFile) =>
        fileService.DeleteFile(storageFile);
    
    /// <summary>
    /// Generates a unique file name based on the file ID and content type.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    private static string GetFileName(Guid id, string contentType) => $"{id}.{contentType.Split('/')[1]}";
}