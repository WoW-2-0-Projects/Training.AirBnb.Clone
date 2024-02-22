using System.Linq.Expressions;
using AirBnB.Application.StorageFiles.Models;
using AirBnB.Application.StorageFiles.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.StorageFiles.Services;

/// <summary>
/// Service class for handling operations related to user profile media files.
/// </summary>
/// <param name="fileProcessingService"></param>
/// <param name="userProfileMediaFileRepository"></param>
/// <param name="userProfileMediaFileValidator"></param>
/// <param name="mapper"></param>
public class UserProfileMediaFileService(
    IFileProcessingService fileProcessingService, 
    IUserProfileMediaFileRepository userProfileMediaFileRepository,
    IValidator<UserProfileMediaFile> userProfileMediaFileValidator,
    IMapper mapper) 
    : IUserProfileMediaFileService
{
    public IQueryable<UserProfileMediaFile> Get(Expression<Func<UserProfileMediaFile, bool>>? predicate = default, bool asNoTracking = false)
    {
        return userProfileMediaFileRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<UserProfileMediaFile?> GetByIdAsync(Guid userProfileMediaId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return userProfileMediaFileRepository.GetByIdAsync(userProfileMediaId, asNoTracking, cancellationToken);
    }

    public async ValueTask<UserProfileMediaFile> CreateAsync(UploadFileInfoDto uploadFileInfo, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var userProfileMediaFile = mapper.Map<UserProfileMediaFile>(uploadFileInfo);

        await userProfileMediaFileValidator.ValidateAsync(userProfileMediaFile, options =>
        {
            options.IncludeRuleSets(EntityEvent.OnCreate.ToString());
            options.ThrowOnFailures();
        }, cancellationToken);

        userProfileMediaFile.StorageFile = await fileProcessingService
            .UploadImageAsync(uploadFileInfo, cancellationToken);

        await RemoveUserProfilePictureIfExists(userProfileMediaFile.UserId, cancellationToken);
        
        return await userProfileMediaFileRepository
            .CreateAsync(userProfileMediaFile, cancellationToken: cancellationToken);
    }

    public async ValueTask<UserProfileMediaFile?> DeleteAsync(UserProfileMediaFile userProfileMedia, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return await userProfileMediaFileRepository.DeleteAsync(userProfileMedia, saveChanges, cancellationToken);
    }

    /// <summary>
    /// Removes the user profile picture if it exists for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    private async ValueTask RemoveUserProfilePictureIfExists(Guid userId, 
        CancellationToken cancellationToken = default)
    {
        var foundUserProfilePicture = Get(media => media.UserId == userId)
            .Include(media => media.StorageFile)
            .FirstOrDefault();

        if (foundUserProfilePicture is null)
            return;

        var deletedProfilePicture = await DeleteAsync(foundUserProfilePicture, cancellationToken: cancellationToken) 
                                    ?? throw new InvalidOperationException("User Profile Media File can't be deleted");

        fileProcessingService.RemoveImage(deletedProfilePicture.StorageFile);
    }
}