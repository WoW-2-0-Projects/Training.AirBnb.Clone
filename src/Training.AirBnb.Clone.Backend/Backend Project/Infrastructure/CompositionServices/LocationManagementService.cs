using Backend_Project.Application.Foundations.LocationServices;
using Backend_Project.Application.Locations.Services;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;

namespace Backend_Project.Infrastructure.CompositionServices;

public class LocationManagementService : ILocationManagementService
{
    private readonly IAddressService _addressService;
    private readonly ILocationService _locationService;
    private readonly IScenicViewService _scenicViewService;
    private readonly ILocationScenicViewsService _locationScenicViewsService;
    private readonly ICityService _cityService;

    public LocationManagementService(IAddressService addressService, ILocationService locationService, IScenicViewService scenicViewService, ILocationScenicViewsService locationScenicViewsService, ICityService cityService)
    {
        _addressService = addressService;
        _locationService = locationService;
        _scenicViewService = scenicViewService;
        _locationScenicViewsService = locationScenicViewsService;
        _cityService = cityService;
    }

    public async ValueTask<Location> CreateLocationByAddressId(Guid addressId)
    {
        _ = await _addressService.GetByIdAsync(addressId);

        var location = await _locationService.CreateAsync(new Location { AddressId = addressId });

        return location;
    }

    public async ValueTask<Location> UpdateLocation(Location location)
    {
        _ = _addressService.GetByIdAsync(location.AddressId);

        var updatedLocation = await _locationService.UpdateAsync(location);

        return updatedLocation;
    }

    public async ValueTask<Location> DeleteLocationById(Guid locationId)
    {
        var deletedLocation = await _locationService.GetByIdAsync(locationId);

        var locationScenicViews = _locationScenicViewsService
            .Get(self => true)
            .Where(locViews => locViews.LocationId == locationId)
            .ToList();

        var addresses = _addressService
            .Get(self => true)
            .Where(address => address.Id.Equals(deletedLocation.AddressId))
            .ToList();

        await DeleteLocationScenicViewsList(locationScenicViews);
        await DeleteAdressList(addresses);

        return await _locationService.DeleteAsync(deletedLocation);
    }

    public async ValueTask<Address> CreateAddress(Address address)
    {
        _ = await _cityService.GetByIdAsync(address.CityId);

        var createdAdress = await _addressService.CreateAsync(address);

        return createdAdress;
    }

    public async ValueTask<Address> UpdateAddress(Address address)
    {
        _ = await _cityService.GetByIdAsync(address.CityId);

        var updatedAdress = await _addressService.UpdateAsync(address);

        return updatedAdress;
    }

    public async ValueTask<ScenicView> UpdateScenicView(ScenicView scenicView)
    {
        var locationScenicViews = _locationScenicViewsService
            .Get(self => true)
            .Where(ls => ls.ScenicViewId.Equals(scenicView.Id)).ToList();

        if (locationScenicViews is not null)
            throw new EntityNotUpdatableException<ScenicView>("this scenic view not updatable");

        return await _scenicViewService.UpdateAsync(scenicView);
    }

    public async ValueTask<ScenicView> DeleteScenicView(Guid ScenicViewId)
    {
        var scenicView = await _scenicViewService.GetByIdAsync(ScenicViewId);

        var locationScenicViews = _locationScenicViewsService
            .Get(self => true)
            .Where(ls => ls.ScenicViewId.Equals(scenicView.Id))
            .ToList();

        if (locationScenicViews is not null)
            throw new EntityNotDeletableException<ScenicView>("this scenicview not deletable");

        return await _scenicViewService.UpdateAsync(scenicView);
    }

    public bool AddScenicViewsToLocation(IEnumerable<Guid> scenicViewsIds, Guid locationId)
    {
        throw new NotImplementedException();
        //
    }

    public bool UpdateLocationScenicViews(IEnumerable<Guid> scenicViewsIds, Guid locationId)
    {
        throw new NotImplementedException();
    }

    private async ValueTask DeleteLocationScenicViewsList(List<LocationScenicViews> scenicViewsList)
    {
        foreach (var item in scenicViewsList)
            await _locationScenicViewsService.DeleteAsync(item);
    }

    private async ValueTask DeleteAdressList(List<Address> addresses)
    {
        foreach (var item in addresses)
            await _addressService.DeleteAsync(item);
    }
}
