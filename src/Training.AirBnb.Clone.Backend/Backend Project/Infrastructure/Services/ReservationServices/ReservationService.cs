using Backend_Project.Domain.Entities;
using System.Linq.Expressions;
using Backend_Project.Persistance.DataContexts;
using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Exceptions.EntityExceptions;

namespace Backend_Project.Infrastructure.Services.ReservationServices
{
    public class ReservationService : IEntityBaseService<Reservation>
    {
        private readonly IDataContext _appDataContext;

        public ReservationService(IDataContext appDateContext)
        {
            _appDataContext = appDateContext;
        }

        public async ValueTask<Reservation> CreateAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsNotBookedReservation(reservation))
                throw new EntityValidationException<Reservation> ("This reservations time already exists");

            if (IsValidEntity(reservation))
                await _appDataContext.Reservations.AddAsync(reservation, cancellationToken);
            else
                throw new EntityValidationException<Reservation>("Reservation didn't pass validation");

            if (saveChanges)
                await _appDataContext.Reservations.SaveChangesAsync(cancellationToken);

            return reservation;
        }

        public IQueryable<Reservation> Get(Expression<Func<Reservation, bool>> predicate)
            => GetUndelatedReservations().Where(predicate.Compile()).AsQueryable();

        public ValueTask<ICollection<Reservation>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => new ValueTask<ICollection<Reservation>>(GetUndelatedReservations()
                .Where(reservation => ids
                .Contains(reservation.Id))
                .ToList());

        public ValueTask<Reservation> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var reservation = GetUndelatedReservations().FirstOrDefault(reservation => reservation.Id.Equals(id));
            
            if (reservation is null) throw new EntityNotFoundException<Reservation> ("Reservation not found.");
            
            return new ValueTask<Reservation>(reservation);
        }

        public async ValueTask<Reservation> UpdateAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var foundReseervation = GetUndelatedReservations().FirstOrDefault(reservation =>
                reservation.Id.Equals(reservation.Id));
            
            if (foundReseervation is null)
                throw new EntityNotFoundException<Reservation> ("Reservation not found.");
            
            if (!IsValidEntity(reservation))
                throw new EntityValidationException<Reservation> ("Reservation is not valid.");

            foundReseervation.ListingId = reservation.ListingId;
            foundReseervation.BookedBy = reservation.BookedBy;
            foundReseervation.OccupancyId = reservation.OccupancyId;
            foundReseervation.StartDate = reservation.StartDate;
            foundReseervation.EndDate = reservation.EndDate;
            foundReseervation.TotalPrice = reservation.TotalPrice;
            foundReseervation.ModifiedDate = DateTimeOffset.UtcNow;
            
            if (saveChanges)  await _appDataContext.Reservations.SaveChangesAsync(cancellationToken);
           
            return foundReseervation;
        }
        public IQueryable<Reservation> Get(Expression<Func<Reservation, bool>> predicate)
            => GetUndelatedReservations().Where(predicate.Compile()).AsQueryable();

        public async ValueTask<Reservation> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var removedReservation = await GetByIdAsync(id);

            removedReservation.IsDeleted = true;
            removedReservation.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.Reservations.SaveChangesAsync(cancellationToken);
            
            return removedReservation;
        }

        public async ValueTask<Reservation> DeleteAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default) 
            => await DeleteAsync(reservation.Id, saveChanges, cancellationToken);

        private bool IsValidEntity(Reservation reservation)
        {
            if (reservation.ListingId.Equals(default)) return false;

            if (reservation.BookedBy.Equals(default)) return false;

            if (reservation.OccupancyId.Equals(default)) return false;

            if (reservation.StartDate.Equals(default)
                || !IsValidDateStartDate(reservation.StartDate))
                return false;

            if (reservation.EndDate.Equals(default)
                || !IsValidDateEndDate(reservation.StartDate, reservation.EndDate))
                return false;

            if (reservation.TotalPrice <= 0)
                return false;

            return true;

        }

        private bool IsValidDateStartDate(DateTime startDate)
        {
            if (startDate <= DateTime.UtcNow) return false;

            return true;
        }
        private bool IsValidDateEndDate(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate) return false;

            if (endDate <= DateTime.UtcNow) return false;

            return true;
        }
        private IQueryable<Reservation> GetUndelatedReservations() => _appDataContext.Reservations
            .Where(res => !res.IsDeleted).AsQueryable();

        private IQueryable<Reservation> GetByListingId(Reservation reservation)
            => GetUndelatedReservations().Where(res => res.ListingId.Equals(reservation.ListingId)).AsQueryable();

        private bool IsNotBookedReservation(Reservation reservation) => GetByListingId(reservation)
            .All(res =>
            (res.StartDate > reservation.StartDate && res.StartDate > reservation.EndDate)
            || (res.EndDate < reservation.StartDate && res.EndDate < reservation.EndDate));
    }
}
