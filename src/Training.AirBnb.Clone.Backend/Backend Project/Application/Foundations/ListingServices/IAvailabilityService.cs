using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IAvailabilityService
{
    IQueryable<Availability> Get(Expression<Func<Availability, bool>> predicate);

    ValueTask<ICollection<Availability>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Availability> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Availability> CreateAsync(Availability availability, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Availability> UpdateAsync(Availability availability, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Availability> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Availability> DeleteAsync(Availability availability, bool saveChanges = true, CancellationToken cancellationToken = default);

}
