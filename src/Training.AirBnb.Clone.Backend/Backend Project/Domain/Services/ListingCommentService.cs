using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.ListingReviewException;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services
{
    public class ListingReviewService : IEntityBaseService<ListingComment>
    {
        private readonly IDataContext _appDataContext;

        public ListingReviewService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<ListingComment> CreateAsync(ListingComment listingReview, bool saveChanges = true)
        {
            if (string.IsNullOrWhiteSpace(listingReview.Comment) || listingReview.Comment.Length < 1000)
                throw new ListingReviewFormatException("Invalid comment!");
            if (listingReview.Rating < 0 || listingReview.Rating > 5)
                throw new ListingReviewFormatException("Invalid rating!");
            
            await _appDataContext.ListingReviews.AddAsync(listingReview);
            
            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return listingReview;
        }

        public async ValueTask<ListingComment> DeleteAsync(Guid id, bool saveChanges = true)
        {
            var deletedListingReview = await GetById(id);

            if (deletedListingReview is null)
                throw new ListingReviewNotFoundException("ListingReview not found!");

            deletedListingReview.IsDeleted = true;
            deletedListingReview.DeletedDate = DateTime.UtcNow;
            
            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return deletedListingReview;
        }

        public async ValueTask<ListingComment> DeleteAsync(ListingComment listingReview, bool saveChanges = true)
        {
            var deletedListingReview = await GetById(listingReview.Id);

            if (deletedListingReview is null)
                throw new ListingReviewNotFoundException("ListingReview not found!");

            deletedListingReview.IsDeleted = true;
            deletedListingReview.DeletedDate= DateTime.UtcNow;

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return deletedListingReview;
        }

        public IQueryable<ListingComment> Get(Expression<Func<ListingComment, bool>> predicate)
        {
            return _appDataContext.ListingReviews.Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<ListingComment>> Get(IEnumerable<Guid> ids)
        {
            var listigReviews = _appDataContext.ListingReviews.
                Where(listingReview => ids.Contains(listingReview.Id));
            return new ValueTask<ICollection<ListingComment>>(listigReviews.ToList());
        }

        public ValueTask<ListingComment> GetById(Guid id)
        {
            return new ValueTask<ListingComment>(_appDataContext.ListingReviews.
                FirstOrDefault(listingReview => listingReview.Id == id) ??
                throw new ListingReviewNotFoundException("ListingReview not found!"));
        }

        public async ValueTask<ListingComment> UpdateAsync(ListingComment listingReview, bool saveChanges = true)
        {
            var updatedListingReview = await GetById(listingReview.Id);

            if (updatedListingReview is null)
                throw new ListingReviewNotFoundException("ListingReview not found!");
            if (string.IsNullOrWhiteSpace(listingReview.Comment) || listingReview.Comment.Length < 1000)
                throw new ListingReviewFormatException("Invalid comment!");
            if (listingReview.Rating < 0 || listingReview.Rating > 5)
                throw new ListingReviewFormatException("Invalid rating!");

            updatedListingReview.Comment = listingReview.Comment;
            updatedListingReview.WrittenBy = listingReview.WrittenBy;
            updatedListingReview.Rating = listingReview.Rating;
            updatedListingReview.ModifiedDate = DateTime.UtcNow;
            updatedListingReview.ListingId = listingReview.ListingId;
            
            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return updatedListingReview;
        }

        private IQueryable<ListingComment> GetUndeletedListingReview() =>_appDataContext.ListingReviews.
            Where(listingReview => !listingReview.IsDeleted).AsQueryable();
    }
}
