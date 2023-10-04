using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Data;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.PhoneNumberServices;

public class PhoneNumberService : IEntityBaseService<PhoneNumber>
{
    private readonly IDataContext _appDataContext;

    private readonly IEntityBaseService<Country> _country;

    public PhoneNumberService(IDataContext appDataContext, IEntityBaseService<Country> country)
    {
        _appDataContext = appDataContext;

        _country = country;
    }

    public async ValueTask<PhoneNumber> CreateAsync(PhoneNumber phoneNumber, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (IsNullable(phoneNumber))
            throw new EntityException<PhoneNumber>("The phone number cannot be empty");

        if (!await IsValidPhoneNumber(phoneNumber))
            throw new EntityValidationException<PhoneNumber>("Phone number not valid");

        if (IsUnique(phoneNumber.UserPhoneNumber))
            throw new DuplicateEntityException<PhoneNumber>("This phone number already exists");

        await _appDataContext.PhoneNumbers.AddAsync(phoneNumber, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return phoneNumber;
    }

    public ValueTask<ICollection<PhoneNumber>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var phoneNumbers = GetUndeletedNumbers()
            .Where(number => ids.Contains(number.Id));

        return new ValueTask<ICollection<PhoneNumber>>(phoneNumbers.ToList());
    }

    public ValueTask<PhoneNumber> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return new ValueTask<PhoneNumber>(GetUndeletedNumbers().
            FirstOrDefault(number => number.Id == id) ??
                throw new EntityNotFoundException<PhoneNumber>("Phone number not found"));
    }

    public IQueryable<PhoneNumber> Get(Expression<Func<PhoneNumber, bool>> predicate) =>
       GetUndeletedNumbers().Where(predicate.Compile()).AsQueryable();

    public async ValueTask<PhoneNumber> UpdateAsync(PhoneNumber phoneNumber, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!await IsValidPhoneNumber(phoneNumber))
            throw new EntityException<PhoneNumber>("Invalid phone number");

        var updatedNumber = await GetByIdAsync(phoneNumber.Id);

        updatedNumber.UserPhoneNumber = phoneNumber.UserPhoneNumber;
        updatedNumber.Code = phoneNumber.Code;
        updatedNumber.CountryId = phoneNumber.CountryId;

        await _appDataContext.PhoneNumbers.UpdateAsync(updatedNumber, cancellationToken);
        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return updatedNumber;
    }

    public async ValueTask<PhoneNumber> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var deletedNumber = await GetByIdAsync(id);

        if (deletedNumber is null)
            throw new EntityNotFoundException<PhoneNumber>("Phone number not found");
        await _appDataContext.PhoneNumbers.RemoveAsync(deletedNumber, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return deletedNumber;
    }

    public async ValueTask<PhoneNumber> DeleteAsync(PhoneNumber phoneNumber, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(phoneNumber.Id, saveChanges, cancellationToken);
   
    private bool IsUnique(string phoneNumber) => GetUndeletedNumbers()
             .Any(number => number.UserPhoneNumber == phoneNumber);

    private bool IsNullable(PhoneNumber phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber.UserPhoneNumber))
            return false;

        return true;
    }

    private async ValueTask<bool> IsValidPhoneNumber(PhoneNumber phoneNumber)
    {
        if (!phoneNumber.UserPhoneNumber[1..].All(char.IsDigit)) return false;
        if (phoneNumber.UserPhoneNumber[0] != '+') return false;

        var country = await _country.GetByIdAsync(phoneNumber.CountryId);

        if (!phoneNumber.UserPhoneNumber.StartsWith(country.CountryDialingCode)) return false;
        if (phoneNumber.UserPhoneNumber.Length != country.RegionPhoneNumberLength) return false;

        return true;
    }

    private IQueryable<PhoneNumber> GetUndeletedNumbers()
        => _appDataContext.PhoneNumbers.Where(number => !number.IsDeleted).AsQueryable();
}