using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Files.Services;

public interface IFileService
{
    bool UploadFile(Stream source, ImageInfo imageInfo);

    bool DeleteFile(ImageInfo imageInfo);
}