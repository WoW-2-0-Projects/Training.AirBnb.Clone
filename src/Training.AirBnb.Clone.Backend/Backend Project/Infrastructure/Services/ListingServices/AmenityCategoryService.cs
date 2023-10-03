using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ListingServices
{
    public class AmenityCategoryService : IEntityBaseService<AmenityCategory>
    {
        private readonly IDataContext _appDataContext;

        public AmenityCategoryService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<AmenityCategory> CreateAsync(AmenityCategory amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsValidCategoryName(amenityCategory.CategoryName))
                throw new EntityValidationException<AmenityCategory>("Invalid categoryName!");
            
            if (IsUniqueCategory(amenityCategory.CategoryName))
                throw new DuplicateEntityException<AmenityCategory>("Category already exists!");

            await _appDataContext.AmenityCategories.AddAsync(amenityCategory, cancellationToken);

            if (saveChanges)
                await _appDataContext.AmenityCategories.SaveChangesAsync(cancellationToken);

            return amenityCategory;
        }

        public ValueTask<ICollection<AmenityCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var amenityCategories = GetUndeletedAmentyCategories().
                Where(amenityCategory => ids.Contains(amenityCategory.Id));

            return new ValueTask<ICollection<AmenityCategory>>(amenityCategories.ToList());
        }

        public ValueTask<AmenityCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return new ValueTask<AmenityCategory>(GetUndeletedAmentyCategories().
                FirstOrDefault(amenityCategory => amenityCategory.Id == id) ??
                throw new EntityNotFoundException<AmenityCategory>("AmentyCategory not found!"));
        }

        public IQueryable<AmenityCategory> Get(Expression<Func<AmenityCategory, bool>> predicate)
        {
            return GetUndeletedAmentyCategories().Where(predicate.Compile()).AsQueryable();
        }

        public async ValueTask<AmenityCategory> UpdateAsync(AmenityCategory amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var updatedAmenityCategory = await GetByIdAsync(amenityCategory.Id);

            if (!IsValidCategoryName(amenityCategory.CategoryName))
                throw new EntityValidationException<AmenityCategory>("Invalid categoryName!");

            if (!IsUniqueCategory(amenityCategory.CategoryName))
                throw new DuplicateEntityException<AmenityCategory>("Category already exists!");

            updatedAmenityCategory.CategoryName = amenityCategory.CategoryName;
            updatedAmenityCategory.ModifiedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.AmenityCategories.SaveChangesAsync(cancellationToken);

            return updatedAmenityCategory;
        }

        public async ValueTask<AmenityCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedAmenityCategory = await GetByIdAsync(id);

            deletedAmenityCategory.IsDeleted = true;
            deletedAmenityCategory.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.AmenityCategories.SaveChangesAsync(cancellationToken);

            return deletedAmenityCategory;
        }

        public async ValueTask<AmenityCategory> DeleteAsync(AmenityCategory amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedAmenityCategory = await GetByIdAsync(amenityCategory.Id);

            deletedAmenityCategory.IsDeleted = true;
            deletedAmenityCategory.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.AmenityCategories.SaveChangesAsync(cancellationToken);

            return deletedAmenityCategory;
        }

        private bool IsValidCategoryName(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName) && categoryName.Length > 2)
                return true;
            else
                return false;
        }

        private bool IsUniqueCategory(string categoryName)
        {
            return _appDataContext.AmenityCategories.Any(category => category.CategoryName == categoryName);
        }

        private IQueryable<AmenityCategory> GetUndeletedAmentyCategories() => _appDataContext.AmenityCategories.
            Where(amenityCategory => !amenityCategory.IsDeleted).AsQueryable();
    }
}