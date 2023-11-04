using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingAmenitiesService : IListingAmenitiesService
{
    private readonly IDataContext _appDataContext;

    public ListingAmenitiesService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<ListingAmenities> CreateAsync(ListingAmenities listingAmenities, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsUnique(listingAmenities))
            throw new DuplicateEntityException<ListingAmenities>();

        await _appDataContext.ListingAmenities.AddAsync(listingAmenities, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return listingAmenities;
    }

    public ValueTask<ICollection<ListingAmenities>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingAmenities>>(GetUndeletedListingAmenities()
            .Where(self => ids.Contains(self.Id))
            .ToList());

    public ValueTask<ListingAmenities> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingAmenities>(GetUndeletedListingAmenities()
            .FirstOrDefault(self => self.Id == id)
            ?? throw new EntityNotFoundException<ListingAmenities>());

    public IQueryable<ListingAmenities> Get(Expression<Func<ListingAmenities, bool>> predicate)
        => GetUndeletedListingAmenities().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<ListingAmenities> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundConnection = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.ListingAmenities.RemoveAsync(foundConnection, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundConnection;
    }

    public async ValueTask<ListingAmenities> DeleteAsync(ListingAmenities listingAmenities, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(listingAmenities.Id, saveChanges, cancellationToken);

    private bool IsUnique(ListingAmenities listingAmenities)
        => !GetUndeletedListingAmenities()
        .Any(self => self.ListingId == listingAmenities.ListingId && self.AmenityId == listingAmenities.AmenityId);

    private IQueryable<ListingAmenities> GetUndeletedListingAmenities()
        => _appDataContext.ListingAmenities.Where(self => !self.IsDeleted).AsQueryable();
}