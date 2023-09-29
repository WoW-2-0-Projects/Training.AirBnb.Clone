using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.AddressExceptions;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services
{
    public class AddressService : IEntityBaseService<Address>
    {
        private IDataContext _appDataContext;
        private IEntityBaseService<City> _cityService;
        private IEntityBaseService<Country> _countryService;

        public AddressService(IDataContext appDataContext, IEntityBaseService<City> cityService, IEntityBaseService<Country> countryService)
        {
            _appDataContext = appDataContext;
            _cityService = cityService;
            _countryService = countryService;
        }
        
        public async ValueTask<Address> CreateAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsValidAddressLines(address.AddressLine1))
                throw new AddressFormatException("Invalid province!");
            if (!IsValidZipCode(address.ZipCode))
                throw new AddressFormatException("Invalid zipCode!");
            if (!(await IsCityWithInCountry(address)))
                throw new CityDoesNotMatchCountryException("City does not match country!");

            await _appDataContext.Addresses.AddAsync(address, cancellationToken);

            if(saveChanges)
                await _appDataContext.Addresses.SaveChangesAsync(cancellationToken);
            
            return address;
        }

        public async ValueTask<Address> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedAddress = await GetByIdAsync(id);

            deletedAddress.IsDeleted = true;
            deletedAddress.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.Addresses.SaveChangesAsync(cancellationToken);
            
            return deletedAddress;
        }

        public async ValueTask<Address> DeleteAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedAddress = await GetByIdAsync(address.Id);

            deletedAddress.IsDeleted = true;
            deletedAddress.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.Addresses.SaveChangesAsync(cancellationToken);

            return deletedAddress;
        }

        public IQueryable<Address> Get(Expression<Func<Address, bool>> predicate)
        {
            return GetUndeletedAddresses().Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<Address>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var addresses = GetUndeletedAddresses().
                Where(address => ids.Contains(address.Id));
            return new ValueTask<ICollection<Address>>(addresses.ToList());
        }

        public ValueTask<Address> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return new ValueTask<Address>(GetUndeletedAddresses().
                FirstOrDefault(address => address.Id == id) ??
                throw new AddressNotFoundException("Address not found"));
        }

        public async ValueTask<Address> UpdateAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var updatedAddress = await GetByIdAsync(address.Id);

            if (!IsValidAddressLines(address.AddressLine1))
                throw new AddressFormatException("Invalid province!");
            if (!IsValidZipCode(address.ZipCode))
                throw new AddressFormatException("Invalid zipCode!");
            if (!(await IsCityWithInCountry(address)))
                throw new CityDoesNotMatchCountryException("City does not match country!");

            updatedAddress.CityId = address.CityId;
            updatedAddress.CountryId = address.CountryId;
            updatedAddress.AddressLine1 = address.AddressLine1;
            updatedAddress.AddressLine2 = address.AddressLine2;
            updatedAddress.AddressLine3 = address.AddressLine3;
            updatedAddress.AddressLine4 = address.AddressLine4;
            updatedAddress.Province = address.Province;
            updatedAddress.ZipCode = address.ZipCode;
            updatedAddress.ModifiedDate = DateTimeOffset.UtcNow;

            if(saveChanges)
                await _appDataContext.Addresses.SaveChangesAsync(cancellationToken);
            
            return updatedAddress;

        }

        private bool IsValidAddressLines(string addressLine)
        {
            if (!string.IsNullOrWhiteSpace(addressLine))
                return true;
            else
                return false;
        }

        private bool IsValidZipCode(string? zipCode)
        {
            if(zipCode is null)
                return true;
            for (int index = 0; index < zipCode?.Length; index++)
                if (!char.IsNumber(zipCode[index]))
                    return false;
            return true;
        }

        private async ValueTask<bool> IsCityWithInCountry(Address address)
        {
            var country = await _countryService.GetByIdAsync(address.CountryId);
            var city = await _cityService.GetByIdAsync(address.CityId);
            if (country.Id == city.CountryId)
                return true;
            else
                return false;
        }

        private IQueryable<Address> GetUndeletedAddresses() => _appDataContext.Addresses
            .Where(address => !address.IsDeleted).AsQueryable();
    }
}
