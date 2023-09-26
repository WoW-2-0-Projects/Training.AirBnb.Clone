using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.CountryExceptions;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services
{
    public class CountryService : IEntityBaseService<Country>
    {
        private IDataContext _appDataContext;
        public CountryService(IDataContext dataContext)
        {
            _appDataContext = dataContext;
        }

        public async ValueTask<Country> CreateAsync(Country country, bool saveChanges = true)
        {
            if (GetUndeletedCountries().Any(c => c.Equals(country)))
                throw new CountryAlreadyExistsException("This Country already Exists");
            if (!IsValidCityName(country))
                throw new CountryFormatException("The Country is in the wrong format");
            await _appDataContext.Countries.AddAsync(country);
            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return country;
        }

        public async ValueTask<Country> DeleteAsync(Guid id, bool saveChanges = true)
        {
            var deletedCountry = await GetByIdAsync(id);
            if (deletedCountry is null)
                throw new CountryNotFoundException("Country not found");
            deletedCountry.DeletedDate = DateTimeOffset.UtcNow;
            deletedCountry.IsDeleted = true;
            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return deletedCountry;
        }

        public async ValueTask<Country> DeleteAsync(Country country, bool saveChanges = true)
        {
            var deletedCountry = await GetByIdAsync(country.Id);
            if (deletedCountry is null)
                throw new CountryNotFoundException("this Country not found");
            deletedCountry.DeletedDate = DateTimeOffset.UtcNow;
            deletedCountry.IsDeleted = true;
            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return deletedCountry;
        }

        public IQueryable<Country> Get(Expression<Func<Country, bool>> predicate)
        {
            return GetUndeletedCountries().Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<Country>> Get(IEnumerable<Guid> ids)
        {
            var countries = GetUndeletedCountries()
            .Where(country => ids.Contains(country.Id));
            return new ValueTask<ICollection<Country>>(countries.ToList());
        }

        public ValueTask<Country> GetByIdAsync(Guid id)
        {
            var foundCountry = GetUndeletedCountries().FirstOrDefault(country => country.Id.Equals(id));
            if (foundCountry is null)
                throw new CountryNotFoundException("Country not found");
            return new ValueTask<Country>(foundCountry);
        }

        public async ValueTask<Country> UpdateAsync(Country country, bool saveChanges = true)
        {
            var foundCountry = await GetByIdAsync(country.Id);
            if (foundCountry is null)
                throw new CountryNotFoundException("Country not found");
            if (!IsValidCityName(country))
                throw new CountryFormatException("The Country is in the wrong format");
            foundCountry.ModifiedDate = DateTimeOffset.UtcNow;
            foundCountry.Name = country.Name;
            if (saveChanges)
                await _appDataContext.SaveChangesAsync();
            return foundCountry;
        }

        private IQueryable<Country> GetUndeletedCountries() => _appDataContext.Countries
            .Where(country => !country.IsDeleted).AsQueryable();
        private bool IsValidCityName(Country country)
        {
            if (string.IsNullOrWhiteSpace(country.Name)
                || (country.Name.Length <= 4
                || country.Name.Length > 185))
                return false;
            return true;
        }
    }
}
