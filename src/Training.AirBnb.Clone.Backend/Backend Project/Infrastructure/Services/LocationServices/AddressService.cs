using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.LocationServices
{
    public class AddressService : IEntityBaseService<Address>
    {
        private readonly IDataContext _appDataContext;

        public AddressService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async ValueTask<Address> CreateAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            Validate(address);

            await _appDataContext.Addresses.AddAsync(address, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return address;
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
                throw new EntityNotFoundException<Address>("Address not found"));
        }

        public IQueryable<Address> Get(Expression<Func<Address, bool>> predicate)
        {
            return GetUndeletedAddresses().Where(predicate.Compile()).AsQueryable();
        }

        public async ValueTask<Address> UpdateAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var updatedAddress = await GetByIdAsync(address.Id);

            Validate(address);

            updatedAddress.CityId = address.CityId;
            updatedAddress.AddressLine1 = address.AddressLine1;
            updatedAddress.AddressLine2 = address.AddressLine2;
            updatedAddress.AddressLine3 = address.AddressLine3;
            updatedAddress.AddressLine4 = address.AddressLine4;
            updatedAddress.Province = address.Province;
            updatedAddress.ZipCode = address.ZipCode;

            await _appDataContext.Addresses.UpdateAsync(updatedAddress, cancellationToken);
            
            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return updatedAddress;
        }

        public async ValueTask<Address> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedAddress = await GetByIdAsync(id);

            await _appDataContext.Addresses.RemoveAsync(deletedAddress, cancellationToken);

            if (saveChanges)
                await _appDataContext.SaveChangesAsync();

            return deletedAddress;
        }

        public async ValueTask<Address> DeleteAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default)
            => await DeleteAsync(address.Id, saveChanges, cancellationToken);

        private bool IsValidAddressLines(string addressLine)
        => !string.IsNullOrWhiteSpace(addressLine);

        private bool IsValidZipCode(string? zipCode)
        {
            if (zipCode is null)
                return true;
            
            for (int index = 0; index < zipCode.Length; index++)
                if (!char.IsNumber(zipCode[index]))
                    return false;
            return true;
        }
        private void Validate(Address address)
        {
            if (!IsValidAddressLines(address.AddressLine1))
                throw new EntityValidationException<Address>("Invalid province!");
            if (!IsValidZipCode(address.ZipCode))
                throw new EntityValidationException<Address>("Invalid zipCode!");
        }
       
        private IQueryable<Address> GetUndeletedAddresses() => _appDataContext.Addresses
            .Where(address => !address.IsDeleted).AsQueryable();
    }
}