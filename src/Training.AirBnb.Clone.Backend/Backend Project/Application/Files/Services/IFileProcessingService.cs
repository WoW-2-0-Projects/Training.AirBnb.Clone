using Backend_Project.Application.Files.Dtos;
using Backend_Project.Application.Listings.Dtos;

namespace Backend_Project.Application.Files.Services;

public interface IFileProcessingService
{
    ValueTask<ImageInfoDto> UploadImageAsync(UploadFileDto file);

    ValueTask<bool> DeleteListingImageAsync(Guid imageId);
}