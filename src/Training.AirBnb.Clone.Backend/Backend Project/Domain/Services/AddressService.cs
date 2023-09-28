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

        public AddressService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<Address> CreateAsync(Address address, bool saveChanges = true)
        {
            if (!IsValidProvince(address.Province))
                throw new AddressFormatException("Invalid province!");
            if (!IsValidAddressLines(address))
                throw new AddressFormatException("Invalid addressLines!");
            if (!IsValidZipCode(address.ZipCode))
                throw new AddressFormatException("Invalid zipCode!");

            await _appDataContext.Addresses.AddAsync(address);

            if(saveChanges)
                await _appDataContext.Addresses.SaveChangesAsync();
            return address;
        }

        public async ValueTask<Address> DeleteAsync(Guid id, bool saveChanges = true)
        {
            var deletedAddress = await GetByIdAsync(id);

            if (deletedAddress is null)
                throw new AddressNotFoundException("Address not found!");

            deletedAddress.IsDeleted = true;
            deletedAddress.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.Addresses.SaveChangesAsync();
            return deletedAddress;
        }

        public async ValueTask<Address> DeleteAsync(Address address, bool saveChanges = true)
        {
            var deletedAddress = await GetByIdAsync(address.Id);

            if (deletedAddress is null)
                throw new AddressNotFoundException("Address not found!");

            deletedAddress.IsDeleted = true;
            deletedAddress.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.Addresses.SaveChangesAsync();
            return deletedAddress;
        }

        public IQueryable<Address> GetAsync(Expression<Func<Address, bool>> predicate)
        {
            return GetUndeletedAddresses().Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<Address>> GetAsync(IEnumerable<Guid> ids)
        {
            var addresses = GetUndeletedAddresses().
                Where(address => ids.Contains(address.Id));
            return new ValueTask<ICollection<Address>>(addresses.ToList());
        }

        public ValueTask<Address> GetByIdAsync(Guid id)
        {
            return new ValueTask<Address>(GetUndeletedAddresses().
                FirstOrDefault(address => address.Id == id) ??
                throw new AddressNotFoundException("Address not found"));
        }

        public async ValueTask<Address> UpdateAsync(Address address, bool saveChanges = true)
        {
            var updatedAddress = GetUndeletedAddresses().
                FirstOrDefault(updateAddress => updateAddress.Id.Equals(address.Id));

            if (updatedAddress is null)
                throw new AddressNotFoundException("Address not found!");

            if (!IsValidProvince(address.Province))
                throw new AddressFormatException("Invalid province!");
            if (!IsValidAddressLines(address))
                throw new AddressFormatException("Invalid addressLines!");
            if (!IsValidZipCode(address.ZipCode))
                throw new AddressFormatException("Invalid zipCode!");

            updatedAddress.CityId = address.CityId;
            updatedAddress.CountryId = address.CountryId;
            updatedAddress.Province = address.Province;
            updatedAddress.AddressLine1 = address.AddressLine1;
            updatedAddress.AddressLine2 = address.AddressLine2;
            updatedAddress.AddressLine3 = address.AddressLine3;
            updatedAddress.AddressLine4 = address.AddressLine4;
            updatedAddress.ZipCode = address.ZipCode;
            updatedAddress.ModifiedDate = DateTimeOffset.UtcNow;

            if(saveChanges)
                await _appDataContext.SaveChangesAsync();
            return updatedAddress;

        }

        private bool IsValidProvince(string addresses)
        {
            if (!string.IsNullOrWhiteSpace(addresses))
                return true;
            else
                return false;
        }

        private bool IsValidAddressLines(Address address)
        {
            var isValid = false;
            if (address.AddressLine1 is  null)
                isValid = true;
            if (address.AddressLine2 is null)
                isValid = true;
            if (address.AddressLine3 is null)
                isValid = true;
            if (address.AddressLine4 is null)
                isValid = true;
            return isValid;

           
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

        private IQueryable<Address> GetUndeletedAddresses() => _appDataContext.Addresses
            .Where(address => !address.IsDeleted).AsQueryable();
    }
}
