using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices;

public class BlockedNightService : IEntityBaseService<BlockedNight>
{
    private readonly IDataContext _dataContext;
    public BlockedNightService(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async ValueTask<BlockedNight> CreateAsync(BlockedNight blockedNight, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidBlockedNights(blockedNight))
            throw new EntityValidationException<BlockedNight>("This Blocked Night is not Valid!!");

        await _dataContext.BlockedNights.AddAsync(blockedNight, cancellationToken);

        if(saveChanges)
            await _dataContext.SaveChangesAsync();

        return blockedNight;
    }


    public IQueryable<BlockedNight> Get(Expression<Func<BlockedNight, bool>> predicate)
        => GetUndeletedListingBlockedNight().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<BlockedNight>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        => new ValueTask<ICollection<BlockedNight>>(GetUndeletedListingBlockedNight()
            .Where(blockedNights => ids
            .Contains(blockedNights.Id))
            .ToList());

    public ValueTask<BlockedNight> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => new ValueTask<BlockedNight>(GetUndeletedListingBlockedNight()
            .FirstOrDefault(blockedNights => blockedNights.Id.Equals(id))
            ?? throw new EntityNotFoundException<BlockedNight>("Blocked Night not Found"));

    public async ValueTask<BlockedNight> UpdateAsync(BlockedNight blockedNight, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if(!IsValidBlockedNights(blockedNight))
            throw new EntityNotUpdatableException<BlockedNight>("This Blocked Night is Not Valid!");

        var foundListingBlockedNight = await GetByIdAsync(blockedNight.Id);

        foundListingBlockedNight.Date = blockedNight.Date;
        foundListingBlockedNight.IsCustomBlock = blockedNight.IsCustomBlock;

        await _dataContext.BlockedNights.UpdateAsync(foundListingBlockedNight);

        if(saveChanges)
            await _dataContext.SaveChangesAsync();  

        return foundListingBlockedNight;
    }
    public async ValueTask<BlockedNight> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var removedListingBlockedNight = await GetByIdAsync(id, cancellationToken);

        await _dataContext.BlockedNights.RemoveAsync(removedListingBlockedNight, cancellationToken);

        if(saveChanges) await _dataContext.SaveChangesAsync();  

        return removedListingBlockedNight;
    }

    public async ValueTask<BlockedNight> DeleteAsync(BlockedNight blockedNight, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(blockedNight.Id, saveChanges, cancellationToken);
    
    private bool IsValidBlockedNights(BlockedNight blockedNight)
    {
        if(blockedNight.Date < DateOnly.FromDateTime(DateTime.Today))
            return false;

        if(_dataContext.BlockedNights.Any(blocked => 
            blocked.Date == blockedNight.Date 
            && blocked.ListingId == blockedNight.ListingId))
            return false;

        return true;
    }

    private IQueryable<BlockedNight> GetUndeletedListingBlockedNight() => _dataContext
        .BlockedNights.Where(res => !res.IsDeleted).AsQueryable();
}