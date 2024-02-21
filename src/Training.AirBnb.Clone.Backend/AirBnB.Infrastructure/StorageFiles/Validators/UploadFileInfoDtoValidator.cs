using AirBnB.Application.StorageFiles.Models;
using AirBnB.Domain.Enums;
using AirBnB.Infrastructure.StorageFiles.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.StorageFiles.Validators;

public class UploadFileInfoDtoValidator : AbstractValidator<UploadFileInfoDto>
{
    public UploadFileInfoDtoValidator(IOptions<StorageFileSettings> storageFileSettings)
    {
        RuleFor(media => media.StorageFileType).IsInEnum();
        RuleFor(media => media.OwnerId).NotEmpty().NotEqual(Guid.Empty);

        RuleFor(media => media.Size)
            .Must((media, size) => ValidateImageSize(media.StorageFileType, size, storageFileSettings.Value))
            .WithMessage("Invalid image size.");

        RuleFor(media => media.ContentType)
            .Must((media, contentType) => ValidateImageContentType(media.StorageFileType, contentType, storageFileSettings.Value))
            .WithMessage("Unsupported file type.");
    }

    private static StorageFileLocationSettings GetStorageFileSettingsByFileType(StorageFileType type, StorageFileSettings storageFileSettings)
    {
        return storageFileSettings.LocationSettings.Single(image =>
            image.StorageFileType == type);
    }

    private static bool ValidateImageSize(StorageFileType type, long size, StorageFileSettings settings)
    {
        var imageSettings = GetStorageFileSettingsByFileType(type, settings);

        return size >= imageSettings.MinimumImageSizeInBytes && size <= imageSettings.MaximumImageSizeInBytes;
    }
    
    private static bool ValidateImageContentType(StorageFileType fileType, string contentType, StorageFileSettings settings)
    {
        var imageSettings = GetStorageFileSettingsByFileType(fileType, settings);
        
        var type = contentType.Split('/');

        return type is ["image", _, ..] &&
               imageSettings.AllowedImageExtensions.Contains(type[1]);
    }
}