using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.AccountServices;

public interface IPhoneNumberService
{
    IQueryable<PhoneNumber> Get(Expression<Func<PhoneNumber, bool>> predicate);

    ValueTask<ICollection<PhoneNumber>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationoken = default);

    ValueTask<PhoneNumber> GetByIdAsync(Guid id, CancellationToken cancellationoken = default);

    ValueTask<PhoneNumber> CreateAsync(PhoneNumber phoneNumber, bool saveChanges = true, CancellationToken cancellationoken = default);

    ValueTask<PhoneNumber> UpdateAsync(PhoneNumber phoneNumber, bool saveChanges = true, CancellationToken cancellationoken = default);

    ValueTask<PhoneNumber> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationoken = default);

    ValueTask<PhoneNumber> DeleteAsync(PhoneNumber phoneNumber, bool saveChanges = true, CancellationToken cancellationoken = default);
}