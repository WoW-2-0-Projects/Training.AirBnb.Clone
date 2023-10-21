using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.LocationServices;

public class LocationScenicViewsService : IEntityBaseService<LocationScenicViews>
{
    private readonly IDataContext _context;
    public LocationScenicViewsService(IDataContext context)
    {
        _context = context;
    }

    public async ValueTask<LocationScenicViews> CreateAsync(LocationScenicViews locationScenicViews, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(locationScenicViews);

        await _context.LocationScenicViews.AddAsync(locationScenicViews, cancellationToken);

        if(saveChanges) await _context.SaveChangesAsync();

        return locationScenicViews;
    }

    public IQueryable<LocationScenicViews> Get(Expression<Func<LocationScenicViews, bool>> predicate)
    => GetUndeletedLocationScenicViews().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<LocationScenicViews>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    => new ValueTask<ICollection<LocationScenicViews>>(Get(locationScenicView => ids.Contains(locationScenicView.Id)).ToList());

    public ValueTask<LocationScenicViews> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => new ValueTask<LocationScenicViews>
        (Get(locationScenicView => locationScenicView.Id == id).FirstOrDefault() ?? 
        throw new EntityNotFoundException<LocationScenicViews>("Location scenic view not found"));

    /// <summary>
    /// Non-updatable entity
    /// </summary>
    public async ValueTask<LocationScenicViews> UpdateAsync
        (LocationScenicViews locationScenicViews, bool saveChanges = true, CancellationToken cancellationToken = default)
    => throw new InvalidOperationException("Location Scenic  View is non-updatable entity");

    public async ValueTask<LocationScenicViews> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundLocationScenicView = await GetByIdAsync(id);

        _context.LocationScenicViews.RemoveAsync(foundLocationScenicView, cancellationToken);

        if (saveChanges) await _context.SaveChangesAsync();

        return foundLocationScenicView;
    }

    public ValueTask<LocationScenicViews> DeleteAsync(LocationScenicViews locationScenicViews, bool saveChanges = true, CancellationToken cancellationToken = default)
    => DeleteAsync(locationScenicViews.Id, saveChanges, cancellationToken);

    private IQueryable<LocationScenicViews> GetUndeletedLocationScenicViews()
        => _context.LocationScenicViews.Where(locationScenicView => !locationScenicView.IsDeleted)
        .AsQueryable();

    private void Validate(LocationScenicViews locationScenicViews)
    {
        var ScenicViewsOfLocation = Get
            (lsv => lsv.LocationId == locationScenicViews.LocationId);

        if(ScenicViewsOfLocation.FirstOrDefault(scenicView => scenicView.ScenicViewId == locationScenicViews.ScenicViewId) != null)
            throw new DuplicateEntityException<LocationScenicViews>();
    }
}
