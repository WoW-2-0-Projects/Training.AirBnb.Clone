using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingPropertyTypeService : IEntityBaseService<ListingPropertyType>
{
    private readonly IDataContext _appDataContext;

    public ListingPropertyTypeService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
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
        if (listingPropertyType.FloorsCount < 1 || listingPropertyType.FloorsCount > 180)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's FloorsCount isn't valid!");

        if (listingPropertyType.ListingFloor < 0 || listingPropertyType.ListingFloor > listingPropertyType.FloorsCount)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's ListingFloor isn't valid!");

        if (listingPropertyType.YearBuilt < 1900 || listingPropertyType.YearBuilt > DateTime.UtcNow.Year)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's YearBuilt isn't valid!");

        if (listingPropertyType.PropertySize < 0)
            throw new EntityValidationException<ListingPropertyType>("Listing property type's PropertySize isn't valid!");
    }

    private IQueryable<ListingPropertyType> GetUndeletedListingPropertyType()
        => _appDataContext.ListingPropertyTypes.Where(property => !property.IsDeleted).AsQueryable();

}
