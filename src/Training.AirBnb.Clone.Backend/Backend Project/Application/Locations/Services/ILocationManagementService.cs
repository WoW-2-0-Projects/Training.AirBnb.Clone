using Backend_Project.Domain.Entities;

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
  
    ValueTask<bool> AddScenicViewsToLocation(IEnumerable<Guid> scenicViewsIds, Guid locationId);
    
    ValueTask<bool> UpdateLocationScenicViews(IEnumerable<Guid> scenicViews, Guid locationId);   
}
