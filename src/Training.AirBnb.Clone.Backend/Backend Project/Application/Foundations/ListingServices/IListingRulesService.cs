using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingRulesService
{
    IQueryable<ListingRules> Get(Expression<Func<ListingRules, bool>> predicate);

    ValueTask<ICollection<ListingRules>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingRules> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingRules> CreateAsync(ListingRules listingRules, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRules> UpdateAsync(ListingRules listingRules, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRules> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRules> DeleteAsync(ListingRules listingRules, bool saveChanges = true, CancellationToken cancellationToken = default);
}