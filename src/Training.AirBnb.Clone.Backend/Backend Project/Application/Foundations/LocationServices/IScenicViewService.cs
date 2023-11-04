using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.LocationServices;

public interface IScenicViewService
{
    IQueryable<ScenicView> Get(Expression<Func<ScenicView, bool>> predicate);

    ValueTask<ICollection<ScenicView>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ScenicView> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ScenicView> CreateAsync(ScenicView scenicView, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ScenicView> UpdateAsync(ScenicView scenicView, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ScenicView> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ScenicView> DeleteAsync(ScenicView scenicView, bool saveChanges = true, CancellationToken cancellationToken = default);
}