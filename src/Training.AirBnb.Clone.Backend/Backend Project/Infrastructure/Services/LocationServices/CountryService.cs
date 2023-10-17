using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.LocationServices
{
    public class CountryService : IEntityBaseService<Country>
    {
        private IDataContext _appDataContext;
        public CountryService(IDataContext dataContext)
        {
            _appDataContext = dataContext;
        }

        public async ValueTask<Country> CreateAsync(Country country, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsUnique(country))
                throw new DuplicateEntityException<Country> ("This Country already Exists");

            if (!IsValidCountryName(country))
                throw new EntityValidationException<Country> ("The Country is in the wrong format");

            if (!IsValidCountryDailingCode(country) || !IsValidRegionPhoneNumberLength(country))
                throw new EntityValidationException<Country> ("This Country is in the wrong format");

            await _appDataContext.Countries.AddAsync(country, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();
            
            return country;
        }

        public async ValueTask<Country> UpdateAsync(Country country, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var foundCountry = await GetByIdAsync(country.Id);

            if (!IsValidCountryName(country))
                throw new EntityValidationException<Country> ("The Country is in the wrong format");

            if (!IsValidCountryDailingCode(country) || !IsValidRegionPhoneNumberLength(country))
                throw new EntityValidationException<Country> ("This Country is in the wrong format");

            foundCountry.Name = country.Name;
            foundCountry.RegionPhoneNumberLength = country.RegionPhoneNumberLength;
            foundCountry.CountryDialingCode = country.CountryDialingCode;

            await _appDataContext.Countries.UpdateAsync(country, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return foundCountry;
        }

        public IQueryable<Country> Get(Expression<Func<Country, bool>> predicate)
            => GetUndeletedCountries().Where(predicate.Compile()).AsQueryable();

        public ValueTask<ICollection<Country>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => new ValueTask<ICollection<Country>>(GetUndeletedCountries()
                .Where(country => ids
                .Contains(country.Id)).ToList());

        public ValueTask<Country> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => new ValueTask<Country> (GetUndeletedCountries()
                .FirstOrDefault(country => country.Id.Equals(id))
                ?? throw new EntityNotFoundException<Country> ("Reservation not found."));
            
        public async ValueTask<Country> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedCountry = await GetByIdAsync(id);

            await _appDataContext.Countries.RemoveAsync(deletedCountry, cancellationToken);

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();

            return deletedCountry;
        }

        public async ValueTask<Country> DeleteAsync(Country country, bool saveChanges = true, CancellationToken cancellationToken = default)
            => await DeleteAsync(country.Id, saveChanges, cancellationToken);
        
        private bool IsValidCountryDailingCode(Country country)
        {
            if (country.CountryDialingCode is null)
                return false;

            if ((country.CountryDialingCode[0].Equals("+")
                || country.CountryDialingCode.Length < 2
                || country.CountryDialingCode.Length > 5))
                return false;

            for (int letter = 1; letter < country.CountryDialingCode.Length; letter++)
                if (!char.IsNumber(country.CountryDialingCode[letter]))
                    return false;

            return true;
        }

        private bool IsValidRegionPhoneNumberLength(Country country)
            => country.RegionPhoneNumberLength < 7 && country.RegionPhoneNumberLength > 15
                ? false : true;

        private IQueryable<Country> GetUndeletedCountries() => _appDataContext.Countries
            .Where(country => !country.IsDeleted).AsQueryable();

        private bool IsValidCountryName(Country country)
            => string.IsNullOrWhiteSpace(country.Name)
                || country.Name.Length <= 4
                || country.Name.Length > 185 ? false : true;

        private bool IsUnique(Country country) => !GetUndeletedCountries().Any(countries => countries.Name.Equals(country.Name));
    }
}
