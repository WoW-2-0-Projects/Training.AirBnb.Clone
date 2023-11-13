using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Availabilities.Services;

public interface IAvailabilityProcessingService
{
    ValueTask<BlockedNight> CreateBlockedNightAsync(BlockedNight blockedNight, CancellationToken cancellationToken = default);

    IList<BlockedNight> GetBlockedNightsByListingId(Guid listingId);
}