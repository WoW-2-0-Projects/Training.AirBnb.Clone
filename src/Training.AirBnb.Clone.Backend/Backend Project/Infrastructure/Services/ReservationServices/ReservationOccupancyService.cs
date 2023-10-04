using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
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
                throw new EntityValidationException<ReservationOccupancy> ("This ReservationOccupancy is not valid");
            else
                await _appDataContext.ReservationOccupancies.AddAsync(reservationOccupancy, cancellationToken);
                   
            if (saveChanges)
                await _appDataContext.ReservationOccupancies.SaveChangesAsync(cancellationToken);

            return reservationOccupancy;
        }

        public async ValueTask<ReservationOccupancy> UpdateAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var foundReservationOccupancy = await GetByIdAsync(reservationOccupancy.Id);
            
            if (!IsValidOccupancy(foundReservationOccupancy))
                throw new EntityValidationException<ReservationOccupancy> ("This ReservationOccupation not valid");
            
            foundReservationOccupancy.Adults = reservationOccupancy.Adults;
            foundReservationOccupancy.Children = reservationOccupancy.Children;
            foundReservationOccupancy.Infants = reservationOccupancy.Infants;
            foundReservationOccupancy.Pets = reservationOccupancy.Pets;
            foundReservationOccupancy.ModifiedDate = DateTime.UtcNow;
            
            if (saveChanges)
                await _appDataContext.ReservationOccupancies.SaveChangesAsync(cancellationToken);

            return foundReservationOccupancy;
        }

        public IQueryable<ReservationOccupancy> Get(Expression<Func<ReservationOccupancy, bool>> predicate)
            => GetUndelatedReservatinOccupancies().Where(predicate.Compile()).AsQueryable();

        public ValueTask<ICollection<ReservationOccupancy>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => new ValueTask<ICollection<ReservationOccupancy>>(GetUndelatedReservatinOccupancies()
                .Where(reservationOccupancy => ids
                .Contains(reservationOccupancy.Id)).ToList());

        public ValueTask<ReservationOccupancy> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => new ValueTask<ReservationOccupancy> (GetUndelatedReservatinOccupancies()
                .FirstOrDefault(reservationOccupancy =>  reservationOccupancy.Id.Equals(id))
                ?? throw new EntityNotFoundException<ReservationOccupancy> ("ReservationOccupancy not found."));

        public async ValueTask<ReservationOccupancy> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var removedReservationOccupancy = await GetByIdAsync(id);

            removedReservationOccupancy.IsDeleted = true;
            removedReservationOccupancy.DeletedDate = DateTimeOffset.UtcNow;

            if (saveChanges)
                await _appDataContext.ReservationOccupancies.SaveChangesAsync(cancellationToken);

            return removedReservationOccupancy;
        }

        public async ValueTask<ReservationOccupancy> DeleteAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
            => await DeleteAsync(reservationOccupancy.Id, saveChanges, cancellationToken);

        private bool IsValidOccupancy(ReservationOccupancy reservationOccupancy)
            => (reservationOccupancy.Adults < 0 && reservationOccupancy.Adults > 50)
            || (reservationOccupancy.Children < 0 && reservationOccupancy.Children > 50)
            || (reservationOccupancy.Infants < 0 && reservationOccupancy.Infants > 50)
            || (reservationOccupancy.Pets < 0 && reservationOccupancy.Pets > 50)
            ? false: true;
        
        private IQueryable<ReservationOccupancy> GetUndelatedReservatinOccupancies() => _appDataContext.ReservationOccupancies
            .Where(rsO => !rsO.IsDeleted).AsQueryable();
    }
}