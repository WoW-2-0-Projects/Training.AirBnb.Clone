using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingImageService : IListingImageService
{
    private readonly IDataContext _appDatacontext;

    public ListingImageService(IDataContext dataContext) => _appDatacontext = dataContext;

    public async ValueTask<ListingImage> CreateAsync(ListingImage listingImage, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!ValidateOnCreate(listingImage))
            throw new EntityValidationException<ListingImage>("Listing image not valid!");
        
        await _appDatacontext.ListingImages.AddAsync(listingImage, cancellationToken);

        if(saveChanges) await _appDatacontext.SaveChangesAsync();

        return listingImage;
    }

    public IQueryable<ListingImage> Get(Expression<Func<ListingImage, bool>> predicate)
        => GetUndeletedListingImages()
            .Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<ListingImage>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new(GetUndeletedListingImages()
                    .Where(feature => ids.Contains(feature.Id))
                    .ToList());

    public ValueTask<ListingImage> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
         => new(GetUndeletedListingImages()
            .FirstOrDefault(feature => feature.Id == id)
            ?? throw new EntityNotFoundException<ListingImage>("Listing Image not found!"));

    public async ValueTask<ListingImage> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundListingImage = await GetByIdAsync(id, cancellationToken);

        await _appDatacontext.ListingImages.RemoveAsync(foundListingImage, cancellationToken);

        if (saveChanges) await _appDatacontext.ListingFeatures.SaveChangesAsync(cancellationToken);

        return foundListingImage;
    }

    public async ValueTask<ListingImage> DeleteAsync(ListingImage listingImage, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(listingImage.Id, saveChanges, cancellationToken);

    private static bool ValidateOnCreate(ListingImage image)
    {
        var fileSize = image.Size / 1024 / 1024;

        if(fileSize < 3)
            return false;

        if(string.IsNullOrWhiteSpace(image.FilePath) 
            || string.IsNullOrWhiteSpace(image.Extension)) 
            return false; 
           
        return true;
    }

    private IQueryable<ListingImage> GetUndeletedListingImages()
            => _appDatacontext.ListingImages
                .Where(feature => !feature.IsDeleted).AsQueryable();
}