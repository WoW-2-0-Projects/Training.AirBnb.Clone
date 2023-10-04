using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices
{
    public class ListingCategoryService : IEntityBaseService<ListingCategory>
    {
        private readonly IDataContext _appDataContext;

        public ListingCategoryService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<ListingCategory> CreateAsync(ListingCategory listingCategory, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            if (!IsValidListingCategory(listingCategory))
                throw new EntityValidationException<ListingCategory> ("Listing Category is not valid");

            if (!IsUniqueListingCategoryName(listingCategory))
                throw new DuplicateEntityException<ListingCategory> ("This listing category already exists");

            await _appDataContext.ListingCategories.AddAsync(listingCategory, cancellationToken);

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();

            return listingCategory;
        }

        public async ValueTask<ListingCategory> UpdateAsync(ListingCategory listingCategory, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            if (!IsValidListingCategory(listingCategory)) 
                throw new EntityValidationException<ListingCategory> ("Listing Category is not valid");

            var foundListingCategory = await GetByIdAsync(listingCategory.Id);

            foundListingCategory.Name = listingCategory.Name;

            await _appDataContext.ListingCategories.UpdateAsync(foundListingCategory, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return foundListingCategory;
        }

        public IQueryable<ListingCategory> Get(Expression<Func<ListingCategory, bool>> predicate)
            => GetUndelatedListingCategories().Where(predicate.Compile()).AsQueryable();

        public ValueTask<ICollection<ListingCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => new ValueTask<ICollection<ListingCategory>> (GetUndelatedListingCategories()
                .Where(listingCategory => ids
                .Contains(listingCategory.Id)).ToList());

        public ValueTask<ListingCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => new ValueTask<ListingCategory> (GetUndelatedListingCategories()
                .FirstOrDefault(listingCategory => listingCategory.Id.Equals(id))
                ?? throw new EntityNotFoundException<ListingCategory> ("ListingCategory not found."));

        public async ValueTask<ListingCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var removedListingCategory = await GetByIdAsync(id, cancellationToken);

            await _appDataContext.ListingCategories.RemoveAsync(removedListingCategory, cancellationToken);
         
            if (saveChanges) await _appDataContext.SaveChangesAsync();

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
            => GetUndelatedListingCategories().Any(lc =>
            lc.Name.Equals(listingCategory.Name, StringComparison.OrdinalIgnoreCase)) ? false : true;
    }
}