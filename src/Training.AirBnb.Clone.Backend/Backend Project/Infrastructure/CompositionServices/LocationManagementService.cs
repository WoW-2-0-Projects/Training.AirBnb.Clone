using Backend_Project.Application.Foundations.LocationServices;
using Backend_Project.Application.Locations.Services;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Domain.Extensions;

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
        await _addressService.GetByIdAsync(location.AddressId);

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

        var address = _addressService
            .Get(self => true)
            .FirstOrDefault(address => address.Id.Equals(deletedLocation.AddressId))
                ?? throw new EntityNotFoundException<Address>("Address not found!");

        await DeleteLocationScenicViewsList(locationScenicViews);
        await _addressService.DeleteAsync(address);

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

        if (locationScenicViews.Any())
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

        if (locationScenicViews.Any())
            throw new EntityNotDeletableException<ScenicView>("this scenicview not deletable");

        return await _scenicViewService.UpdateAsync(scenicView);
    }

    public async ValueTask<bool> AddScenicViewsToLocation(IEnumerable<Guid> scenicViewsIds, Guid locationId)
    {
        var location = _locationService.GetByIdAsync(locationId);

        var locationScenicViews = _locationScenicViewsService
            .Get(self => true)
            .Where(ls => ls.LocationId.Equals(locationId)).ToList();

        if (locationScenicViews.Any())
            throw new DuplicateEntityException<LocationScenicViews>("this location already exists");

        _ = await _locationService.GetByIdAsync(locationId);
        await AddLocationScenicViews(scenicViewsIds, locationId);

        return true;
    }

    public async ValueTask<bool> UpdateLocationScenicViews(IEnumerable<Guid> scenicViewsIds, Guid locationId)
    {
        await CheckScenicViewsAsync(scenicViewsIds);

        var location = _locationService.GetByIdAsync(locationId);

        var oldLocationScenicViews = _locationScenicViewsService
            .Get(self => self.LocationId == locationId)
            .Select(locationscenic => locationscenic.ScenicViewId)
            .ToList();

        var (addedItems, removedItems) = oldLocationScenicViews.GetAddedAndRemovedItems(scenicViewsIds);

        await RemoveLocatinScenicViews(removedItems);
        await AddLocationScenicViews(addedItems, locationId);

        return true;
    }

    private async ValueTask DeleteLocationScenicViewsList(List<LocationScenicViews> scenicViewsList)
    {
        foreach (var item in scenicViewsList)
            await _locationScenicViewsService.DeleteAsync(item);
    }

    private async ValueTask AddLocationScenicViews(IEnumerable<Guid> scenicViewsIds, Guid locationId)
    {
        foreach (var item in scenicViewsIds)
        {
            var scenicView = await _scenicViewService.GetByIdAsync(item);

            await _locationScenicViewsService.CreateAsync(new LocationScenicViews
            {
                LocationId = locationId,
                ScenicViewId = scenicView.Id
            });
        }
    }

    private async ValueTask RemoveLocatinScenicViews(IEnumerable<Guid> scenicViewsIds)
    {
        foreach (var item in scenicViewsIds)
            await _locationScenicViewsService.DeleteAsync(item);
    }

    private async ValueTask CheckScenicViewsAsync(IEnumerable<Guid> scenicViewsIds)
    {
        foreach (var item in scenicViewsIds)
            await _scenicViewService.GetByIdAsync(item);
    }
}
