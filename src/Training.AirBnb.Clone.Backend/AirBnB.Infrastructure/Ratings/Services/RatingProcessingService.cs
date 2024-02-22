using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Ratings.Services;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Ratings.Services;

public class RatingProcessingService(ICacheBroker cacheBroker, AppDbContext appDbContext) 
    : IRatingProcessingService
{
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1); 
    
    public async ValueTask ProcessListingsRatings()
    {
        await Semaphore.WaitAsync();
        
        await ProcessCachedFeedbacks(CacheKeyConstants.DeletedGuestFeedbacks, UpdateDeletedRatings);
        await ProcessCachedFeedbacks(CacheKeyConstants.AddedGuestFeedbacks, UpdateRatings);
        
        Semaphore.Release();
    }

    private async ValueTask ProcessCachedFeedbacks(string cacheKey,
        Func<(Guid ListingId, int FeedbacksCount, Rating RatingSum), ValueTask> updateMethod)
    {
        var cachedFeedbacks = await GetCachedFeedbacks(cacheKey) ?? [];
        var groupedFeedbacks = GroupRatingsByListingId(cachedFeedbacks);

        foreach (var feedback in groupedFeedbacks)
            await updateMethod(feedback);
    }
    
    private async ValueTask<List<GuestFeedbackCacheDto>?> GetCachedFeedbacks(string key)
    {
        var feedbacks = await cacheBroker.GetAsync<List<GuestFeedbackCacheDto>>(key);
        await cacheBroker.SetAsync<List<GuestFeedbackCacheDto>>(key, []);

        return feedbacks;
    }

    private IList<(Guid ListingId, int FeedbacksCount, Rating RatingSum)> GroupRatingsByListingId(IList<GuestFeedbackCacheDto> guestFeedbacks)
    {
         return guestFeedbacks.GroupBy(feedback => feedback.ListingId)
            .Select(group => (
                ListingId: group.Key,
                FeedbacksCount: group.Count(),
                AverageRating: new Rating
                {
                    Accuracy = group.Sum(feedback => feedback.Rating.Accuracy),
                    CheckIn = group.Sum(feedback => feedback.Rating.CheckIn),
                    Cleanliness = group.Sum(feedback => feedback.Rating.Cleanliness),
                    Communication = group.Sum(feedback => feedback.Rating.Communication),
                    Location = group.Sum(feedback => feedback.Rating.Location),
                    Value = group.Sum(feedback => feedback.Rating.Value),
                    OverallRating = group.Sum(feedback => feedback.Rating.OverallRating)
                }))
            .ToList();
    }

    private async ValueTask UpdateRatings((Guid ListingId, int FeedbacksCount, Rating RatingSum) newFeedbacks)
    {
        var (listingId, feedbacksCount, ratingSum) = newFeedbacks;

        var allFeedbacksCount =
            appDbContext.GuestFeedbacks.Count(feedback => feedback.ListingId == listingId && !feedback.IsDeleted);

        var existingRatingsCount = allFeedbacksCount - feedbacksCount;
        
        await appDbContext.Listings.Where(listing => listing.Id == listingId)
            .Select(listing => listing.Rating)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(rating => rating.OverallRating, rating =>
                    (rating.OverallRating * existingRatingsCount + ratingSum.OverallRating) / allFeedbacksCount)
                .SetProperty(rating => rating.Cleanliness, rating => 
                    (rating.Cleanliness * existingRatingsCount + ratingSum.Cleanliness) / allFeedbacksCount)
                .SetProperty(rating => rating.Communication, rating => 
                    (rating.Communication * existingRatingsCount + ratingSum.Communication) / allFeedbacksCount)
                .SetProperty(rating => rating.Accuracy, rating => 
                    (rating.Accuracy * existingRatingsCount + ratingSum.Accuracy) / allFeedbacksCount)
                .SetProperty(rating => rating.CheckIn, rating => 
                    (rating.CheckIn * existingRatingsCount + ratingSum.CheckIn) / allFeedbacksCount)
                .SetProperty(rating => rating.Location, rating => 
                    (rating.Location * existingRatingsCount + ratingSum.Location) / allFeedbacksCount)
                .SetProperty(rating => rating.Value, rating => 
                    (rating.Value * existingRatingsCount + ratingSum.Value) / allFeedbacksCount));
    }

    private async ValueTask UpdateDeletedRatings((Guid ListingId, int FeedbacksCount, Rating RatingSum) deletedFeedbacks)
    {
        var (listingId, feedbacksCount, ratingSum) = deletedFeedbacks;

        var existingRatingsCount =
            appDbContext.GuestFeedbacks.Count(feedback => feedback.ListingId == listingId && !feedback.IsDeleted);

        var allFeedbacksCount = existingRatingsCount + feedbacksCount;

        if (existingRatingsCount == 0) existingRatingsCount++;
        
        await appDbContext.Listings.Where(listing => listing.Id == listingId)
            .Select(listing => listing.Rating)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(rating => rating.OverallRating, rating =>
                    (rating.OverallRating * allFeedbacksCount - ratingSum.OverallRating) / existingRatingsCount)
                .SetProperty(rating => rating.Cleanliness, rating => 
                    (rating.Cleanliness * allFeedbacksCount - ratingSum.Cleanliness) / existingRatingsCount)
                .SetProperty(rating => rating.Communication, rating => 
                    (rating.Communication * allFeedbacksCount - ratingSum.Communication) / existingRatingsCount)
                .SetProperty(rating => rating.Accuracy, rating => 
                    (rating.Accuracy * allFeedbacksCount - ratingSum.Accuracy) / existingRatingsCount)
                .SetProperty(rating => rating.CheckIn, rating => 
                    (rating.CheckIn * allFeedbacksCount - ratingSum.CheckIn) / existingRatingsCount)
                .SetProperty(rating => rating.Location, rating => 
                    (rating.Location * allFeedbacksCount - ratingSum.Location) / existingRatingsCount)
                .SetProperty(rating => rating.Value, rating => 
                    (rating.Value * allFeedbacksCount - ratingSum.Value) / existingRatingsCount));
    }
}