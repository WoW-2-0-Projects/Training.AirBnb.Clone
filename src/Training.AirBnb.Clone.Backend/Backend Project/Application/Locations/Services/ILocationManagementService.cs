using Backend_Project.Domain.Entities;
using System.Collections;

namespace Backend_Project.Application.Locations.Services;

public interface ILocationManagementService
{
    ValueTask<Location> CreateLocationByAddressId(Guid addressId);

    ValueTask<Location> UpdateLocation(Location location);

    ValueTask<Location> DeleteLocationById(Guid locationId);

    Address CreateAddress(Address address);
    // validate city

    Address UpdateAddress(Address address);
    // validete city

    // Address GetAddressByListingId(Guid listingId);

    ScenicView UpdateScenicView(ScenicView scenicView);
    // scenicview check LScenic view exists

    ScenicView DeleteScenicView(Guid ScenicViewId);
    // the same update
    bool AddScenicViewsToLocation(IEnumerable<Guid> scenicViewsIds, Guid locationId);
    // check Locationexist and scenicview and create lscenicviews
    // location id bo'yicha scenic view bo'lsa exception;
    bool UpdateLocationScenicViews(IEnumerable<Guid> scenicViews, Guid locationId);
    // 
}
