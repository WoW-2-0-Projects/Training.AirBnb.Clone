using AutoMapper;
using Backend_Project.Application.Files.Constants;
using Backend_Project.Application.Files.Dtos;
using Backend_Project.Application.Files.Services;
using Backend_Project.Application.Files.Settings;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Listings.Dtos;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Enums;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Backend_Project.Infrastructure.Files.Service;

public class FileProcessingService : IFileProcessingService
{
    private readonly IImageInfoService _imageService;
    private readonly IFileService _fileService;
    private readonly FileSettings _fileSettings;
    private readonly IMapper _mapper;

    public FileProcessingService(IImageInfoService listingImageService, IFileService fileService, IOptions<FileSettings> fileSettings, 
        IMapper mapper)
    {
        _imageService = listingImageService;
        _fileService = fileService;
        _fileSettings = fileSettings.Value;
        _mapper = mapper;
    }

    public async ValueTask<ImageInfoDto> UploadImageAsync(UploadFileDto file)
    {
        var (extension, isValid) = ValidateImageType(file.ContentType);

        if (!isValid)
            throw new ValidationException("Invalid image extension!");

        var image = _mapper.Map<ImageInfo>(file);
        image.Extension = extension;
        image.FilePath = GenerateFilePath(file);
           
        var imageInfo = await _imageService.CreateAsync(image);

        _fileService.UploadFile(file.Source, imageInfo);

        if (file.Type == ImageType.User)
            await RemoveOldProfilePictureIfExists(imageInfo.UserId);

        return _mapper.Map<ImageInfoDto>(imageInfo);
    }

    public async ValueTask<bool> DeleteListingImageAsync(Guid imageId)
    {
        var imageInfo = await _imageService.GetByIdAsync(imageId);

        if (imageInfo.Type != ImageType.Listing)
            throw new ArgumentException("Invalid Image Id");

        _fileService.DeleteFile(imageInfo);

        await _imageService.DeleteAsync(imageInfo);

        return true;
    }

    private static (string Extension, bool IsValid) ValidateImageType(string contentType)
    {
        var type = contentType.Split('/');

        if (type.Length < 2)
            return (string.Empty, false);

        if (type[0] != "image")
            return (string.Empty, false);

        return (string.Concat('.', type[1]), true);
    }

    private async Task RemoveOldProfilePictureIfExists(Guid userId)
    {
        var images = _imageService.Get(image => image.UserId == userId && image.Type == ImageType.User);

        var userImage = images.MinBy(file => file.CreatedDate);

        if (userImage == null)
            return;

        _fileService.DeleteFile(userImage);

        await _imageService.DeleteAsync(userImage.Id);
    }

    private string GenerateFilePath(UploadFileDto file)
    {
        if (file.Type == ImageType.User)
            return _fileSettings.ProfilePicturePath
              .Replace(FileConstants.UserIdToken, file.UserId.ToString());
        
        return _fileSettings.ListingImagePath
            .Replace(FileConstants.UserIdToken, file.UserId.ToString())
            .Replace(FileConstants.ListingIdToken, file.ListingId.ToString());
    }
}