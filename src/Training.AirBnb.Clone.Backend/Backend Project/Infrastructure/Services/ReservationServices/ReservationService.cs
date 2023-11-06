using Backend_Project.Domain.Entities;
using System.Linq.Expressions;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Backend_Project.Application.Foundations.ReservationServices;
using Backend_Project.Application.Reservations;
using Microsoft.Extensions.Options;

namespace Backend_Project.Infrastructure.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationOccupancySettings _occupancySettings;
        private readonly IDataContext _appDataContext;

        public ReservationService(IOptions<ReservationOccupancySettings> occupancySettings, IDataContext appDateContext)
        {
            _appDataContext = appDateContext;
            _occupancySettings = occupancySettings.Value;
        }

        public async ValueTask<Reservation> CreateAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsNotBookedReservation(reservation))
                throw new EntityValidationException<Reservation> ("This reservations time already exists");

            if (!IsValidEntity(reservation))
                throw new EntityValidationException<Reservation>("Reservation didn't pass validation");
            
            await _appDataContext.Reservations.AddAsync(reservation, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return reservation;
        }

        public IQueryable<Reservation> Get(Expression<Func<Reservation, bool>> predicate)
            => GetUndelatedReservations().Where(predicate.Compile()).AsQueryable();

        public ValueTask<ICollection<Reservation>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => new (GetUndelatedReservations()
                .Where(reservation => ids
                .Contains(reservation.Id))
                .ToList());

        public ValueTask<Reservation> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var reservation = GetUndelatedReservations().FirstOrDefault(reservation => reservation.Id.Equals(id));

            return reservation is null
                ? throw new EntityNotFoundException<Reservation>("Reservation not found.")
                : new ValueTask<Reservation>(reservation);
        }

        public async ValueTask<Reservation> UpdateAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsValidEntity(reservation))
                throw new EntityValidationException<Reservation> ("Reservation is not valid.");
            
            var foundReseervation = await GetByIdAsync(reservation.Id, cancellationToken);

            foundReseervation.ListingId = reservation.ListingId;
            foundReseervation.BookedBy = reservation.BookedBy;
            foundReseervation.OccupancyId = reservation.OccupancyId;
            foundReseervation.StartDate = reservation.StartDate;
            foundReseervation.EndDate = reservation.EndDate;
            foundReseervation.TotalPrice = reservation.TotalPrice;
            
            await _appDataContext.Reservations.UpdateAsync(foundReseervation, cancellationToken);
            if (saveChanges)  await _appDataContext.SaveChangesAsync();
           
            return foundReseervation;
        }

        public async ValueTask<Reservation> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var removedReservation = await GetByIdAsync(id, cancellationToken);

            await _appDataContext.Reservations.RemoveAsync(removedReservation, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();
            
            return removedReservation;
        }

        public async ValueTask<Reservation> DeleteAsync(Reservation reservation, bool saveChanges = true, CancellationToken cancellationToken = default) 
            => await DeleteAsync(reservation.Id, saveChanges, cancellationToken);

        private bool IsValidEntity(Reservation reservation)
        {
            if (reservation.StartDate.Equals(default)
                || !IsValidDateStartDate(reservation.StartDate))
                return false;

            if (reservation.EndDate.Equals(default)
                || !IsValidDateEndDate(reservation.StartDate, reservation.EndDate))
                return false;

            if (reservation.TotalPrice <= _occupancySettings.MinReservationTotalPrice)
                return false;

            return true;

        }

        private static bool IsValidDateStartDate(DateTime startDate)
        {
            if (startDate <= DateTime.UtcNow) return false;

            return true;
        }

        private static bool IsValidDateEndDate(DateTime startDate, DateTime endDate)
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