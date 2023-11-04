using Backend_Project.Application.Foundations.ReviewServices;
using Backend_Project.Application.Review.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ReviewServices;

public class RatingService : IRatingService
{
    private readonly IDataContext _appDataContext;
    private readonly ReviewSettings _ratingSettings;

    public RatingService(IDataContext appDataContext, IOptions<ReviewSettings> ratingSettings)
    {
        _appDataContext = appDataContext;
        _ratingSettings = ratingSettings.Value;
    }

    public async ValueTask<Rating> CreateAsync(Rating rating, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!IsValidRating(rating))
            throw new EntityValidationException<Rating>("Invalid rating");

        await _appDataContext.Ratings.AddAsync(rating, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return rating;
    }

    public IQueryable<Rating> Get(Expression<Func<Rating, bool>> predicate) =>
        GetUndeletedRatings().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<Rating>> GetAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default) =>
        new (GetUndeletedRatings()
            .Where(rating => ids.Contains(rating.Id)).ToList());


    public ValueTask<Rating> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        new (GetUndeletedRatings().FirstOrDefault(rating => rating.Id == id) ??
            throw new EntityNotFoundException<Rating>("Rating not found"));

    public async ValueTask<Rating> UpdateAsync(Rating rating, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var updatingRating = await GetByIdAsync(rating.Id, cancellationToken);

        if (!IsValidRating(rating))
            throw new EntityValidationException<Rating>("Rating is not valid");

        updatingRating.Mark = rating.Mark;
        await _appDataContext.Ratings.UpdateAsync(rating, cancellationToken);
       
        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return updatingRating;
    }

    public async ValueTask<Rating> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var deletingRating = await GetByIdAsync(id,cancellationToken);

        await _appDataContext.Ratings.RemoveAsync(deletingRating, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return deletingRating;
    }

    public async ValueTask<Rating> DeleteAsync(Rating rating, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(rating.Id, saveChanges, cancellationToken);
    
    private bool IsValidRating(Rating rating) =>
        rating.Mark >= _ratingSettings.RatingMinValue && rating.Mark <= _ratingSettings.RatingMaxValue;

    private IQueryable<Rating> GetUndeletedRatings() =>
        _appDataContext.Ratings.Where(rating => !rating.IsDeleted).AsQueryable();
}