using Backend_Project.Application.Foundations.AccountServices;
using Backend_Project.Application.Identity;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.AccountServices;

public class UserCredentialsService : IUserCredentialsService
{
    private readonly IDataContext _appDataContext;

    public UserCredentialsService(IDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (userCredentials.UserId == default) throw new EntityValidationException<UserCredentials>("User id is not valid");
        if (!IsUnique(userCredentials.UserId)) throw new DuplicateEntityException<UserCredentials>("This user already has credential");

        await _appDataContext.UserCredentials.AddAsync(userCredentials, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return userCredentials;
    }

    public IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate) =>
       GetUndeletedUserCredentials().Where(predicate.Compile()).AsQueryable();

    public ValueTask<ICollection<UserCredentials>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var userCredentials = GetUndeletedUserCredentials()
            .Where(credential => ids.Contains(credential.Id));

        return new ValueTask<ICollection<UserCredentials>>(userCredentials.ToList());
    }

    public ValueTask<UserCredentials> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        new (GetUndeletedUserCredentials()
            .FirstOrDefault(credential => credential.Id == id) ??
        throw new EntityNotFoundException<UserCredentials>("Credential not found"));

    public async ValueTask<UserCredentials> UpdateAsync(UserCredentials newUserCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var userCredentals = await GetByIdAsync(newUserCredentials.Id, cancellationToken);
                
        await _appDataContext.UserCredentials.UpdateAsync(userCredentals, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return userCredentals;

    }
    private IQueryable<UserCredentials> GetUndeletedUserCredentials() =>
        _appDataContext.UserCredentials
            .Where(userCredentials => !userCredentials.IsDeleted).AsQueryable();

    public async ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var deletedUserCredentials = await GetByIdAsync(id, cancellationToken);

        await _appDataContext.UserCredentials.RemoveAsync(deletedUserCredentials, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return deletedUserCredentials;
    }

    public async ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default)
        => await DeleteAsync(userCredentials.Id, saveChanges, cancellationToken);
  
    private bool IsUnique(Guid userId) =>
        !GetUndeletedUserCredentials().Any(cred => cred.UserId == userId);
}