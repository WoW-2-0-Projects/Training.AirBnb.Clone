using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.ListingRatingException;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services
{
    public class ListingRatingService : IEntityBaseService<ListingRating>
    {
        private readonly IDataContext _appDataContext;
        public ListingRatingService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<ListingRating> CreateAsync(ListingRating listingRating, bool saveChanges = true)
        {
            if (IsUniqueListingRating(listingRating.ListingId))
                throw new ListingRatingAlreadyExistsException("This listingRating already exists!");
            if (!IsValidRating(listingRating.Rating))
                throw new ListingRatingFormatException("Invalid rating!");

            await _appDataContext.ListingRatings.AddAsync(listingRating);

            if(saveChanges)
                await _appDataContext.ListingRatings.SaveChangesAsync();
            
            return listingRating;
               
        }

        public async ValueTask<ListingRating> DeleteAsync(Guid id, bool saveChanges = true)
        {
            var deletedListingRating = await GetByIdAsync(id);
            
            if (deletedListingRating is null)
                throw new ListingRatingNotFoundException("ListingRating not found!");

            deletedListingRating.IsDeleted = true;
            deletedListingRating.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.ListingRatings.SaveChangesAsync();
            
            return deletedListingRating;
        }

        public async ValueTask<ListingRating> DeleteAsync(ListingRating ListingRating, bool saveChanges = true)
        {
            var deletedListingRating = await GetByIdAsync(ListingRating.Id);

            if (deletedListingRating is null)
                throw new ListingRatingNotFoundException("ListingRating not found!");

            deletedListingRating.IsDeleted = true;
            deletedListingRating.DeletedDate= DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.ListingRatings.SaveChangesAsync();

            return deletedListingRating;
        }

        public IQueryable<ListingRating> Get(Expression<Func<ListingRating, bool>> predicate)
        {
            return GetUndeletedListingRatings().Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<ListingRating>> GetAsync(IEnumerable<Guid> ids)
        {
            var listingRatings = GetUndeletedListingRatings().
                Where(listingRating => ids.Contains(listingRating.Id));
            return new ValueTask<ICollection<ListingRating>>(listingRatings.ToList());
        }

        public ValueTask<ListingRating> GetByIdAsync(Guid id)
        {
            return new ValueTask<ListingRating>(GetUndeletedListingRatings().
                FirstOrDefault(listingRating => listingRating.Id == id) ??
                throw new ListingRatingNotFoundException("ListingRating not found!"));
        }

        public async ValueTask<ListingRating> UpdateAsync(ListingRating listingRating, bool saveChanges = true)
        {
            var updatedListingRating = await GetByIdAsync(listingRating.Id);

            if(updatedListingRating is null)
                throw new ListingRatingNotFoundException("ListingRating not found!");
            if (!IsValidRating(listingRating.Rating))
                throw new ListingRatingFormatException("Invalid listingRating!");

            updatedListingRating.Rating = listingRating.Rating;
            updatedListingRating.ModifiedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.ListingRatings.SaveChangesAsync();

            return updatedListingRating;
        }

        private bool IsValidRating(double rating)
        {
            if (rating >= 0 && rating <= 5.0)
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
