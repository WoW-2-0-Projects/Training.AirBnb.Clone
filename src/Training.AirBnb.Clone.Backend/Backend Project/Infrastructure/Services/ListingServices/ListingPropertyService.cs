using Backend_Project.Application.Entity;
using Backend_Project.Application.Listings.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingPropertyService : IEntityBaseService<ListingProperty>
{
    private readonly IDataContext _appDataContext;
    private readonly ListingRulesSettings _propertySettings;

    public ListingPropertyService(IDataContext appDataContext, IOptions<ListingRulesSettings> propertySettings)
    {
        _appDataContext = appDataContext;
        _propertySettings = propertySettings.Value;
    }

    public async ValueTask<ListingProperty> CreateAsync(ListingProperty property, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateProperty(property);

        await _appDataContext.ListingProperties.AddAsync(property, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return property;
    }

    public ValueTask<ICollection<ListingProperty>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingProperty>>(GetUndeletedProperties()
        .Where(property => ids.Contains(property.Id))
        .ToList());

    public ValueTask<ListingProperty> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingProperty>(GetUndeletedProperties()
        .FirstOrDefault(property => property.Id == id)
        ?? throw new EntityNotFoundException<ListingProperty>());

    public IQueryable<ListingProperty> Get(Expression<Func<ListingProperty, bool>> predicate)
        => GetUndeletedProperties().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<ListingProperty> UpdateAsync(ListingProperty property, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateProperty(property);

        var foundProperty = await GetByIdAsync(property.Id, cancellationToken);

        foundProperty.PropertyName = property.PropertyName;
        foundProperty.PropertyCount = property.PropertyCount;
        foundProperty.IsShared = property.IsShared;
        
        await _appDataContext.ListingProperties.UpdateAsync(foundProperty, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundProperty;
    }

    public async ValueTask<ListingProperty> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundProperty = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.ListingProperties.RemoveAsync(foundProperty, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundProperty;
    }

    public async ValueTask<ListingProperty> DeleteAsync(ListingProperty property, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(property.Id, saveChanges, cancellationToken);

    private void ValidateProperty(ListingProperty property)
    {
        if (IsValidProperty(property))
            throw new EntityValidationException<ListingProperty>();

        if (!IsUniqueProperty(property))
            throw new DuplicateEntityException<ListingProperty>();
    }

    private bool IsValidProperty(ListingProperty property)
    {
        if (string.IsNullOrWhiteSpace(property.PropertyName)) return false;
        if (property.PropertyCount < _propertySettings.PropertyMinValue) return false;

        return true;
    }

    private bool IsUniqueProperty(ListingProperty property)
        => !GetUndeletedProperties().Any(self => self.PropertyName == property.PropertyName
        && self.PropertyCount == property.PropertyCount
        && self.ListingId == property.ListingId
        && self.IsShared == property.IsShared);

    private IQueryable<ListingProperty> GetUndeletedProperties()
        => _appDataContext.ListingProperties.Where(property => !property.IsDeleted).AsQueryable();
}