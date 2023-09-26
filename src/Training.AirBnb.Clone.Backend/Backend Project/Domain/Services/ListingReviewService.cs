using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services
{
    internal class ListingReviewService : IEntityBaseService<ListingReview>
    {
        private readonly IDataContext _appDataContext;

        public ListingReviewService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public ValueTask<ListingReview> CreateAsync(ListingReview entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingReview> DeleteAsync(Guid id, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingReview> DeleteAsync(ListingReview entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ListingReview> Get(Expression<Func<ListingReview, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICollection<ListingReview>> Get(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingReview> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingReview> UpdateAsync(ListingReview entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
