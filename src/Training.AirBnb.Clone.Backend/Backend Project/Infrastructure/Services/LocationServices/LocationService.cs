using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;
using System.Reflection.Metadata;

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
        await _appDataContext.Locations.AddAsync(location, cancellationToken);

        if(saveChanges) await _appDataContext.SaveChangesAsync();

        return location;
    }

    public IQueryable<Location> Get(Expression<Func<Location, bool>> predicate) 
        => _appDataContext.Locations.Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<Location>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<Location>>
        (Get(location => 
        ids.Contains(location.Id))
            .ToList());

    public async ValueTask<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _appDataContext.Locations.FindAsync(id) ?? throw new EntityNotFoundException<Location>("Locationn not found.");

    public async ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundLocation = await GetByIdAsync(location.Id, cancellationToken);

        foundLocation.AddressId = location.AddressId;
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
}
