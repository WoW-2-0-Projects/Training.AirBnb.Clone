using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.LocationServices;

public class LocationService : IEntityBaseService<Location>
{
    private readonly IDataContext _appDataContext;

    public LocationService(IDataContext dataContext)
    {
        _appDataContext = dataContext;
    }

    public async ValueTask<Location> CreateAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(location);

        await _appDataContext.Locations.AddAsync(location, cancellationToken);
        
        if(saveChanges) await _appDataContext.SaveChangesAsync();

        return location;
    }

    public IQueryable<Location> Get(Expression<Func<Location, bool>> predicate) 
        => GetUndeletedLocations().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<Location>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<Location>>
        (Get(location => 
        ids.Contains(location.Id))
            .ToList());

    public ValueTask<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<Location>(Get(location => location.Id == id).FirstOrDefault() 
            ?? throw new EntityNotFoundException<Location>("Locationn not found."));

    public async ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(location);

        var foundLocation = await GetByIdAsync(location.Id, cancellationToken);

        foundLocation.NeighborhoodDescription = location.NeighborhoodDescription;
        foundLocation.GettingAround = location.GettingAround;
        await _appDataContext.Locations.UpdateAsync(foundLocation, cancellationToken);

        if(saveChanges) await _appDataContext.SaveChangesAsync();

        return foundLocation;
    }
    public async ValueTask<Location> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundLocation = await GetByIdAsync(id);

        await _appDataContext.Locations.RemoveAsync(foundLocation, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();
        
        return foundLocation;
    }

    public ValueTask<Location> DeleteAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default) => DeleteAsync(location.Id, saveChanges, cancellationToken);

    public IQueryable<Location> GetUndeletedLocations()
    => _appDataContext.Locations.Where(location => !location.IsDeleted).AsQueryable();

    private bool IsUnique(Location givenLocation)
        => !GetUndeletedLocations().Any(location => location.AddressId == givenLocation.AddressId);

    private void Validate(Location location)
    {
        if (!IsUnique(location)) throw new DuplicateEntityException<Location>();

        if (!_appDataContext.Addresses.Select(address => address.Id).Contains(location.AddressId))
            throw new EntityValidationException<Address>("Invalid address id!");
    }
}