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
/// Service for managing listing media files.
/// </summary>
/// <param name="listingMediaFileRepository"></param>
/// <param name="fileProcessingService"></param>
/// <param name="listingMediaFileValidator"></param>
/// <param name="mapper"></param>
public class ListingMediaFileService(
    IListingMediaFileRepository listingMediaFileRepository, 
    IFileProcessingService fileProcessingService,
    IValidator<ListingMediaFile> listingMediaFileValidator,
    IMapper mapper) 
    : IListingMediaFileService
{
    public IQueryable<ListingMediaFile> Get(Expression<Func<ListingMediaFile, bool>>? predicate = default, bool asNoTracking = false)
    {
        return listingMediaFileRepository.Get(predicate, asNoTracking);
    }

    public IQueryable<ListingMediaFile> GetListingMediaFilesByListingId(Guid listingId, bool asNoTracking = false, bool isOrdered = true)
    {
        var initialQuery = Get(media => media.ListingId == listingId, asNoTracking: asNoTracking).Include(media => media.StorageFile);

        return isOrdered ? initialQuery.OrderBy(media => media.OrderNumber) : initialQuery;
    }


    public ValueTask<ListingMediaFile?> GetByIdAsync(Guid mediaFileId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return listingMediaFileRepository.GetByIdAsync(mediaFileId, asNoTracking, cancellationToken);
    }

    public async ValueTask<ListingMediaFile> CreateAsync(UploadFileInfoDto uploadFileInfo, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var listingMediaFile = mapper.Map<ListingMediaFile>(uploadFileInfo);

        await SetListingMediaFileOrderNumber(listingMediaFile);
        
        await listingMediaFileValidator.ValidateAsync(listingMediaFile, options =>
        {
            options.IncludeRuleSets(EntityEvent.OnCreate.ToString());
            options.ThrowOnFailures();
        }, cancellationToken);

        // Upload listing image
        listingMediaFile.StorageFile = await fileProcessingService.UploadImageAsync(uploadFileInfo, cancellationToken);
        
        return await listingMediaFileRepository.CreateAsync(listingMediaFile, saveChanges, cancellationToken);
    }

    public async ValueTask ReorderListingMediaFiles(List<ListingMediaFile> listingMediaFiles, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var mediaFilesDictionary = listingMediaFiles.ToDictionary(media => media.Id);
        var mediaFilesIds = listingMediaFiles.Select(media => media.Id);
        
        var originalMediaFiles = await Get()
            .Where(media => mediaFilesIds.Contains(media.Id))
            .ToListAsync(cancellationToken);

        if (listingMediaFiles.Count < 2 || !IsValidReordering(listingMediaFiles, originalMediaFiles))
            throw new ArgumentException("Sequence contains invalid order numbers");

        foreach (var originalMedia in originalMediaFiles)
            originalMedia.OrderNumber = mediaFilesDictionary[originalMedia.Id].OrderNumber;
        
        await listingMediaFileRepository.UpdateRangeAsync(originalMediaFiles, cancellationToken: cancellationToken);
    }

    public async ValueTask<ListingMediaFile?> DeleteByIdAsync(Guid listingMediaFileId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundListingMediaFile = await Get(media => media.Id == listingMediaFileId)
            .Include(media => media.StorageFile)
            .FirstOrDefaultAsync(cancellationToken) 
            ?? throw new ArgumentException("ListingMediaFile not found.");

        if (Get(media => media.ListingId == foundListingMediaFile.ListingId).Count() < 5)
            throw new InvalidOperationException("Listing must have at least 5 images.");

        var deletedMediaFile = await listingMediaFileRepository
            .DeleteAsync(foundListingMediaFile, saveChanges, cancellationToken)
            ?? throw new InvalidOperationException("ListingMediaFile can't be deleted.");

        await AdjustImageOrderNumbers(deletedMediaFile, cancellationToken);

        // TODO: create an event for file removal and publish it here
        fileProcessingService.RemoveImage(deletedMediaFile.StorageFile);

        return deletedMediaFile;
    }

    /// <summary>
    /// Sets the order number for the given listing media file based on the current count.
    /// </summary>
    /// <param name="listingMediaFile"></param>
    /// <returns></returns>
    private ValueTask SetListingMediaFileOrderNumber(ListingMediaFile listingMediaFile)
    {
        var lastOrderNumber = GetListingMediaFilesByListingId(listingMediaFile.ListingId, isOrdered: false)
            .Count();
        
        listingMediaFile.OrderNumber = (byte)lastOrderNumber;

        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Adjusts the order numbers of listing media files after deleting a media file.
    /// </summary>
    /// <param name="deletedMediaFile"></param>
    /// <param name="cancellationToken"></param>
    private async ValueTask AdjustImageOrderNumbers(ListingMediaFile deletedMediaFile, CancellationToken cancellationToken)
    {
        var imagesToReorder = await Get(media =>
            media.ListingId == deletedMediaFile.ListingId && media.OrderNumber > deletedMediaFile.OrderNumber)
            .ToListAsync(cancellationToken);

        if (imagesToReorder.Count == 0)
            return;

        foreach (var media in imagesToReorder)
            media.OrderNumber--;
        
        await listingMediaFileRepository.UpdateRangeAsync(imagesToReorder, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Validates whether the reordering of media files is valid by comparing order numbers.
    /// </summary>
    /// <param name="updatedMediaFiles"></param>
    /// <param name="originalMediaFiles"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static bool IsValidReordering(
        IReadOnlyCollection<ListingMediaFile> updatedMediaFiles, 
        IReadOnlyCollection<ListingMediaFile> originalMediaFiles)
    {
        if (updatedMediaFiles.Count != originalMediaFiles.Count)
            throw new ArgumentException("One or more of Listing Images were not found.");

        var updatedMediaFilesOrderNums = updatedMediaFiles.Select(media => media.OrderNumber);

        return originalMediaFiles.All(media => updatedMediaFilesOrderNums.Contains(media.OrderNumber));
    }
}