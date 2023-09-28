using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.ListingOccupancyExceptions;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services
{
    public class ListingOccupancyService : IEntityBaseService<ListingOccupancy>
    {
        private IDataContext _appDataContext;
        public ListingOccupancyService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }
        public async ValueTask<ListingOccupancy> CreateAsync(ListingOccupancy listingOccupancy, bool saveChanges = true)
        {
            if (!IsValidOccupancy(listingOccupancy))
                throw new ListingOccupancyValidationException("This Occupancy is not valid");
            else
                await _appDataContext.ListingOccupancies.AddAsync(listingOccupancy);
            if (saveChanges)
                await _appDataContext.ListingOccupancies.SaveChangesAsync();
            return listingOccupancy;
        }

        public async ValueTask<ListingOccupancy> DeleteAsync(Guid id, bool saveChanges = true)
        {
            var removedListingOccupancy = await GetByIdAsync(id);
            removedListingOccupancy.IsDeleted = true;
            removedListingOccupancy.DeletedDate = DateTimeOffset.UtcNow;
            if (saveChanges)
                await _appDataContext.ListingOccupancies.SaveChangesAsync();
            return removedListingOccupancy;
        }

        public async ValueTask<ListingOccupancy> DeleteAsync(ListingOccupancy listingOccupancy, bool saveChanges = true)
        {
            
            return await DeleteAsync(listingOccupancy.Id, saveChanges);
        }

        public IQueryable<ListingOccupancy> Get(Expression<Func<ListingOccupancy, bool>> predicate)
        {
            return GetUndelatedListingOccupancies().Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<ListingOccupancy>> GetAsync(IEnumerable<Guid> ids)
        {
            var listingOccupancy = GetUndelatedListingOccupancies()
                .Where(listingOccupanc => ids.Contains(listingOccupanc.Id));
            return new ValueTask<ICollection<ListingOccupancy>>(listingOccupancy.ToList());
        }

        public ValueTask<ListingOccupancy> GetByIdAsync(Guid id)
        {
            var listingOccupancy = GetUndelatedListingOccupancies()
                .FirstOrDefault(lsO => lsO.Id.Equals(id));
            if(listingOccupancy is null)
                throw new ListingOccupancyNotFoundException("Listing Occupation not found");
            return new ValueTask<ListingOccupancy>(listingOccupancy);
        }

        public async ValueTask<ListingOccupancy> UpdateAsync(ListingOccupancy listingOccupancy, bool saveChanges = true)
        {
            var foundListingOccupancy = await GetByIdAsync(listingOccupancy.Id);
            if (!IsValidOccupancy(foundListingOccupancy))
                throw new ListingOccupancyValidationException("This listingOccupation not valid");
            foundListingOccupancy.Adults = listingOccupancy.Adults;
            foundListingOccupancy.Children = listingOccupancy.Children;
            foundListingOccupancy.Infants = listingOccupancy.Infants;
            foundListingOccupancy.Pets = listingOccupancy.Pets;
            foundListingOccupancy.ModifiedDate = DateTime.UtcNow;
            if (saveChanges)
                await _appDataContext.ListingOccupancies.SaveChangesAsync();
            return foundListingOccupancy;

        }
        private bool IsValidOccupancy(ListingOccupancy listingOccupancy)
        {
            if(listingOccupancy.Adults < 0 && listingOccupancy.Adults > 50)
                return false;
            if(listingOccupancy.Children < 0 && listingOccupancy.Children > 50)
                return false;
            if(listingOccupancy.Infants < 0 && listingOccupancy.Infants > 50)
                return false;
            if(listingOccupancy.Pets < 0 && listingOccupancy.Pets > 50)
                return false;
            return true;
        }
        private IQueryable<ListingOccupancy> GetUndelatedListingOccupancies() => _appDataContext.ListingOccupancies
            .Where(lsO => !lsO.IsDeleted).AsQueryable();
    }
}
