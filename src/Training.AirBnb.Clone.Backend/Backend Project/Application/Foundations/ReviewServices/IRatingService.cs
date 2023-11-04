using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ReviewServices;

public interface IRatingService
{
    IQueryable<Rating> Get(Expression<Func<Rating, bool>> predicate);

    ValueTask<ICollection<Rating>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Rating> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Rating> CreateAsync(Rating rating, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Rating> UpdateAsync(Rating rating, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Rating> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Rating> DeleteAsync(Rating rating, bool saveChanges = true, CancellationToken cancellationToken = default);
}