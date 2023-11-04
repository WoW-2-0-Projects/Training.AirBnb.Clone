using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.LocationServices;

public interface ILocationScenicViewsService
{
    IQueryable<LocationScenicViews> Get(Expression<Func<LocationScenicViews, bool>> predicate);

    ValueTask<ICollection<LocationScenicViews>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<LocationScenicViews> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<LocationScenicViews> CreateAsync(LocationScenicViews locationScenicViews, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<LocationScenicViews> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<LocationScenicViews> DeleteAsync(LocationScenicViews locationScenicViews, bool saveChanges = true, CancellationToken cancellationToken = default);

}