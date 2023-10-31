using Backend_Project.Application.Entity;
using Backend_Project.Application.Listings.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingPropertyTypeService : IEntityBaseService<ListingPropertyType>
{
    private readonly IDataContext _appDataContext;
    private readonly ListingPropertyTypeSettings _propertyTypeSetting;

    public ListingPropertyTypeService(IDataContext appDataContext, IOptions<ListingPropertyTypeSettings> propertyTypeSettings)
    {
        _appDataContext = appDataContext;
        _propertyTypeSetting = propertyTypeSettings.Value;
    }

    public async ValueTask<ListingPropertyType> CreateAsync(ListingPropertyType listingPropertyType, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateListingPropertyType(listingPropertyType);

        await _appDataContext.ListingPropertyTypes.AddAsync(listingPropertyType, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return listingPropertyType;
    }

    public IQueryable<ListingPropertyType> Get(Expression<Func<ListingPropertyType, bool>> predicate)
        => GetUndeletedListingPropertyType().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<ListingPropertyType>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingPropertyType>>(GetUndeletedListingPropertyType()
            .Where(listingPropertyType => ids.Contains(listingPropertyType.Id))
            .ToList());

    public ValueTask<ListingPropertyType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingPropertyType>(GetUndeletedListingPropertyType()
            .FirstOrDefault(listingPropertyType => listingPropertyType.Id.Equals(id))
            ?? throw new EntityNotFoundException<ListingPropertyType>("Listing property type not found!"));

    public async ValueTask<ListingPropertyType> UpdateAsync(ListingPropertyType listingPropertyType, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateListingPropertyType(listingPropertyType);

        var foundListingPropertyType = await GetByIdAsync(listingPropertyType.Id, cancellationToken);

        foundListingPropertyType.CategoryId = listingPropertyType.CategoryId;
        foundListingPropertyType.TypeId = listingPropertyType.TypeId;
        foundListingPropertyType.FloorsCount = listingPropertyType.FloorsCount;
        foundListingPropertyType.ListingFloor = listingPropertyType.ListingFloor;
        foundListingPropertyType.YearBuilt = listingPropertyType.YearBuilt;
        foundListingPropertyType.PropertySize = listingPropertyType.PropertySize;
        foundListingPropertyType.UnitOfSize = listingPropertyType.UnitOfSize;

        await _appDataContext.ListingPropertyTypes.UpdateAsync(foundListingPropertyType, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundListingPropertyType;
    }

    public async ValueTask<ListingPropertyType> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var deletedListingPropertyType = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.ListingPropertyTypes.RemoveAsync(deletedListingPropertyType, cancellationToken);

        if(saveChanges) await _appDataContext.SaveChangesAsync();

        return deletedListingPropertyType;
    }

    public async ValueTask<ListingPropertyType> DeleteAsync(ListingPropertyType listingPropertyType, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(listingPropertyType.Id, saveChanges, cancellationToken);

    private void ValidateListingPropertyType(ListingPropertyType listingPropertyType)
    {
        if (listingPropertyType.FloorsCount < _propertyTypeSetting.MinFloorsCount || listingPropertyType.FloorsCount > _propertyTypeSetting.MaxFloorsCount)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's FloorsCount isn't valid!");

        if (listingPropertyType.ListingFloor < _propertyTypeSetting.MinListingFloor || listingPropertyType.ListingFloor > listingPropertyType.FloorsCount)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's ListingFloor isn't valid!");

        if (listingPropertyType.YearBuilt < _propertyTypeSetting.BuiltYearMinValue || listingPropertyType.YearBuilt > DateTime.UtcNow.Year)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's YearBuilt isn't valid!");

        if (listingPropertyType.PropertySize < _propertyTypeSetting.PropertySizeMinValue)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's PropertySize isn't valid!");

        if (listingPropertyType.PropertySize is not null && listingPropertyType.UnitOfSize is null)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's Unit of size isn't valid");
    }

    private IQueryable<ListingPropertyType> GetUndeletedListingPropertyType()
        => _appDataContext.ListingPropertyTypes.Where(property => !property.IsDeleted).AsQueryable();
}