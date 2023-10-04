using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;

namespace Backend_Project.Infrastructure.CompositionServices;

public class ListingCategoryDetailsService : IListingCategoryDetailsService
{
    private readonly IEntityBaseService<ListingCategory> _listingCategoryService;
    private readonly IEntityBaseService<ListingFeature> _listingFeatureService;
    private readonly IEntityBaseService<ListingFeatureOption> _listingFeatureOptionService;
    private readonly IEntityBaseService<ListingCategoryFeatureOption> _listingCategoryFeatureOptionService;
    private readonly IEntityBaseService<Listing> _listingService;

    public ListingCategoryDetailsService(IEntityBaseService<ListingCategory> listingCategoryService, IEntityBaseService<ListingFeature> listingFeatureService, IEntityBaseService<ListingFeatureOption> listingFeatureOptionService, IEntityBaseService<ListingCategoryFeatureOption> listingCategoryFeatureOptionService, IEntityBaseService<Listing> listingService)
    {
        _listingCategoryService = listingCategoryService;
        _listingFeatureService = listingFeatureService;
        _listingFeatureOptionService = listingFeatureOptionService;
        _listingCategoryFeatureOptionService = listingCategoryFeatureOptionService;
        _listingService = listingService;
    }

    public ValueTask<ListingFeature> AddListingFeatureAsync(ListingFeature feature)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ListingCategory> DeleteCategoryAsync(Guid categoryId)
    {
        if (_listingService.Get(listing => listing.CategoryId == categoryId).Any())
            throw new EntityNotDeletableException<ListingCategory>("There are active listings which are in this category.");

        var deletedCategory = await _listingCategoryService.DeleteAsync(categoryId);

        return deletedCategory;
    }

    public ValueTask<ListingFeatureOption> DeleteFeatureOptionAsync(Guid featureOptionId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ListingFeature> UpdateListingFeatureAsync(ListingFeature feature)
    {
        throw new NotImplementedException();
    }
}