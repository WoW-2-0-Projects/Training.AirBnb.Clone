using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IBlockedNightService
{
    IQueryable<BlockedNight> Get(Expression<Func<BlockedNight, bool>> predicate);

    ValueTask<ICollection<BlockedNight>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<BlockedNight> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<BlockedNight> CreateAsync(BlockedNight blockedNight, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<BlockedNight> UpdateAsync(BlockedNight blockedNight, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<BlockedNight> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<BlockedNight> DeleteAsync(BlockedNight blockedNight, bool saveChanges = true, CancellationToken cancellationToken = default);
}