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
    private readonly IEntityBaseService<ListingProperty> _listingPropertyService;

    public ListingCategoryDetailsService(IEntityBaseService<ListingCategory> listingCategoryService, IEntityBaseService<ListingFeature> listingFeatureService, IEntityBaseService<ListingFeatureOption> listingFeatureOptionService, IEntityBaseService<ListingCategoryFeatureOption> listingCategoryFeatureOptionService, IEntityBaseService<Listing> listingService, IEntityBaseService<ListingProperty> listingPropertyService)
    {
        _listingCategoryService = listingCategoryService;
        _listingFeatureService = listingFeatureService;
        _listingFeatureOptionService = listingFeatureOptionService;
        _listingCategoryFeatureOptionService = listingCategoryFeatureOptionService;
        _listingService = listingService;
        _listingPropertyService = listingPropertyService;
    }

    public async ValueTask<ListingFeature> AddListingFeatureAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _listingFeatureOptionService.GetByIdAsync(feature.FeatureOptionsId, cancellationToken);
        return await _listingFeatureService.CreateAsync(feature, saveChanges, cancellationToken);
    }

    public async ValueTask<ListingFeature> UpdateListingFeatureAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var listingProperties = await GetListingPropertiesByTypeId(feature.FeatureOptionsId);

        var min = listingProperties.MinBy(self => self.PropertyCount)?.PropertyCount;
        var max = listingProperties.MaxBy(self => self.PropertyCount)?.PropertyCount;

        if (min is not null && min < feature.MinValue)
            throw new EntityNotUpdatableException<ListingFeature>($"Not possible to update {nameof(feature.MinValue)} of the listing feature. It is exceeding the limits of the current listing properties in use.");

        if (max is not null && max < feature.MaxValue)
            throw new EntityNotUpdatableException<ListingFeature>($"Not possible to update {nameof(feature.MaxValue)} of the listing feature. It is exceeding the limits of the current listing properties in use.");

        return await _listingFeatureService.UpdateAsync(feature, saveChanges, cancellationToken);
    }

    public ICollection<ListingFeature> GetListingFeaturesByOptionId(Guid listingFeatureOptionId)
       => _listingFeatureService.Get(feature => feature.FeatureOptionsId == listingFeatureOptionId).ToList();

    public async ValueTask<ListingFeature> DeleteListingFeatureAsync(Guid featureId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var feature = await _listingFeatureService.GetByIdAsync(featureId, cancellationToken);

        var listingProperties = await GetListingPropertiesByTypeId(feature.FeatureOptionsId);

        if (listingProperties.Any(self => self.PropertyName == feature.Name))
            throw new EntityNotDeletableException<ListingFeature>("You can't delete this listing feature. There are active listings which have this listing feature.");

        return await _listingFeatureService.DeleteAsync(featureId, saveChanges, cancellationToken);
    }

    public async ValueTask<ListingCategory> DeleteCategoryAsync(Guid categoryId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (_listingService.Get(listing => listing.CategoryId == categoryId).Any())
            throw new EntityNotDeletableException<ListingCategory>("There are active listings which are in this category.");

        var deletedCategory = await _listingCategoryService.DeleteAsync(categoryId);

        await DeleteCategoryRelations(categoryId, saveChanges, cancellationToken);

        return deletedCategory;
    }

    public async ValueTask<ICollection<ListingFeatureOption>> GetFeatureOptionsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var relations = _listingCategoryFeatureOptionService
            .Get(relation => relation.ListingCategoryId == categoryId);

        var featureOptions = new List<ListingFeatureOption>();

        foreach (var relation in relations)
            featureOptions.Add(await _listingFeatureOptionService.GetByIdAsync(relation.ListingFeatureOptionId));

        return featureOptions;
    }

    public async ValueTask<ListingFeatureOption> DeleteFeatureOptionAsync(Guid featureOptionId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (_listingService.Get(listing => listing.TypeId == featureOptionId).Any())
            throw new EntityNotDeletableException<ListingFeatureOption>("There are active listings of this type.");

        await DeleteFeatureOptionsRelations(featureOptionId, saveChanges, cancellationToken);

        var deletedFeatureOption = await _listingFeatureOptionService.DeleteAsync(featureOptionId, saveChanges, cancellationToken);

        return deletedFeatureOption;
    }

    public async ValueTask<ListingCategoryFeatureOption> AddListingCategoryFeatureOptionAsync(ListingCategoryFeatureOption relation, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _listingCategoryService.GetByIdAsync(relation.ListingCategoryId, cancellationToken);
        await _listingFeatureOptionService.GetByIdAsync(relation.ListingFeatureOptionId, cancellationToken);

        return await _listingCategoryFeatureOptionService.CreateAsync(relation, saveChanges, cancellationToken);
    }

    public async ValueTask<bool> AddListingCategoryFeatureOptionsAsync(Guid categoryId, List<Guid> featureOptions, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _listingCategoryService.GetByIdAsync(categoryId, cancellationToken);

        foreach (var featureOption in featureOptions)
        {
            await _listingFeatureOptionService.GetByIdAsync(featureOption, cancellationToken);
            var newRelation = new ListingCategoryFeatureOption()
            {
                ListingCategoryId = categoryId,
                ListingFeatureOptionId = featureOption
            };
            await _listingCategoryFeatureOptionService.CreateAsync(newRelation, saveChanges, cancellationToken);
        }

        return true;
    }

    public async ValueTask<bool> UpdateListingCategoryFeatureOptionsAsync(Guid categoryId, List<Guid> updatedFeatureOptions, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundFeatureOptions = await GetFeatureOptionsByCategoryIdAsync(categoryId, cancellationToken);

        var deletedFeatureOptions = foundFeatureOptions.
            Select(feature => feature.Id)
            .Except(updatedFeatureOptions)
            .ToList();

        var newFeatureOptions = updatedFeatureOptions
            .Except(foundFeatureOptions
                .Select(feature => feature.Id))
            .ToList();

        var listingCategoryFeatureOptions = _listingCategoryFeatureOptionService
            .Get(relation => relation.ListingCategoryId == categoryId);

        foreach (var newOption in newFeatureOptions)
        {
            await _listingFeatureOptionService.GetByIdAsync(newOption, cancellationToken);

            var newRelation = new ListingCategoryFeatureOption
            {
                ListingCategoryId = categoryId,
                ListingFeatureOptionId = newOption
            };

            await _listingCategoryFeatureOptionService.CreateAsync(newRelation);
        }

        foreach (var deletedOption in deletedFeatureOptions)
        {
            var deletedRelationId = listingCategoryFeatureOptions
                .Single(relation => relation.ListingFeatureOptionId == deletedOption).Id;
            await _listingCategoryFeatureOptionService.DeleteAsync(deletedRelationId);
        }

        return true;
    }

    private async ValueTask DeleteCategoryRelations(Guid categoryId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var connections = _listingCategoryFeatureOptionService
            .Get(self => self.ListingCategoryId == categoryId);

        foreach (var connection in connections)
          await _listingCategoryFeatureOptionService.DeleteAsync(connection, saveChanges, cancellationToken);
    }

    private async ValueTask DeleteFeatureOptionsRelations(Guid featurOptionId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var connections = _listingCategoryFeatureOptionService
            .Get(connection => connection.ListingFeatureOptionId == featurOptionId);

        var connectedFeatures = _listingFeatureService
            .Get(feature => feature.FeatureOptionsId == featurOptionId);

        foreach (var connection in connections)
            await _listingCategoryFeatureOptionService.DeleteAsync(connection, saveChanges, cancellationToken);

        foreach (var  feature in connectedFeatures)
            await _listingFeatureService.DeleteAsync(feature, saveChanges, cancellationToken);
    }

    private async ValueTask<ICollection<ListingProperty>> GetListingPropertiesByTypeId(Guid typeId)
    {
        var listingIds = _listingService
            .Get(listing => listing.TypeId == typeId)
            .Select(listing => listing.Id)
            .ToList();

        return await _listingPropertyService.GetAsync(listingIds);
    }
}