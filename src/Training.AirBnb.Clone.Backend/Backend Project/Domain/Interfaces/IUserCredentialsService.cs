
using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Interfaces;

public interface IUserCredentialsService
{
    IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate);

    ValueTask<ICollection<UserCredentials>> GetAsync(IEnumerable<Guid> ids);

    ValueTask<UserCredentials> GetByIdAsync(Guid id);

    ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true);

    ValueTask<UserCredentials> UpdateAsync(string oldPassword,UserCredentials userCredentials, bool saveChanges = true);

    ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true);

    ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true);
}
