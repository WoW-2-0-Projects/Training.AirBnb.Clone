using System.Linq.Expressions;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Infrastructure.Common.Validators;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Identity.Services;

public class ListingService(IListingRepository listingRepository, ListingValidator listingValidator)
    : IListingService 
{
    public IQueryable<Listing> Get(
        Expression<Func<Listing, bool>>? predicate = default,
        bool asNoTracking = false)
        => listingRepository.Get(predicate, asNoTracking);

    public ValueTask<IList<Listing>> GetAsync(
        QuerySpecification<Listing> querySpecification,
        CancellationToken cancellationToken = default)
        => listingRepository.GetAsync(querySpecification, cancellationToken);

    public ValueTask<IList<Listing>> GetAsync(
        QuerySpecification querySpecification, 
        CancellationToken cancellationToken = default)
        => listingRepository.GetAsync(querySpecification, cancellationToken);

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