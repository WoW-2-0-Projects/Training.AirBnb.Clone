using AutoMapper;
using AutoMapper.Features;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.ListingCategoryDetails.Dtos;
using Backend_Project.Application.ListingCategoryDetails.Services;
using Backend_Project.Application.Listings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Domain.Extensions;

namespace Backend_Project.Infrastructure.CompositionServices;

public class ListingCategoryDetailsService : IListingCategoryDetailsService
{
    private readonly IListingCategoryService _listingCategoryService;
    private readonly IListingFeatureService _listingFeatureService;
    private readonly IListingTypeService _listingTypeService;
    private readonly IListingCategoryTypeService _listingCategoryTypeService;
    private readonly IListingService _listingService;
    private readonly IListingPropertyService _listingPropertyService;
    private readonly IListingPropertyTypeService _listingPropertyTypeService;
    private readonly IMapper _mapper;

    public ListingCategoryDetailsService(IListingCategoryService listingCategoryService,
        IListingFeatureService listingFeatureService,
        IListingTypeService listingTypeService,
        IListingCategoryTypeService listingCategoryTypeService,
        IListingService listingService,
        IListingPropertyService listingPropertyService,
        IListingPropertyTypeService listingPropertyTypeService,
        IMapper mapper)
    {
        _listingCategoryService = listingCategoryService;
        _listingFeatureService = listingFeatureService;
        _listingTypeService = listingTypeService;
        _listingCategoryTypeService = listingCategoryTypeService;
        _listingService = listingService;
        _listingPropertyService = listingPropertyService;
        _listingPropertyTypeService = listingPropertyTypeService;
        _mapper = mapper;
    }

    public async ValueTask<ListingFeatureDto> AddListingFeatureAsync(ListingFeatureDto featureDto, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _listingTypeService.GetByIdAsync(featureDto.ListingTypeId, cancellationToken);

        return _mapper.Map<ListingFeatureDto>(await _listingFeatureService.CreateAsync(_mapper.Map<ListingFeature>(featureDto), saveChanges, cancellationToken));
    }

    public async ValueTask<ListingFeatureDto> UpdateListingFeatureAsync(ListingFeatureDto featureDto, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var listingProperties = await GetListingPropertiesByTypeId(featureDto.ListingTypeId);

        var min = listingProperties.MinBy(self => self.PropertyCount)?.PropertyCount;
        var max = listingProperties.MaxBy(self => self.PropertyCount)?.PropertyCount;

        if (min is not null && min < featureDto.MinValue)
            throw new EntityNotUpdatableException<ListingFeature>($"Not possible to update {nameof(featureDto.MinValue)} of the listing feature. It is exceeding the limits of the current listing properties in use.");

        if (max is not null && max < featureDto.MaxValue)
            throw new EntityNotUpdatableException<ListingFeature>($"Not possible to update {nameof(featureDto.MaxValue)} of the listing feature. It is exceeding the limits of the current listing properties in use.");

        return _mapper.Map<ListingFeatureDto>(await _listingFeatureService.UpdateAsync(_mapper.Map<ListingFeature>(featureDto), saveChanges, cancellationToken));
    }

    public ICollection<ListingFeatureDto> GetListingFeaturesByTypeId(Guid listingTypeId)
       => _listingFeatureService.Get(feature => feature.ListingTypeId == listingTypeId)
        .Select(lf => _mapper.Map<ListingFeatureDto>(lf)).ToList();

    public async ValueTask<ListingFeatureDto> DeleteListingFeatureAsync(Guid featureId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var feature = await _listingFeatureService.GetByIdAsync(featureId, cancellationToken);

        var listingProperties = await GetListingPropertiesByTypeId(feature.ListingTypeId);

        if (listingProperties.Any(self => self.PropertyName == feature.Name))
            throw new EntityNotDeletableException<ListingFeature>("You can't delete this listing feature. There are active listings which have this listing feature.");

        return _mapper.Map<ListingFeatureDto>( await _listingFeatureService.DeleteAsync(_mapper.Map<ListingFeature>( featureId), saveChanges, cancellationToken));
    }

    public async ValueTask<ListingCategoryDto> DeleteCategoryAsync(Guid categoryId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (_listingPropertyTypeService.Get(property => property.CategoryId == categoryId).Any())
            throw new EntityNotDeletableException<ListingCategory>("There are active listings which are in this category.");

        var deletedCategory = await _listingCategoryService.DeleteAsync(categoryId, saveChanges, cancellationToken);

        await DeleteCategoryRelations(categoryId, saveChanges, cancellationToken);

        return _mapper.Map<ListingCategoryDto>(deletedCategory);
    }

    public ValueTask<ICollection<ListingTypeDto>> GetListingTypesByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var relations = _listingCategoryTypeService
            .Get(relation => relation.ListingCategoryId == categoryId);

        var listingTypes = _listingTypeService.Get(type => true);

        var query = from categoryType in relations
                    join type in listingTypes on categoryType.ListingTypeId equals type.Id
                    select type;

        return new(query.Select(lt => _mapper.Map<ListingTypeDto>(lt)).ToList());
    }

    public async ValueTask<ListingTypeDto> DeleteListingTypeAsync(Guid typeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (_listingPropertyTypeService.Get(property => property.TypeId == typeId).Any())
            throw new EntityNotDeletableException<ListingType>("There are active listings of this type.");

        var deletedFeatureOption = await _listingTypeService.DeleteAsync(typeId, saveChanges, cancellationToken);

        await DeleteListingTypesRelations(typeId, saveChanges, cancellationToken);

        return _mapper.Map<ListingTypeDto>(deletedFeatureOption);
    }

    public async ValueTask<ListingCategoryTypeDto> AddListingCategoryTypeAsync(ListingCategoryTypeDto relation, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _listingCategoryService.GetByIdAsync(relation.ListingCategoryId, cancellationToken);
        await _listingTypeService.GetByIdAsync(relation.ListingTypeId, cancellationToken);

        return _mapper.Map<ListingCategoryTypeDto>(await _listingCategoryTypeService.CreateAsync(_mapper.Map<ListingCategoryType>(relation), saveChanges, cancellationToken));
    }

    public async ValueTask<bool> AddListingCategoryTypesAsync(Guid categoryId, List<Guid> listingTypes, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await _listingCategoryService.GetByIdAsync(categoryId, cancellationToken);

        foreach (var type in listingTypes)
        {
            await _listingTypeService.GetByIdAsync(type, cancellationToken);

            var newRelation = new ListingCategoryType()
            {
                ListingCategoryId = categoryId,
                ListingTypeId = type
            };
            await _listingCategoryTypeService.CreateAsync(newRelation, saveChanges, cancellationToken);
        }

        return true;
    }

    public async ValueTask<bool> UpdateListingCategoryTypesAsync(Guid categoryId, List<Guid> updatedListingTypes, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundTypes = await GetListingTypesByCategoryIdAsync(categoryId, cancellationToken);


        var (newTypes, deletedTypes) = foundTypes
            .Select(type => type.Id)
            .GetAddedAndRemovedItems(updatedListingTypes);

        await AddListingCategoryTypesAsync(categoryId, newTypes.ToList(), saveChanges, cancellationToken);
        await DeleteListingCategoryTypes(categoryId, deletedTypes.ToList(), saveChanges, cancellationToken);

        return true;
    }

    private async ValueTask DeleteListingCategoryTypes(Guid categoryId, ICollection<Guid> typesId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var listingCategoryTypes = _listingCategoryTypeService
           .Get(relation => relation.ListingCategoryId == categoryId);

        foreach (var type in typesId)
        {
            var deletedRelationId = listingCategoryTypes
                .Single(relation => relation.ListingTypeId == type).Id;

            await _listingCategoryTypeService.DeleteAsync(deletedRelationId, saveChanges, cancellationToken);
        }
    }

    private async ValueTask DeleteCategoryRelations(Guid categoryId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var connections = _listingCategoryTypeService
            .Get(self => self.ListingCategoryId == categoryId);

        foreach (var connection in connections)
            await _listingCategoryTypeService.DeleteAsync(connection, saveChanges, cancellationToken);
    }

    private async ValueTask DeleteListingTypesRelations(Guid typeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var connections = _listingCategoryTypeService
            .Get(connection => connection.ListingTypeId == typeId);

        var connectedFeatures = _listingFeatureService
            .Get(feature => feature.ListingTypeId == typeId);

        foreach (var connection in connections)
            await _listingCategoryTypeService.DeleteAsync(connection, saveChanges, cancellationToken);

        foreach (var feature in connectedFeatures)
            await _listingFeatureService.DeleteAsync(feature, saveChanges, cancellationToken);
    }

    private ValueTask<ICollection<ListingProperty>> GetListingPropertiesByTypeId(Guid typeId)
    {
        var listingPropertyTypes = _listingPropertyTypeService
            .Get(propertyType => propertyType.TypeId == typeId);

        var listings = _listingService.Get(listing => true);
        var listingProperties = _listingPropertyService.Get(property => true);

        var properties = from propertyTypes in listingPropertyTypes
                         join listing in listings on propertyTypes.Id equals listing.PropertyTypeId
                         join property in listingProperties on listing.Id equals property.ListingId
                         select property;

        return new ValueTask<ICollection<ListingProperty>>(properties.ToList());
    }
}