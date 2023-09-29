using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services
{
    public class ListingCategoryService : IEntityBaseService<ListingCategory>
    {
        IDataContext _appDataContext;

        public ListingCategoryService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public ValueTask<ListingCategory> CreateAsync(ListingCategory listingCategory, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            if(!IsValidListingCategory(listingCategory))
                throw new NotImplementedException();

            if(!IsUniqueListingCategoryName(listingCategory))
                throw new NotImplementedException();

             _appDataContext.ListingCategories.AddAsync(listingCategory,cancellationToken);

            if (saveChanges)
                 _appDataContext.ListingCategories.SaveChangesAsync(cancellationToken);

            return new ValueTask<ListingCategory>(listingCategory);
        }

        public async ValueTask<ListingCategory> UpdateAsync(ListingCategory listingCategory, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            if (!IsValidListingCategory(listingCategory)) throw new NotImplementedException();


            var foundListingCategory = await GetByIdAsync(listingCategory.Id);

            foundListingCategory.Name = listingCategory.Name;
            foundListingCategory.ModifiedDate = DateTime.UtcNow;

            if (saveChanges) await _appDataContext.ListingCategories.SaveChangesAsync();
        
            return foundListingCategory;
        }

        public IQueryable<ListingCategory> Get(Expression<Func<ListingCategory, bool>> predicate) 
            => GetUndelatedListingCategories().Where(predicate.Compile()).AsQueryable();
       
        public ValueTask<ICollection<ListingCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var listingCategories = GetUndelatedListingCategories()
                .Where(lc => ids.Contains(lc.Id));

            return new ValueTask<ICollection<ListingCategory>>(listingCategories.ToList());
        }

        public ValueTask<ListingCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var listingCategories = GetUndelatedListingCategories().FirstOrDefault(lc => lc.Id == id);

            if (listingCategories is null)
                throw new ArgumentException();

            return new ValueTask<ListingCategory>(listingCategories);
        }

        public async ValueTask<ListingCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var removedListingCategory = await GetByIdAsync(id, cancellationToken);

            removedListingCategory.IsDeleted = true;
            removedListingCategory.DeletedDate = DateTime.UtcNow;

            if (saveChanges) await _appDataContext.ListingCategories.SaveChangesAsync();
            
            return removedListingCategory;
        }

        public async ValueTask<ListingCategory> DeleteAsync(ListingCategory entity, bool saveChanges = true,
            CancellationToken cancellationToken = default) => await DeleteAsync(entity.Id, saveChanges, cancellationToken);
       
        private bool IsValidListingCategory(ListingCategory listingCategory) 
            => string.IsNullOrWhiteSpace(listingCategory.Name) 
            || listingCategory.Name.Length < 2 ? false : true;
    
        private IQueryable<ListingCategory> GetUndelatedListingCategories() => _appDataContext.ListingCategories
            .Where(res => !res.IsDeleted).AsQueryable();

        private bool IsUniqueListingCategoryName(ListingCategory listingCategory)
            => GetUndelatedListingCategories().Any( lc => 
            lc.Name.Equals(listingCategory.Name,StringComparison.OrdinalIgnoreCase)) ? false: true;
    }
}