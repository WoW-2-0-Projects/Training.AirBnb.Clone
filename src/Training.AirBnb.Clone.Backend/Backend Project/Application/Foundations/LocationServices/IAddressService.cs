using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.LocationServices;

public interface IAddressService
{
    IQueryable<Address> Get(Expression<Func<Address, bool>> predicate);

    ValueTask<ICollection<Address>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Address> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Address> CreateAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Address> UpdateAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Address> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Address> DeleteAsync(Address address, bool saveChanges = true, CancellationToken cancellationToken = default);
}