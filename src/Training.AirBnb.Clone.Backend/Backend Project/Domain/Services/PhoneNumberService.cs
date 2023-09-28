using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.PhoneNumberExceptions;
using Backend_Project.Domain.Exceptions.User;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Data;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services;

public class PhoneNumberService : IEntityBaseService<PhoneNumber>
{
    private readonly IDataContext _appDataContext;

    public PhoneNumberService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<PhoneNumber> CreateAsync(PhoneNumber phoneNumber, bool saveChanges = true)
    {
        if (!(IsValidPhoneNumber(phoneNumber.UserPhoneNumber)))
            throw new PhoneNumberFormatException();
        if (string.IsNullOrWhiteSpace(phoneNumber.UserPhoneNumber))
            throw new ArgumentNullException("The phone number cannot be empty");
        if (IsUnique(phoneNumber.UserPhoneNumber))
            throw new PhoneNumberAlreadyExistsException ("This phone number already exists");
        if (saveChanges)
            await _appDataContext.PhoneNumbers.SaveChangesAsync();
        return phoneNumber;
    }

    public async ValueTask<PhoneNumber> DeleteAsync(Guid id, bool saveChanges = true)
    {
        var deletedNumber = await GetByIdAsync(id);
        if (deletedNumber is null)
            throw new PhoneNumberNotFoundException("Phone number not found");
        deletedNumber.DeletedDate = DateTimeOffset.UtcNow;
        deletedNumber.IsDeleted = true;
        if (saveChanges)
            await _appDataContext.PhoneNumbers.SaveChangesAsync();
        return deletedNumber;
    }

    public async ValueTask<PhoneNumber> DeleteAsync(PhoneNumber phoneNumber, bool saveChanges = true)
    {
        var deletedNumber = await GetByIdAsync(phoneNumber.Id);
        if (deletedNumber is null)
            throw new PhoneNumberNotFoundException("Phone number not found");
        deletedNumber.DeletedDate = DateTimeOffset.UtcNow;
        deletedNumber.IsDeleted = true;
        if (saveChanges)
            await _appDataContext.PhoneNumbers.SaveChangesAsync();
        return deletedNumber;
    }

    public IQueryable<PhoneNumber> Get(Expression<Func<PhoneNumber, bool>> predicate)
    {
        return _appDataContext.PhoneNumbers.Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<ICollection<PhoneNumber>> GetAsync(IEnumerable<Guid> ids)
    {
        var phoneNumbers = _appDataContext.PhoneNumbers
            .Where(number => ids.Contains(number.Id));
        return new ValueTask<ICollection<PhoneNumber>>(phoneNumbers.ToList());
    }

    public ValueTask<PhoneNumber> GetByIdAsync(Guid id)
    {
        return new ValueTask<PhoneNumber>(_appDataContext.PhoneNumbers.
            FirstOrDefault(number => number.Id == id && !number.IsDeleted) ??
            throw new PhoneNumberNotFoundException("Phone number not found"));
    }

    public async ValueTask<PhoneNumber> UpdateAsync(PhoneNumber phoneNumber, bool saveChanges = true)
    {
        var updatedNumber = await GetByIdAsync(phoneNumber.Id);
        if (updatedNumber is null)
            throw new PhoneNumberNotFoundException("Phone number not found");
        if (!(IsValidPhoneNumber(phoneNumber.UserPhoneNumber)))
            throw new PhoneNumberFormatException("Invalid phone number");
        updatedNumber.UserPhoneNumber = phoneNumber.UserPhoneNumber;
        updatedNumber.Code = phoneNumber.Code;
        updatedNumber.ModifiedDate = DateTimeOffset.UtcNow;
        updatedNumber.CountryId = phoneNumber.CountryId;
        if (saveChanges)
            await _appDataContext.PhoneNumbers.SaveChangesAsync();
        return updatedNumber;
    }

    private bool IsUnique(string phoneNumber) => _appDataContext.PhoneNumbers
             .Any(number => number.UserPhoneNumber == phoneNumber);

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return true;
    }
}
