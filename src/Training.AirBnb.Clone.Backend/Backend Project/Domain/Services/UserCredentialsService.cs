using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.User;
using Backend_Project.Domain.Exceptions.UserCredentialsExceptions;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
namespace Backend_Project.Domain.Services;
public class UserCredentialsService:IEntityBaseService<UserCredentials>
{
    private readonly IDataContext _appDataContext;
    public UserCredentialsService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true)
    {
        var passwordInfo = IsStrongPassword(userCredentials.Password);
        if (!passwordInfo.IsStrong)
            throw new NotValidUserCredentialsException(passwordInfo.WarningMessage);
        if (userCredentials.UserId == default)
            throw new UserFormatException("User id is not valid");
        if (!IsUnique(userCredentials.UserId))
            throw new UserCredentailsAlreadyExistsException("This user already has credential");
        userCredentials.Password = PasswordHasherService.Hash(userCredentials.Password);
        await _appDataContext.UserCredentials.AddAsync(userCredentials);
        if (saveChanges)
            await _appDataContext.UserCredentials.SaveChangesAsync();
        return userCredentials;
    }

    public async  ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true)
    {
        var deletedUserCredentials = await GetByIdAsync(id);
        deletedUserCredentials.IsDeleted = true;
        deletedUserCredentials.DeletedDate = DateTimeOffset.UtcNow;
        if (saveChanges)
            await _appDataContext.UserCredentials.SaveChangesAsync();
        return deletedUserCredentials;
    }

    public async ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true)
    {
        var deletedUserCredentials = await GetByIdAsync(userCredentials.Id);
        deletedUserCredentials.IsDeleted = true;
        deletedUserCredentials.DeletedDate = DateTime.UtcNow;
        if (saveChanges)
            await _appDataContext.UserCredentials.SaveChangesAsync();
        return deletedUserCredentials;
    }

    public IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate)=>
       GetUndeletedUserCredentials().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<UserCredentials>> GetAsync(IEnumerable<Guid> ids)
    {
        var userCredentials = GetUndeletedUserCredentials()
            .Where(credential => ids.Contains(credential.Id));
        return new ValueTask<ICollection<UserCredentials>>(userCredentials.ToList());
    }

    public ValueTask<UserCredentials> GetByIdAsync(Guid id) =>
        new ValueTask<UserCredentials>(GetUndeletedUserCredentials()
            .FirstOrDefault(credential => credential.Id == id) ??
        throw new UserCredentialsNotFoundException("Credential not found"));
    
    public async ValueTask<UserCredentials> UpdateAsync(UserCredentials newUserCredentials, bool saveChanges = true)
    {
        var userCredentals = await GetByIdAsync(newUserCredentials.Id);
        var passwordInfo = IsStrongPassword(newUserCredentials.Password);
        if (!passwordInfo.IsStrong)
            throw new NotValidUserCredentialsException(passwordInfo.WarningMessage);
        if (!PasswordHasherService.Verify(newUserCredentials.Password,userCredentals.Password))
            throw new NotValidUserCredentialsException("New password can not be same as old password");
        userCredentals.Password = PasswordHasherService.Hash(newUserCredentials.Password);
        userCredentals.ModifiedDate = DateTimeOffset.UtcNow;
        if (saveChanges)
            await _appDataContext.UserCredentials.SaveChangesAsync();
        return userCredentals;

    }
    private IQueryable<UserCredentials> GetUndeletedUserCredentials() =>
        _appDataContext.UserCredentials
            .Where(userCredentials => !userCredentials.IsDeleted).AsQueryable();
    private (bool IsStrong,string WarningMessage) IsStrongPassword(string password)
    {
        if (password.Length < 8)
            return (false, "Password can not be less than 8 character");
        if (!password.Any(char.IsDigit)) 
            return (false, "Password should contain at least one digit!");
        if (!password.Any(char.IsUpper)) 
            return ( false,"Password should contain at least one upper case letter!");
        if (!password.Any(char.IsLower)) 
            return ( false,"Password should contain at least one lower case letter!");
        if (!password.Any(char.IsPunctuation)) 
            return ( false,$"Password should contain at least one symbol like {"!@#$%^&?"}!");
        return (true, "");
    }
    private bool IsUnique(Guid userId) =>
        !GetUndeletedUserCredentials().Any(cred => cred.UserId == userId);
}
