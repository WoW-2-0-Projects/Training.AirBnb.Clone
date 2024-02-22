using System.Linq.Expressions;
using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Settings;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AirBnB.Persistence.Repositories;

public class GuestFeedbackRepository(AppDbContext dbContext, ICacheBroker cacheBroker, 
    IOptions<GuestFeedbacksCacheSettings> feedbackCachingSettings, IMapper mapper) 
    : EntityRepositoryBase<GuestFeedback, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()),
        IGuestFeedbackRepository
{
    private readonly GuestFeedbacksCacheSettings _feedbackCachingSettings = feedbackCachingSettings.Value;
    
    public new IQueryable<GuestFeedback> Get(
        Expression<Func<GuestFeedback, bool>>? predicate = default,
        bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new async ValueTask<GuestFeedback?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public new async ValueTask<GuestFeedback> CreateAsync(
        GuestFeedback guestFeedback, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var createdFeedback = await base.CreateAsync(guestFeedback, saveChanges, cancellationToken);
        
        await CacheFeedback(mapper.Map<GuestFeedbackCacheDto>(guestFeedback), CacheKeyConstants.AddedGuestFeedbacks, cancellationToken);

        return createdFeedback;
    }

    public new async ValueTask<GuestFeedback?> DeleteAsync(
        GuestFeedback guestFeedback, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var deletedFeedback = await base.DeleteAsync(guestFeedback, saveChanges, cancellationToken) ??
                              throw new ArgumentException("Guest Feedback not found");
        
        await CacheFeedback(mapper.Map<GuestFeedbackCacheDto>(deletedFeedback), CacheKeyConstants.DeletedGuestFeedbacks, cancellationToken);

        return deletedFeedback;
    }

    public new async ValueTask<GuestFeedback?> DeleteByIdAsync(
        Guid id, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var deletedFeedback = await base.DeleteByIdAsync(id, saveChanges, cancellationToken) 
                              ?? throw new ArgumentException("Guest Feedback not found");
        
        await CacheFeedback(mapper.Map<GuestFeedbackCacheDto>(deletedFeedback), CacheKeyConstants.DeletedGuestFeedbacks, cancellationToken);

        return deletedFeedback;
    }
    
    private async ValueTask CacheFeedback(
        GuestFeedbackCacheDto guestFeedback, 
        string key, 
        CancellationToken cancellationToken = default)
    {
        if (await cacheBroker.TryGetAsync(key, out List<GuestFeedbackCacheDto>? cachedFeedbacks, cancellationToken))
            cachedFeedbacks!.Add(guestFeedback);
        else
            cachedFeedbacks = new List<GuestFeedbackCacheDto> { guestFeedback };

        await cacheBroker.SetAsync(key, cachedFeedbacks,
            new CacheEntryOptions(
                TimeSpan.FromHours(_feedbackCachingSettings.AbsoluteExpirationTimeInSeconds),
                TimeSpan.FromHours(_feedbackCachingSettings.SlidingExpirationTimeInSeconds)),
            cancellationToken);
    }
}