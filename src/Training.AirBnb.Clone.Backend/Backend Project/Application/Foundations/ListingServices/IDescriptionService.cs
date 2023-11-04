using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IDescriptionService
{
    IQueryable<Description> Get(Expression<Func<Description, bool>> predicate);

    ValueTask<ICollection<Description>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Description> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Description> CreateAsync(Description description, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Description> UpdateAsync(Description description, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Description> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Description> DeleteAsync(Description description, bool saveChanges = true, CancellationToken cancellationToken = default);
}