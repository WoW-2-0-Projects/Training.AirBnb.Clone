using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingFeatureOptionService : IEntityBaseService<ListingFeatureOption>
{
    private readonly IDataContext _appDataContext;

    public ListingFeatureOptionService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<ListingFeatureOption> CreateAsync(ListingFeatureOption option, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateOption(option);

        await _appDataContext.ListingFeatureOptions.AddAsync(option, cancellationToken);

        if (saveChanges) await _appDataContext.ListingFeatureOptions.SaveChangesAsync(cancellationToken);

        return option;
    }

    public ValueTask<ICollection<ListingFeatureOption>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingFeatureOption>>(GetUndeletedOptions()
        .Where(option => ids.Contains(option.Id))
        .ToList());

    public ValueTask<ListingFeatureOption> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingFeatureOption>(GetUndeletedOptions()
            .FirstOrDefault(option => option.Id == id)
            ?? throw new EntityNotFoundException<ListingFeatureOption>());

    public IQueryable<ListingFeatureOption> Get(Expression<Func<ListingFeatureOption, bool>> predicate)
        => GetUndeletedOptions().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<ListingFeatureOption> UpdateAsync(ListingFeatureOption option, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        ValidateOption(option);

        var foundOption = await GetByIdAsync(option.Id, cancellationToken);

        foundOption.Name = option.Name;
        foundOption.ModifiedDate = DateTime.UtcNow;

        if (saveChanges) await _appDataContext.ListingFeatureOptions.SaveChangesAsync(cancellationToken);

        return foundOption;
    }

    public async ValueTask<ListingFeatureOption> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundOption = await GetByIdAsync(id, cancellationToken);

        foundOption.IsDeleted = true;
        foundOption.DeletedDate = DateTime.UtcNow;

        if (saveChanges) await _appDataContext.ListingFeatureOptions.SaveChangesAsync(cancellationToken);

        return foundOption;
    }

    public async ValueTask<ListingFeatureOption> DeleteAsync(ListingFeatureOption option, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(option.Id, saveChanges, cancellationToken);

    private void ValidateOption(ListingFeatureOption option)
    {
        if (!IsValidOption(option))
            throw new EntityValidationException<ListingFeatureOption>();

        if (!IsUniqueOption(option))
            throw new DuplicateEntityException<ListingFeatureOption>();
    }

    private bool IsValidOption(ListingFeatureOption option)
        => !string.IsNullOrWhiteSpace(option.Name) && option.Name.Length > 2;

    private bool IsUniqueOption(ListingFeatureOption option)
        => !GetUndeletedOptions().Any(self => self.Name == option.Name);

    private IQueryable<ListingFeatureOption> GetUndeletedOptions()
        => _appDataContext.ListingFeatureOptions.Where(option => !option.IsDeleted).AsQueryable();
}