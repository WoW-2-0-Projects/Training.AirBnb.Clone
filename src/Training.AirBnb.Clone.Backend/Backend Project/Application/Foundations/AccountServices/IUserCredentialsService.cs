using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.AccountServices;

public interface IUserCredentialsService
{
    IQueryable<UserCredentials> Get(Expression<Func<UserCredentials, bool>> predicate);

    ValueTask<ICollection<UserCredentials>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> CreateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> UpdateAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<UserCredentials> DeleteAsync(UserCredentials userCredentials, bool saveChanges = true, CancellationToken cancellationToken = default);

}