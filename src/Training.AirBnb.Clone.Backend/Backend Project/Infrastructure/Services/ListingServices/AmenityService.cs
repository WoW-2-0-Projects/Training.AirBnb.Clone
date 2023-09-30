﻿using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class AmenityService : IEntityBaseService<Amenity>
{
    private readonly IDataContext _appDataContext;

    public AmenityService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<Amenity> CreateAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidAmenity(amenity))
            throw new EntityValidationException<Amenity>("Invalid amenity!");

        if (!IsUnique(amenity.AmenityName))
            throw new DuplicateEntityException<Amenity>();

        await _appDataContext.Amenities.AddAsync(amenity, cancellationToken);

        if (saveChanges) await _appDataContext.Amenities.SaveChangesAsync(cancellationToken);

        return amenity;
    }

    public ValueTask<ICollection<Amenity>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    => new ValueTask<ICollection<Amenity>>(GetUndeletedAmenities()
        .Where(amenity => ids
            .Contains(amenity.Id))
        .ToList());

    public ValueTask<Amenity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => new ValueTask<Amenity>(GetUndeletedAmenities()
        .FirstOrDefault(amenity => amenity.Id == id)
        ?? throw new EntityNotFoundException<Amenity>());

    public IQueryable<Amenity> Get(Expression<Func<Amenity, bool>> predicate)
    => GetUndeletedAmenities().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<Amenity> UpdateAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundAmenity = await GetByIdAsync(amenity.Id);

        if (!IsValidAmenity(amenity))
            throw new EntityValidationException<Amenity>("Invalid Amenity!");

        foundAmenity.AmenityName = amenity.AmenityName;
        foundAmenity.CategoryId = amenity.CategoryId;
        foundAmenity.ModifiedDate = DateTime.UtcNow;

        if (saveChanges) await _appDataContext.Amenities.SaveChangesAsync(cancellationToken);

        return foundAmenity;
    }

    public async ValueTask<Amenity> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundAmenity = await GetByIdAsync(id, cancellationToken);

        foundAmenity.IsDeleted = true;
        foundAmenity.DeletedDate = DateTime.UtcNow;

        if (saveChanges) await _appDataContext.Amenities.SaveChangesAsync(cancellationToken);

        return foundAmenity;
    }

    public async ValueTask<Amenity> DeleteAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(amenity.Id, saveChanges, cancellationToken);

    private bool IsValidAmenity(Amenity amenity)
        => !string.IsNullOrEmpty(amenity.AmenityName)
            && amenity.AmenityName.Length > 2
            && amenity.CategoryId != default;

    private bool IsUnique(string amenity)
        => !GetUndeletedAmenities().Any(self => self.AmenityName.Equals(amenity));

    private IQueryable<Amenity> GetUndeletedAmenities()
        => _appDataContext.Amenities.Where(amenity => !amenity.IsDeleted).AsQueryable();
}