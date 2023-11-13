using Backend_Project.Application.Availabilities.Services;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.CompositionServices;

public class AvailabilityProcessingService : IAvailabilityProcessingService
{
    private readonly IListingService _listingService;
    private readonly IBlockedNightService _blockedNightService;

    public AvailabilityProcessingService(
        IListingService listingService,
        IBlockedNightService blockedNightService
    )
    {
        _listingService = listingService;
        _blockedNightService = blockedNightService;
    }

    public IList<BlockedNight> GetBlockedNightsByListingId(Guid listingId) =>
        _blockedNightService.Get(night => night.ListingId == listingId).ToList();

    public async ValueTask<BlockedNight> CreateBlockedNightAsync(BlockedNight blockedNight, CancellationToken cancellationToken = default)
    {
        await _listingService.GetByIdAsync(blockedNight.ListingId, cancellationToken);

        return await _blockedNightService.CreateAsync(blockedNight, cancellationToken: cancellationToken);
    }
}