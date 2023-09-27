
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Extensions;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services;

public class UserCredentialsService:IUserCredentialsService
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
            throw new ArgumentException(passwordInfo.WarningMessage);
        userCredentials.Password = userCredentials.HashPassword();
        await _appDataContext.UserCredentials.AddAsync(userCredentials);
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return userCredentials;
    }

    public async  ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true)
    {
        var deletedUserCredentials = await GetByIdAsync(id);
        deletedUserCredentials.IsDeleted = true;
        deletedUserCredentials.DeletedDate = DateTime.UtcNow;
        return deletedUserCredentials;
    }

    public async ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true)
    {
        var deletedUserCredentials = await GetByIdAsync(userCredentials.Id);
        deletedUserCredentials.IsDeleted = true;
        deletedUserCredentials.DeletedDate = DateTime.UtcNow;
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


    public async ValueTask<UserCredentials> GetByIdAsync(Guid id) =>
        GetUndeletedUserCredentials().FirstOrDefault(credential => credential.Id == id) ??
        throw new ArgumentException();
    

    public async ValueTask<UserCredentials> UpdateAsync(string oldPassword,UserCredentials newUserCredentials, bool saveChanges = true)
    {
        var oldUserCredentials = await GetByIdAsync(newUserCredentials.Id);
        var passwordInfo = IsStrongPassword(newUserCredentials.Password);
        if (oldUserCredentials.VerifyPassword(oldPassword))
            throw new ArgumentException();
        if (passwordInfo.IsStrong)
            throw new ArgumentException(passwordInfo.WarningMessage);
        if (oldUserCredentials.VerifyPassword(newUserCredentials.Password))
            throw new ArgumentException("New password can not be same as old password");
        newUserCredentials.Password = newUserCredentials.HashPassword();
        oldUserCredentials.Password = newUserCredentials.Password;
        return oldUserCredentials;

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
            return ( false,"Password should contain at least one lower case! letter");
        if (!password.Any(char.IsPunctuation)) 
            return ( false,$"Password should contain at least one symbol like {"!@#$%^&?"}!");
        return (true, "");
    }
}
