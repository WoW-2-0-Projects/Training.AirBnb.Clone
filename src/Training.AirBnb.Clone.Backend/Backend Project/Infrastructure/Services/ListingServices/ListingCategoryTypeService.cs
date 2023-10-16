using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class ListingCategoryTypeService : IEntityBaseService<ListingCategoryType>
{
    private readonly IDataContext _appDataContext;

    public ListingCategoryTypeService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<ListingCategoryType> CreateAsync(ListingCategoryType option, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsUnique(option))
            throw new DuplicateEntityException<ListingCategoryType>();

        await _appDataContext.ListingCategoryTypes.AddAsync(option, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return option;
    }

    public ValueTask<ICollection<ListingCategoryType>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<ListingCategoryType>>(GetUndeletedOptions()
            .Where(option => ids.Contains(option.Id))
            .ToList());

    public ValueTask<ListingCategoryType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<ListingCategoryType>(GetUndeletedOptions()
            .FirstOrDefault(option => option.Id == id)
            ?? throw new EntityNotFoundException<ListingCategoryType>());

    public IQueryable<ListingCategoryType> Get(Expression<Func<ListingCategoryType, bool>> predicate)
        => GetUndeletedOptions().Where(predicate.Compile()).AsQueryable();

    // Non Updatable entity
    public ValueTask<ListingCategoryType> UpdateAsync(ListingCategoryType option, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Non updatable entity");
    }

    public async ValueTask<ListingCategoryType> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundOption = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.ListingCategoryTypes.RemoveAsync(foundOption, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();
          
        return foundOption;
    }

    public async ValueTask<ListingCategoryType> DeleteAsync(ListingCategoryType option, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(option.Id, saveChanges, cancellationToken);

    private bool IsUnique(ListingCategoryType option)
        => !GetUndeletedOptions()
            .Any(self => self.ListingCategoryId == option.ListingCategoryId
                && self.ListingTypeId == option.ListingTypeId);

    private IQueryable<ListingCategoryType> GetUndeletedOptions()
        => _appDataContext.ListingCategoryTypes.Where(option => !option.IsDeleted).AsQueryable();
}