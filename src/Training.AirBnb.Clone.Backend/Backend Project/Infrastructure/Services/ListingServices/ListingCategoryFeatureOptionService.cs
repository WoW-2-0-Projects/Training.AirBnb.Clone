using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingCategoryFeatureOptionService : IEntityBaseService<ListingCategoryFeatureOption>
{
    private readonly IDataContext _appDataContext;

    public ListingCategoryFeatureOptionService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<ListingCategoryFeatureOption> CreateAsync(ListingCategoryFeatureOption option, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValid(option))
            throw new EntityValidationException<ListingCategoryFeatureOption>();

        if (!IsUnique(option))
            throw new DuplicateEntityException<ListingCategoryFeatureOption>();

        await _appDataContext.ListingCategoryFeatureOptions.AddAsync(option, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return option;
    }

    public ValueTask<ICollection<ListingCategoryFeatureOption>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingCategoryFeatureOption>>(GetUndeletedOptions()
            .Where(option => ids.Contains(option.Id))
            .ToList());

    public ValueTask<ListingCategoryFeatureOption> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingCategoryFeatureOption>(GetUndeletedOptions()
            .FirstOrDefault(option => option.Id == id)
            ?? throw new EntityNotFoundException<ListingCategoryFeatureOption>());

    public IQueryable<ListingCategoryFeatureOption> Get(Expression<Func<ListingCategoryFeatureOption, bool>> predicate)
        => GetUndeletedOptions().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<ListingCategoryFeatureOption> UpdateAsync(ListingCategoryFeatureOption option, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValid(option))
            throw new EntityValidationException<ListingCategoryFeatureOption>();

        var foundOption = await GetByIdAsync(option.Id, cancellationToken);

        foundOption.FeatureMinValue = option.FeatureMinValue;
        foundOption.FeatureMaxValue = option.FeatureMaxValue;

        await _appDataContext.ListingCategoryFeatureOptions.UpdateAsync(foundOption, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundOption;
    }

    public async ValueTask<ListingCategoryFeatureOption> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundOption = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.ListingCategoryFeatureOptions.RemoveAsync(foundOption, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundOption;
    }

    public async ValueTask<ListingCategoryFeatureOption> DeleteAsync(ListingCategoryFeatureOption option, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(option.Id, saveChanges, cancellationToken);

    private bool IsValid(ListingCategoryFeatureOption option)
        => option.FeatureMinValue >= 0 && option.FeatureMaxValue <= 50;

    private bool IsUnique(ListingCategoryFeatureOption option)
        => !GetUndeletedOptions()
            .Any(self => self.ListingCategoryId == option.ListingCategoryId
                && self.ListingFeatureOptionId == option.ListingFeatureOptionId
                && self.ListingFeatureId == option.ListingFeatureId);

    private IQueryable<ListingCategoryFeatureOption> GetUndeletedOptions()
        => _appDataContext.ListingCategoryFeatureOptions.Where(option => !option.IsDeleted).AsQueryable();
}