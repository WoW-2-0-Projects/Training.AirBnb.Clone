using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingFeatureService : IEntityBaseService<ListingFeature>
{
    private readonly IDataContext _appDataContext;

    public ListingFeatureService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<ListingFeature> CreateAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateOnCreate(feature);

        await _appDataContext.ListingFeatures.AddAsync(feature, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return feature;
    }

    public ValueTask<ICollection<ListingFeature>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingFeature>>(GetUndeletedFeatures()
                .Where(feature => ids.Contains(feature.Id))
                .ToList());

    public ValueTask<ListingFeature> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingFeature>(GetUndeletedFeatures()
            .FirstOrDefault(feature => feature.Id == id)
            ?? throw new EntityNotFoundException<ListingFeature>("Listing Feature not found!"));

    public IQueryable<ListingFeature> Get(Expression<Func<ListingFeature, bool>> predicate)
        => GetUndeletedFeatures().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<ListingFeature> UpdateAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundFeature = await GetByIdAsync(feature.Id, cancellationToken);

        if (!ValidateOnUpdate(feature, foundFeature))
            throw new EntityValidationException<ListingFeature>("Not valid listing feature.");

        foundFeature.Name = feature.Name;
        foundFeature.MinValue = feature.MinValue;
        foundFeature.MaxValue = feature.MaxValue;   
        foundFeature.FeatureOptionsId = feature.FeatureOptionsId;

        await _appDataContext.ListingFeatures.UpdateAsync(foundFeature, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundFeature;
    }

    public async ValueTask<ListingFeature> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundFeature = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.ListingFeatures.RemoveAsync(foundFeature, cancellationToken);

        if (saveChanges) await _appDataContext.ListingFeatures.SaveChangesAsync(cancellationToken);

        return foundFeature;
    }

    public async ValueTask<ListingFeature> DeleteAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(feature.Id, saveChanges, cancellationToken);

    private void ValidateOnCreate(ListingFeature feature)
    {
        if (!IsValidFeature(feature))
            throw new EntityValidationException<ListingFeature>("Not valid feature!");

        if (FeatureExists(feature))
            throw new DuplicateEntityException<ListingFeature>("Feature already exists!");
    }

    private bool ValidateOnUpdate(ListingFeature feature, ListingFeature existingFeature)
    {
        if (existingFeature.Name == feature.Name && existingFeature.FeatureOptionsId == feature.FeatureOptionsId)
            return IsValidFeature(feature);

        if (FeatureExists(feature))
            throw new DuplicateEntityException<ListingFeature>("Listing feature already exists.");

        return true;
    }

    private bool IsValidFeature(ListingFeature feature)
        => !string.IsNullOrWhiteSpace(feature.Name)
            && feature.Name.Length > 2
            && feature.MinValue >= 0;

    private bool FeatureExists(ListingFeature feature)
        => GetUndeletedFeatures()
            .Any(self => self.Name == feature.Name 
                && self.FeatureOptionsId == feature.FeatureOptionsId);

    private IQueryable<ListingFeature> GetUndeletedFeatures()
        => _appDataContext.ListingFeatures.Where(feature => !feature.IsDeleted).AsQueryable();
}