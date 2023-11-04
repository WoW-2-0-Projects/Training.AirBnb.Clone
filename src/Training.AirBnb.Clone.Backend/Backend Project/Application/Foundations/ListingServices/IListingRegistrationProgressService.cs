using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingRegistrationProgressService
{
    IQueryable<ListingRegistrationProgress> Get(Expression<Func<ListingRegistrationProgress, bool>> predicate);

    ValueTask<ICollection<ListingRegistrationProgress>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingRegistrationProgress> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingRegistrationProgress> CreateAsync(ListingRegistrationProgress listingRegistrationProgress, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRegistrationProgress> UpdateAsync(ListingRegistrationProgress listingRegistrationProgress, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRegistrationProgress> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRegistrationProgress> DeleteAsync(ListingRegistrationProgress listingRegistrationProgress, bool saveChanges = true, CancellationToken cancellationToken = default);
}