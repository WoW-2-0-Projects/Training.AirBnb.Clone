using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Listings.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Enums;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ImageInfoService : IImageInfoService
{
    private readonly IDataContext _appDataContext;
    private readonly ImageSettings _imageSettings;

    public ImageInfoService(IDataContext dataContext, IOptions<ImageSettings> imageSettings) 
        => (_appDataContext, _imageSettings) = (dataContext, imageSettings.Value);

    public async ValueTask<ImageInfo> CreateAsync(ImageInfo listingImage, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!ValidateOnCreate(listingImage))
            throw new EntityValidationException<ImageInfo>("Listing image not valid!");
        
        await _appDataContext.ImageInfos.AddAsync(listingImage, cancellationToken);

        if(saveChanges) await _appDataContext.SaveChangesAsync();

        return listingImage;
    }

    public IQueryable<ImageInfo> Get(Expression<Func<ImageInfo, bool>> predicate)
        => GetUndeletedListingImages()
            .Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<ImageInfo>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new(GetUndeletedListingImages()
                    .Where(feature => ids.Contains(feature.Id))
                    .ToList());

    public ValueTask<ImageInfo> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
         => new(GetUndeletedListingImages()
            .FirstOrDefault(feature => feature.Id == id)
            ?? throw new EntityNotFoundException<ImageInfo>("Listing Image not found!"));

    public async ValueTask<ImageInfo> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundListingImage = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.ImageInfos.RemoveAsync(foundListingImage, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundListingImage;
    }

    public async ValueTask<ImageInfo> DeleteAsync(ImageInfo listingImage, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(listingImage.Id, saveChanges, cancellationToken);

    private bool ValidateOnCreate(ImageInfo image)
    {
        var fileSizeInKB = image.Size / 1024;

        if (fileSizeInKB < _imageSettings.MinImageSizeInKB)
            return false;

        if (string.IsNullOrWhiteSpace(image.FilePath) 
            || string.IsNullOrWhiteSpace(image.Extension))
            return false;

        if (image.Type == ImageType.Listing && image.ListingId == null)
            return false;

        if (image.Type == ImageType.User && image.ListingId != null)
            return false;
           
        return true;
    }

    private IQueryable<ImageInfo> GetUndeletedListingImages()
            => _appDataContext.ImageInfos
                .Where(feature => !feature.IsDeleted).AsQueryable();
}