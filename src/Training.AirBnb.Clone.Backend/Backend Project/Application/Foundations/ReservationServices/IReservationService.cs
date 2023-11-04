using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ReservationServices;

public interface IReservationService
{
    IQueryable<Reservation> Get(Expression<Func<Reservation, bool>> predicate);

    ValueTask<ICollection<Reservation>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Reservation> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Reservation> CreateAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Reservation> UpdateAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Reservation> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Reservation> DeleteAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default);
}