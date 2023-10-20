using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class AvailabilityService : IEntityBaseService<Availability>
{
    private readonly IDataContext _appDataContext;

    public AvailabilityService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<Availability> CreateAsync(Availability availability, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateAvailability(availability);

        await _appDataContext.Availabilities.AddAsync(availability, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return availability;
    }

    public IQueryable<Availability> Get(Expression<Func<Availability, bool>> predicate)
        => GetUndeletedAvailability().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<Availability>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<Availability>>(GetUndeletedAvailability()
            .Where(availability => ids.Contains(availability.Id))
            .ToList());

    public ValueTask<Availability> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<Availability>(GetUndeletedAvailability()
            .FirstOrDefault(availability => availability.Id.Equals(id))
            ?? throw new EntityNotFoundException<Availability>("Availability not found!"));

    public async ValueTask<Availability> UpdateAsync(Availability availability, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateAvailability(availability);

        var findAvailability = await GetByIdAsync(availability.Id, cancellationToken);

        findAvailability.MinNights = availability.MinNights;
        findAvailability.MaxNights = availability.MaxNights;
        findAvailability.PreparationDays = availability.PreparationDays;
        findAvailability.AvailabilityWindow = availability.AvailabilityWindow;

        await _appDataContext.Availabilities.UpdateAsync(findAvailability, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return availability;
    }

    public async ValueTask<Availability> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var deletedAvailabilities = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.Availabilities.RemoveAsync(deletedAvailabilities, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return deletedAvailabilities;
    }

    public async ValueTask<Availability> DeleteAsync(Availability availability, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(availability.Id, saveChanges, cancellationToken);

    private void ValidateAvailability(Availability availability)
    {
        if (availability.MinNights < 1 || availability.MinNights > availability.MaxNights)
            throw new EntityValidationException<Availability>("Availability minNights is not valid!");

        if (availability.MaxNights > 730)
            throw new EntityValidationException<Availability>("Availability maxNights isn't valid!");

        if (availability.PreparationDays is not null && (availability.PreparationDays > 2
            || availability.PreparationDays < 0))
            throw new EntityValidationException<Availability>("Availability Propertiondays isn't valid!");

        if (availability.AvailabilityWindow < 3
            || availability.AvailabilityWindow > 24)
            throw new EntityValidationException<Availability>("Availability Window isn't valid!");
    }

    private IQueryable<Availability> GetUndeletedAvailability()
        => _appDataContext.Availabilities.Where(availability => !availability.IsDeleted).AsQueryable();
}