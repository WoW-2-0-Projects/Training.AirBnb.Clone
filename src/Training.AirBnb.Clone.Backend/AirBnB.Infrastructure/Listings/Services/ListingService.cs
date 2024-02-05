using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Infrastructure.Common.Validators;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Listings.Services;

public class ListingService(
    IListingRepository listingRepository,
    ListingValidator listingValidator,
    IRequestUserContextProvider userContextProvider)
    : IListingService 
{
    public IQueryable<Listing> Get(
        FilterPagination filterPagination,
        bool asNoTracking = false)
    {
        return listingRepository.Get(asNoTracking: asNoTracking)
            .Include(listing => listing.ImagesStorageFile
                .OrderBy(image => image.OrderNumber))
            .ThenInclude(media => media.StorageFile)
            .Skip((int)((filterPagination.PageToken - 1) * filterPagination.PageSize))
            .Take((int)filterPagination.PageSize);
    }

    public ValueTask<Listing?> GetByIdAsync(
        Guid listingId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => listingRepository.GetByIdAsync(listingId, asNoTracking, cancellationToken);

    public ValueTask<IList<Listing>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => listingRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    
    public ValueTask<Listing> CreateAsync(
        Listing listing,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        listing.HostId = userContextProvider.GetUserId();
        
        var validationResult = listingValidator
            .Validate(listing,
                options =>
                    options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return listingRepository.CreateAsync(listing, saveChanges, cancellationToken);
    }

    public ValueTask<Listing> UpdateAsync(
        Listing listing,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
        => listingRepository.UpdateAsync(listing, saveChanges, cancellationToken);

    public ValueTask<Listing?> DeleteAsync(
        Listing listing,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
        => listingRepository.DeleteAsync(listing, saveChanges, cancellationToken);

    public ValueTask<Listing?> DeleteByIdAsync(
        Guid listingId, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
        => listingRepository.DeleteByIdAsync(listingId, saveChanges, cancellationToken);
}