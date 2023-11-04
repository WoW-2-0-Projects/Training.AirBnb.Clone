using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ReservationServices;

public interface IReservationOccupancyService
{
    IQueryable<ReservationOccupancy> Get(Expression<Func<ReservationOccupancy, bool>> predicate);

    ValueTask<ICollection<ReservationOccupancy>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ReservationOccupancy> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ReservationOccupancy> CreateAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ReservationOccupancy> UpdateAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ReservationOccupancy> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ReservationOccupancy> DeleteAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default);
}