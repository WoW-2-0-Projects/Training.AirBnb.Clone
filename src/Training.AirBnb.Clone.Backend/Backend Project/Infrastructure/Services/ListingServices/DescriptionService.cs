using Backend_Project.Application.Entity;
using Backend_Project.Application.Listings.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;
public class DescriptionService : IEntityBaseService<Description>
{
    private readonly IDataContext _dataContext;
    private readonly ListingSettings _descriptionSettings;

    public DescriptionService(IDataContext dataContext, IOptions<ListingSettings> descriptionSettings)
    {
        _dataContext = dataContext;
        _descriptionSettings = descriptionSettings.Value;
    }

    public async ValueTask<Description> CreateAsync(Description description, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!ValidateDescription(description))
            throw new EntityValidationException<Description>("This description is Invalid!!");

        await _dataContext.Descriptions.AddAsync(description, cancellationToken);

        if (saveChanges)
            await _dataContext.SaveChangesAsync();

        return description;
    }

    public IQueryable<Description> Get(Expression<Func<Description, bool>> predicate)
        => GetUndeletedListingDescription().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<Description>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
         => new ValueTask<ICollection<Description>>(GetUndeletedListingDescription()
             .Where(description => ids
                .Contains(description.Id))
             .ToList());

    public ValueTask<Description> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
         => new ValueTask<Description>(GetUndeletedListingDescription()
             .FirstOrDefault(description => description.Id.Equals(id))
             ?? throw new EntityNotFoundException<Description>("Description not found."));

    public async ValueTask<Description> UpdateAsync(Description description, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!ValidateDescription(description))
            throw new EntityNotUpdatableException<Description>("This Description is Not Valid!!");

        var foundListingDescription = await GetByIdAsync(description.Id);

        foundListingDescription.ListingDescription = description.ListingDescription;
        foundListingDescription.TheSpace = description.TheSpace;
        foundListingDescription.OtherDetails = description.OtherDetails;
        foundListingDescription.InteractionWithGuests = description.InteractionWithGuests;

        await _dataContext.Descriptions.UpdateAsync(foundListingDescription);

        if (saveChanges) await _dataContext.SaveChangesAsync();

        return foundListingDescription;
    }

    public async ValueTask<Description> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var removedListingDescription = await GetByIdAsync(id, cancellationToken);

        await _dataContext.Descriptions.RemoveAsync(removedListingDescription, cancellationToken);
        
        if (saveChanges) await _dataContext.SaveChangesAsync();
        
        return removedListingDescription;
    }

    public async ValueTask<Description> DeleteAsync(Description description, bool saveChanges = true, CancellationToken cancellationToken = default)
         => await DeleteAsync(description.Id, saveChanges, cancellationToken);

    private bool ValidateDescription(Description description)
    {
        if (description.ListingDescription.Length > _descriptionSettings.ListingDescriptionMaxLength || string
            .IsNullOrWhiteSpace(description.ListingDescription))
            return false;

        if (string.IsNullOrWhiteSpace(description.TheSpace))
            description.TheSpace = null;

        if (string.IsNullOrWhiteSpace(description.GuestAccess))
            description.GuestAccess = null;

        if (string.IsNullOrWhiteSpace(description.OtherDetails))
            description.OtherDetails = null;

        if (string.IsNullOrWhiteSpace(description.InteractionWithGuests))
            description.InteractionWithGuests = null;

        return true;
    }
    private IQueryable<Description> GetUndeletedListingDescription() => _dataContext
        .Descriptions.Where(res => !res.IsDeleted).AsQueryable();
}