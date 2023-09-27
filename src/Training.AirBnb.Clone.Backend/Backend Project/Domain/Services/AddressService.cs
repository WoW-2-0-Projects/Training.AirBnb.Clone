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
            if (!(await IsValidAddresses(address.Province)))
                throw new AddressFormatException("Invalid province!");
            if (!(await IsValidAddresses(address.AddressLine1)))
                throw new AddressFormatException("Invalid addressLine1!");

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
            return GetUndelatedAddresses().Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<Address>> GetAsync(IEnumerable<Guid> ids)
        {
            var addresses = GetUndelatedAddresses().
                Where(address => ids.Contains(address.Id));
            return new ValueTask<ICollection<Address>>(addresses.ToList());
        }

        public ValueTask<Address> GetByIdAsync(Guid id)
        {
            return new ValueTask<Address>(GetUndelatedAddresses().
                FirstOrDefault(address => address.Id == id) ??
                throw new AddressNotFoundException("Address not found"));
        }

        public async ValueTask<Address> UpdateAsync(Address address, bool saveChanges = true)
        {
            var updatedAddress = GetUndelatedAddresses().
                FirstOrDefault(updateAddress => updateAddress.Id.Equals(address.Id));

            if (updatedAddress is null)
                throw new AddressNotFoundException("Address not found!");

            if (!(await IsValidAddresses(address.Province)))
                throw new AddressFormatException("Invalid province!");
            if (!(await IsValidAddresses(address.AddressLine1)))
                throw new AddressFormatException("Invalid addressLine1!");

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


        private ValueTask<bool> IsValidAddresses(string addresses)
        {
            if (!string.IsNullOrWhiteSpace(addresses))
                return new ValueTask<bool>(true);
            else return new ValueTask<bool>(false);
        }
        private ValueTask<bool> IsValidZipCode(string zipCode)
        {
            if (!string.IsNullOrWhiteSpace(zipCode))
            {
                var countCharacter = 0;
                for (int index = 0; index < zipCode.Length; index++)
                {
                    if (!char.IsNumber(zipCode[index]))
                        countCharacter++;
                }
                if (countCharacter == 0) return new ValueTask<bool>(true);
                else return new ValueTask<bool>(false);
            }
            return new ValueTask<bool>(false);
        }

        private IQueryable<Address> GetUndelatedAddresses() => _appDataContext.Addresses
            .Where(address => !address.IsDeleted).AsQueryable();
    }
}
