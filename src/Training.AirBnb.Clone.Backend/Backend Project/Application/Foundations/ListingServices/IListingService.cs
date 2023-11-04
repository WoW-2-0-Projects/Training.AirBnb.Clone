using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingService
{
    IQueryable<Listing> Get(Expression<Func<Listing, bool>> predicate);

    ValueTask<ICollection<Listing>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Listing> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Listing> CreateAsync(Listing listing, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Listing> UpdateAsync(Listing listing, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Listing> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Listing> DeleteAsync(Listing listing, bool saveChanges = true, CancellationToken cancellationToken = default);
}