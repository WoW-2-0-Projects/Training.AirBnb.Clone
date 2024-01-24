using AirBnB.Application.Ratings.Services;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Ratings.Services;

public class RatingRecalculationService(ICacheBroker cacheBroker, AppDbContext appDbContext) 
    : IRatingRecalculationService
{
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1); 
    
    public async ValueTask RecalculateListingsRatings()
    {
        await Semaphore.WaitAsync();
       
        var deletedFeedbacks = await GetCachedFeedbacks(CacheKeyConstants.DeletedGuestFeedbacks)
            is List<GuestFeedback> removedFeedbacks
            ? GroupRatingsByListingId(removedFeedbacks)
            : new List<(Guid ListingId, int FeedbacksCount, Rating RatingSum)>();

        foreach (var deletedFeedback in deletedFeedbacks)
            UpdateDeletedRatings(deletedFeedback);

        var addedFeedbacks = await GetCachedFeedbacks(CacheKeyConstants.AddedGuestFeedbacks)
            is List<GuestFeedback> newFeedbacks
            ? GroupRatingsByListingId(newFeedbacks)
            : new List<(Guid ListingId, int FeedbacksCount, Rating RatingSum)>();

        foreach (var addedFeedback in addedFeedbacks)
            UpdateRatings(addedFeedback);
        
        Semaphore.Release();
    }

    public async ValueTask<List<GuestFeedback>?> GetCachedFeedbacks(string key)
    {
        var feedbacks = await cacheBroker.GetAsync<List<GuestFeedback>>(key);
        await cacheBroker.SetAsync<List<GuestFeedback>>(key, []);

        return feedbacks;
    }

    public IList<(Guid ListingId, int FeedbacksCount, Rating RatingSum)> GroupRatingsByListingId(IList<GuestFeedback> guestFeedbacks)
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

    public void UpdateRatings((Guid ListingId, int FeedbacksCount, Rating RatingSum) newFeedbacks)
    {
        var (listingId, feedbacksCount, ratingSum) = newFeedbacks;

        var allFeedbacksCount =
            appDbContext.GuestFeedbacks.Count(feedback => feedback.ListingId == listingId && !feedback.IsDeleted);

        var existingRatingsCount = allFeedbacksCount - feedbacksCount;
        
        appDbContext.Listings.Where(listing => listing.Id == listingId)
            .Select(listing => listing.Rating)
            .ExecuteUpdate(setters => setters
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

    public void UpdateDeletedRatings((Guid ListingId, int FeedbacksCount, Rating RatingSum) deletedFeedbacks)
    {
        var (listingId, feedbacksCount, ratingSum) = deletedFeedbacks;

        var existingRatingsCount =
            appDbContext.GuestFeedbacks.Count(feedback => feedback.ListingId == listingId && !feedback.IsDeleted);

        var allFeedbacksCount = existingRatingsCount + feedbacksCount;

        if (existingRatingsCount == 0) existingRatingsCount++;
        
        appDbContext.Listings.Where(listing => listing.Id == listingId)
            .Select(listing => listing.Rating)
            .ExecuteUpdate(setters => setters
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