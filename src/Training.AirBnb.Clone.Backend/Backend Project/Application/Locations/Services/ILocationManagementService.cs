using Backend_Project.Domain.Entities;
using System.Collections;

namespace Backend_Project.Application.Locations.Services;

public interface ILocationManagementService
{
    ValueTask<Location> CreateLocationByAddressId(Guid addressId);

    ValueTask<Location> UpdateLocation(Location location);

    ValueTask<Location> DeleteLocationById(Guid locationId);

    ValueTask<Address> CreateAddress(Address address);

    ValueTask<Address> UpdateAddress(Address address);

    ValueTask<ScenicView> UpdateScenicView(ScenicView scenicView);

    ValueTask<ScenicView> DeleteScenicView(Guid ScenicViewId);
  
    bool AddScenicViewsToLocation(IEnumerable<Guid> scenicViewsIds, Guid locationId);
    // check Locationexist and scenicview and create lscenicviews
    // location id bo'yicha scenic view bo'lsa exception;
    bool UpdateLocationScenicViews(IEnumerable<Guid> scenicViews, Guid locationId);
    // 
}
