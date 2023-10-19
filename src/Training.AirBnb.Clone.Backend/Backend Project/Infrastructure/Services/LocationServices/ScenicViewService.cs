using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.LocationServices;

public class ScenicViewService : IEntityBaseService<ScenicView>
{
    private readonly IDataContext _appDataContext;
    public ScenicViewService(IDataContext dataContext)
    {
        _appDataContext = dataContext;
    }
    public async ValueTask<ScenicView> CreateAsync(ScenicView scenicView, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(scenicView);

        await _appDataContext.ScenicViews.AddAsync(scenicView, cancellationToken);

        if(saveChanges) await _appDataContext.SaveChangesAsync();

        return scenicView;
    }

    public IQueryable<ScenicView> Get(Expression<Func<ScenicView, bool>> predicate)
    => GetUndeletedScenicViews().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<ScenicView>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    => new ValueTask<ICollection<ScenicView>>(Get(scenicView => ids.Contains(scenicView.Id)).ToList());

    public ValueTask<ScenicView> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => new ValueTask<ScenicView>(Get(scenicView => scenicView.Id == id).FirstOrDefault() ?? throw new EntityNotFoundException<ScenicView>("Scenic view not found"));

    public async ValueTask<ScenicView> UpdateAsync(ScenicView scenicView, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(scenicView);

        var foundScenicView = await GetByIdAsync(scenicView.Id);

        foundScenicView.Name = scenicView.Name;
        await _appDataContext.ScenicViews.UpdateAsync(foundScenicView, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundScenicView;
    }

    public async ValueTask<ScenicView> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundScenicView = await GetByIdAsync(id);

        await _appDataContext.ScenicViews.RemoveAsync(foundScenicView, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return foundScenicView;
    }

    public ValueTask<ScenicView> DeleteAsync(ScenicView scenicView, bool saveChanges = true, CancellationToken cancellationToken = default)
    => DeleteAsync(scenicView.Id, saveChanges, cancellationToken);

    private void Validate(ScenicView scenicView)
    {
        if(string.IsNullOrWhiteSpace(scenicView.Name)) throw new EntityValidationException<Address>("Invalid scenic view!");
    }

    private IQueryable<ScenicView> GetUndeletedScenicViews()
        => _appDataContext.ScenicViews.Where(scenicView => !scenicView.IsDeleted).AsQueryable();
}
