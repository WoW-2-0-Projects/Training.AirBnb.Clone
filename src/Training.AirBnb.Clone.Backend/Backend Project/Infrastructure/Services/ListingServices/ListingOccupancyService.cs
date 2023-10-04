﻿using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingOccupancyService : IEntityBaseService<ListingOccupancy>
{
    private readonly IDataContext _appDateContext;

    public ListingOccupancyService(IDataContext appDataContext)
    {
        _appDateContext = appDataContext;        
    }

    public async ValueTask<ListingOccupancy> CreateAsync(ListingOccupancy occupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidOccupancy(occupancy))
            throw new EntityValidationException<ListingOccupancy>();

        await _appDateContext.ListingOccupancies.AddAsync(occupancy, cancellationToken);

        if (saveChanges) await _appDateContext.ListingOccupancies.SaveChangesAsync(cancellationToken);

        return occupancy;
    }

    public ValueTask<ICollection<ListingOccupancy>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingOccupancy>>(GetUndeletedListingOccupancies()
            .Where(occupancy => ids.Contains(occupancy.Id))
            .ToList());

    public ValueTask<ListingOccupancy> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingOccupancy>(GetUndeletedListingOccupancies()
            .FirstOrDefault(occupancy => occupancy.Id == id)
            ?? throw new EntityNotFoundException<ListingOccupancy>());

    public IQueryable<ListingOccupancy> Get(Expression<Func<ListingOccupancy, bool>> predicate)
        => GetUndeletedListingOccupancies().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<ListingOccupancy> UpdateAsync(ListingOccupancy occupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidOccupancy(occupancy))
            throw new EntityValidationException<ListingOccupancy>();

        var foundOccupancy = await GetByIdAsync(occupancy.Id, cancellationToken);

        foundOccupancy.Guests = occupancy.Guests;
        foundOccupancy.AllowPets = occupancy.AllowPets;
        foundOccupancy.ModifiedDate = DateTime.UtcNow;

        if (saveChanges) await _appDateContext.ListingOccupancies.SaveChangesAsync(cancellationToken);

        return foundOccupancy;
    }

    public async ValueTask<ListingOccupancy> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundOccupancy = await GetByIdAsync(id, cancellationToken);

        foundOccupancy.IsDeleted = true;
        foundOccupancy.DeletedDate = DateTime.UtcNow;

        if (saveChanges) await _appDateContext.ListingOccupancies.SaveChangesAsync(cancellationToken);

        return foundOccupancy;
    }

    public async ValueTask<ListingOccupancy> DeleteAsync(ListingOccupancy occupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(occupancy.Id, saveChanges, cancellationToken);

    private bool IsValidOccupancy(ListingOccupancy occupancy)
        => occupancy.Guests >= 1;

    private IQueryable<ListingOccupancy> GetUndeletedListingOccupancies()
        => _appDateContext.ListingOccupancies.Where(occupancy => !occupancy.IsDeleted).AsQueryable();
}