using AirBnB.Application.StorageFiles.Brokers;
using AirBnB.Application.StorageFiles.Services;
using AirBnB.Domain.Entities;
using AirBnB.Infrastructure.StorageFiles.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.StorageFiles.Services;

/// <summary>
/// Implementation of the <see cref="IFileService"/> interface for file-related operations.
/// </summary>
/// <param name="fileBroker"></param>
/// <param name="directoryBroker"></param>
/// <param name="storageFileSettings"></param>
/// <param name="environment"></param>
public class FileService(
    IFileBroker fileBroker, 
    IDirectoryBroker directoryBroker, 
    IOptions<StorageFileSettings> storageFileSettings,
    IWebHostEnvironment environment) 
    : IFileService
{
    private readonly List<StorageFileLocationSettings> _locationSettings = storageFileSettings.Value.LocationSettings.ToList();
    
    public bool UploadFile(Stream source, StorageFile storageFile)
    {
        var filePath = GetFilePath(storageFile, true);

        using var fileStream = fileBroker.CreateFileStream(filePath);
        fileBroker.StreamTransfer(source, fileStream);
        
        return true;
    }

    public bool DeleteFile(StorageFile storageFile)
    {
        var filePath = GetFilePath(storageFile);

        if (!fileBroker.FileExists(filePath)) return false;
        
        fileBroker.DeleteFile(filePath);
        return true;
    }

    /// <summary>
    /// Gets the file path for the specified <see cref="StorageFile"/>
    /// </summary>
    /// <param name="storageFile"></param>
    /// <param name="createDirectory"></param>
    /// <returns></returns>
    private string GetFilePath(StorageFile storageFile, bool createDirectory = false)
    {
        var relativeFileLocation = _locationSettings
            .Single(file => file.StorageFileType == storageFile.Type).FolderPath;

        var absoluteFileLocation = Path.Combine(environment.WebRootPath, relativeFileLocation);

        if (createDirectory) directoryBroker.CreateDirectory(absoluteFileLocation);
        
        return Path.Combine(absoluteFileLocation, storageFile.FileName);
    }
}