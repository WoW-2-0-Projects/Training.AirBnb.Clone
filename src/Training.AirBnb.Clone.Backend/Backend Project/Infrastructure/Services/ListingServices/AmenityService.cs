using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class AmenityService : IAmenityService
{
    private readonly IDataContext _appDataContext;

    public AmenityService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<Amenity> CreateAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateAmenity(amenity);

        await _appDataContext.Amenities.AddAsync(amenity, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return amenity;
    }

    public ValueTask<ICollection<Amenity>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<Amenity>>(GetUndeletedAmenities()
            .Where(amenity => ids.Contains(amenity.Id))
            .ToList());

    public  ValueTask<Amenity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var a = GetUndeletedAmenities()
            .FirstOrDefault(amenity => amenity.Id.Equals(id));

            if( a is null)
                throw new EntityNotFoundException<Amenity>("Amenity not found.");

        return new ValueTask<Amenity>( a);
    }

    public IQueryable<Amenity> Get(Expression<Func<Amenity, bool>> predicate)
        => GetUndeletedAmenities().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<Amenity> UpdateAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundAmenity = await GetByIdAsync(amenity.Id);

        ValidateAmenity(amenity);

        foundAmenity.AmenityName = amenity.AmenityName;
        foundAmenity.CategoryId = amenity.CategoryId;

        await _appDataContext.Amenities.UpdateAsync(foundAmenity, cancellationToken);
        if (saveChanges) await _appDataContext.Amenities.SaveChangesAsync(cancellationToken);

        return foundAmenity;
    }

    public async ValueTask<Amenity> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundAmenity = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.Amenities.RemoveAsync(foundAmenity, cancellationToken);

        if (saveChanges) await _appDataContext.Amenities.SaveChangesAsync(cancellationToken);

        return foundAmenity;
    }

    public async ValueTask<Amenity> DeleteAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(amenity.Id, saveChanges, cancellationToken);

    private void ValidateAmenity(Amenity amenity)
    {
        if (!IsValidAmenity(amenity))
            throw new EntityValidationException<Amenity>("Invalid amenity!");

        if (!IsUnique(amenity))
            throw new DuplicateEntityException<Amenity>();
    }

    private bool IsValidAmenity(Amenity amenity)
        => !string.IsNullOrWhiteSpace(amenity.AmenityName);

    private bool IsUnique(Amenity amenity)
        => !GetUndeletedAmenities()
        .Any(self => self.AmenityName.Equals(amenity.AmenityName) 
            && self.CategoryId == amenity.CategoryId);

    private IQueryable<Amenity> GetUndeletedAmenities()
        => _appDataContext.Amenities.Where(amenity => !amenity.IsDeleted).AsQueryable();
}