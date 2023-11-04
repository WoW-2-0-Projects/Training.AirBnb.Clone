using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Review.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices
{
    public class ListingRatingService : IListingRatingService
    {
        private readonly IDataContext _appDataContext;
        private readonly ReviewSettings _ratingSettings;

        public ListingRatingService(IDataContext appDataContext, IOptions<ReviewSettings> ratingSettings)
        {
            _appDataContext = appDataContext;
            _ratingSettings = ratingSettings.Value;
        }

        public async ValueTask<ListingRating> CreateAsync(ListingRating listingRating, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (IsUniqueListingRating(listingRating.ListingId))
                throw new DuplicateEntityException<ListingRating>("This listingRating already exists!");
            
            if (!IsValidRating(listingRating.Rating))
                throw new EntityValidationException<ListingRating>("Invalid rating!");

            await _appDataContext.ListingRatings.AddAsync(listingRating, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return listingRating;
        }

        public ValueTask<ICollection<ListingRating>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var listingRatings = GetUndeletedListingRatings().
                Where(listingRating => ids.Contains(listingRating.Id));
            return new ValueTask<ICollection<ListingRating>>(listingRatings.ToList());
        }

        public ValueTask<ListingRating> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return new ValueTask<ListingRating>(GetUndeletedListingRatings().
                FirstOrDefault(listingRating => listingRating.Id == id) ??
                throw new EntityNotFoundException<ListingRating>("ListingRating not found!"));
        }

        public IQueryable<ListingRating> Get(Expression<Func<ListingRating, bool>> predicate)
        {
            return GetUndeletedListingRatings().Where(predicate.Compile()).AsQueryable();
        }

        public async ValueTask<ListingRating> UpdateAsync(ListingRating listingRating, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var updatedListingRating = await GetByIdAsync(listingRating.Id, cancellationToken);

            if (!IsValidRating(listingRating.Rating))
                throw new EntityValidationException<ListingRating>("Invalid listingRating!");

            updatedListingRating.Rating = listingRating.Rating;
            
            await _appDataContext.ListingRatings.UpdateAsync(updatedListingRating, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return updatedListingRating;
        }

        public async ValueTask<ListingRating> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedListingRating = await GetByIdAsync(id, cancellationToken);

            await _appDataContext.ListingRatings.RemoveAsync(deletedListingRating, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return deletedListingRating;
        }

        public async ValueTask<ListingRating> DeleteAsync(ListingRating ListingRating, bool saveChanges = true, CancellationToken cancellationToken = default)
            => await DeleteAsync(ListingRating.Id, saveChanges, cancellationToken);

        private bool IsValidRating(double rating)
        {
            if (rating >= _ratingSettings.RatingMinValue && rating <= _ratingSettings.RatingMaxValue)
                return true;
            else
                return false;
        }

        private bool IsUniqueListingRating(Guid id)
        {
            return _appDataContext.ListingRatings.
                Any(Listingating => Listingating.ListingId == id);
        }
        private IQueryable<ListingRating> GetUndeletedListingRatings() => _appDataContext.ListingRatings.
            Where(listingRating => !listingRating.IsDeleted).AsQueryable();
    }
}