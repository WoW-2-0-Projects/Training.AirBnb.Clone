using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Listings.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingRegistrationProgressService : IListingRegistrationProgressService
{
    private readonly IDataContext _appFileContext;
    private readonly ListingRegistrationProgressSettings _registrationSettings;

    public ListingRegistrationProgressService(IDataContext context, IOptions<ListingRegistrationProgressSettings> registrationSettings)
    {
        _appFileContext = context;
        _registrationSettings = registrationSettings.Value;
    }

    public async ValueTask<ListingRegistrationProgress> CreateAsync(ListingRegistrationProgress progress, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidProgress(progress))
            throw new EntityValidationException<ListingRegistrationProgress>("Invalid progress record!");

        if (!IsUnique(progress))
            throw new DuplicateEntityException<ListingRegistrationProgress>("Listing registration progress already exists!");

        await _appFileContext.ListingRegistrationProgresses.AddAsync(progress, cancellationToken);

        if (saveChanges) await _appFileContext.SaveChangesAsync();

        return progress;
    }

    public IQueryable<ListingRegistrationProgress> Get(Expression<Func<ListingRegistrationProgress, bool>> predicate)
        => GetUndeletedProgresses().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<ListingRegistrationProgress>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new(Get(progress => ids.Contains(progress.Id)).ToList());

    public ValueTask<ListingRegistrationProgress> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new(GetUndeletedProgresses().FirstOrDefault(self => self.Id == id) ?? throw new EntityNotFoundException<ListingRegistrationProgress>("Listing registration progress not found!"));

    public async ValueTask<ListingRegistrationProgress> UpdateAsync(ListingRegistrationProgress progress, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundProgress = await GetByIdAsync(progress.Id, cancellationToken);

        if (!IsValidOnUpdate(foundProgress, progress))
            throw new EntityValidationException<ListingRegistrationProgress>("Invalid listing registration progress!");

        foundProgress.Progress = progress.Progress;

        await _appFileContext.ListingRegistrationProgresses.UpdateAsync(foundProgress, cancellationToken);

        if (saveChanges) await _appFileContext.SaveChangesAsync();

        return foundProgress;
    }

    public async ValueTask<ListingRegistrationProgress> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundProgress = await GetByIdAsync(id, cancellationToken);

        await _appFileContext.ListingRegistrationProgresses.RemoveAsync(foundProgress, cancellationToken);

        if (saveChanges) await _appFileContext.SaveChangesAsync();

        return foundProgress;
    }

    public async ValueTask<ListingRegistrationProgress> DeleteAsync(ListingRegistrationProgress progress, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(progress.Id, saveChanges, cancellationToken);

    private bool IsValidProgress(ListingRegistrationProgress progress)
        => progress.Progress >= _registrationSettings.ProgressMinValue && progress.Progress <= _registrationSettings.ProgressMaxValue;

    private bool IsUnique(ListingRegistrationProgress progress)
        => GetUndeletedProgresses().Any(self => self.ListingId == progress.ListingId);

    private bool IsValidOnUpdate(ListingRegistrationProgress oldProgress, ListingRegistrationProgress updatedProgress)
        => oldProgress.Progress <= updatedProgress.Progress && updatedProgress.Progress <= _registrationSettings.ProgressMaxValue;

    private IEnumerable<ListingRegistrationProgress> GetUndeletedProgresses()
        => _appFileContext.ListingRegistrationProgresses.Where(self => !self.IsDeleted);
}