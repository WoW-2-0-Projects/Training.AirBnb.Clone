using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.ListingOccupancyExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ReservationServices
{
    public class ReservationOccupancyService : IEntityBaseService<ReservationOccupancy>
    {
        private IDataContext _appDataContext;
        public ReservationOccupancyService(IDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }
        public async ValueTask<ReservationOccupancy> CreateAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsValidOccupancy(reservationOccupancy))
                throw new ReservationOccupancyValidationException("This Occupancy is not valid");
            else
                await _appDataContext.ReservationOccupancies.AddAsync(reservationOccupancy, cancellationToken);
            if (saveChanges)
                await _appDataContext.ReservationOccupancies.SaveChangesAsync();
            return reservationOccupancy;
        }

        public async ValueTask<ReservationOccupancy> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var removedReservationOccupancy = await GetByIdAsync(id);
            removedReservationOccupancy.IsDeleted = true;
            removedReservationOccupancy.DeletedDate = DateTimeOffset.UtcNow;
            if (saveChanges)
                await _appDataContext.ReservationOccupancies.SaveChangesAsync();
            return removedReservationOccupancy;
        }

        public async ValueTask<ReservationOccupancy> DeleteAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
        {

            return await DeleteAsync(reservationOccupancy.Id, saveChanges);
        }

        public IQueryable<ReservationOccupancy> Get(Expression<Func<ReservationOccupancy, bool>> predicate)
        {
            return GetUndelatedReservatinOccupancies().Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<ReservationOccupancy>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var reservationOccupancy = GetUndelatedReservatinOccupancies()
                .Where(reservationOccupanc => ids.Contains(reservationOccupanc.Id));
            return new ValueTask<ICollection<ReservationOccupancy>>(reservationOccupancy.ToList());
        }

        public ValueTask<ReservationOccupancy> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var reservationOccupancy = GetUndelatedReservatinOccupancies()
                .FirstOrDefault(rsO => rsO.Id.Equals(id));
            if (reservationOccupancy is null)
                throw new ReservationOccupancyNotFoundException("Listing Occupation not found");
            return new ValueTask<ReservationOccupancy>(reservationOccupancy);
        }

        public async ValueTask<ReservationOccupancy> UpdateAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var foundReservationOccupancy = await GetByIdAsync(reservationOccupancy.Id);
            if (!IsValidOccupancy(foundReservationOccupancy))
                throw new ReservationOccupancyValidationException("This listingOccupation not valid");
            foundReservationOccupancy.Adults = reservationOccupancy.Adults;
            foundReservationOccupancy.Children = reservationOccupancy.Children;
            foundReservationOccupancy.Infants = reservationOccupancy.Infants;
            foundReservationOccupancy.Pets = reservationOccupancy.Pets;
            foundReservationOccupancy.ModifiedDate = DateTime.UtcNow;
            if (saveChanges)
                await _appDataContext.ReservationOccupancies.SaveChangesAsync();
            return foundReservationOccupancy;

        }
        private bool IsValidOccupancy(ReservationOccupancy reservationOccupancy)
        {
            if (reservationOccupancy.Adults < 0 && reservationOccupancy.Adults > 50)
                return false;
            if (reservationOccupancy.Children < 0 && reservationOccupancy.Children > 50)
                return false;
            if (reservationOccupancy.Infants < 0 && reservationOccupancy.Infants > 50)
                return false;
            if (reservationOccupancy.Pets < 0 && reservationOccupancy.Pets > 50)
                return false;
            return true;
        }
        private IQueryable<ReservationOccupancy> GetUndelatedReservatinOccupancies() => _appDataContext.ReservationOccupancies
            .Where(rsO => !rsO.IsDeleted).AsQueryable();
    }
}