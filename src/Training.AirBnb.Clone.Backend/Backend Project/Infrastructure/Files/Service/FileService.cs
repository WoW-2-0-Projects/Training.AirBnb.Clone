using Backend_Project.Application.Files.Brokers;
using Backend_Project.Application.Files.Services;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Files.Service;

public class FileService : IFileService
{
    private readonly IFileBroker _fileBroker;
    private readonly IDirectoryBroker _directoryBroker;

    public FileService(IFileBroker fileBroker, IDirectoryBroker directoryBroker)
    {
        _fileBroker = fileBroker;
        _directoryBroker = directoryBroker;
    }

    public bool UploadFile(Stream source, ImageInfo imageInfo)
    {
        _directoryBroker.CreateDirectory(imageInfo.FilePath);

        var fileName = imageInfo.Id.ToString() + imageInfo.Extension;
        var filePath = Path.Combine(imageInfo.FilePath, fileName);

        var fileStream = _fileBroker.CreateFileStream(filePath);

        _fileBroker.StreamTransfer(source, fileStream);

        return true;
    }

    public bool DeleteFile(ImageInfo imageInfo)
    {
        var fileName = string.Concat(imageInfo.Id.ToString(), imageInfo.Extension);
        var filePath = Path.Combine(imageInfo.FilePath, fileName);

        _fileBroker.DeleteFile(filePath);

        return true;
    }
}