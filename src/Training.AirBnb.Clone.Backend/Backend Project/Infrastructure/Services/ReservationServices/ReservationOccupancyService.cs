using Backend_Project.Application.Foundations.ReservationServices;
using Backend_Project.Application.Reservations.Settings;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.ReservationServices
{
    public class ReservationOccupancyService : IReservationOccupancyService
    {
        private readonly ReservationOccupancySettings _occupancysettings;
        private readonly IDataContext _appDataContext;

        public ReservationOccupancyService(IOptions<ReservationOccupancySettings> reservationOccupansySettings, IDataContext appDataContext)
        {
            _occupancysettings = reservationOccupansySettings.Value;
            _appDataContext = appDataContext;
        }

        public async ValueTask<ReservationOccupancy> CreateAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsValidOccupancy(reservationOccupancy))
                throw new EntityValidationException<ReservationOccupancy> ("This ReservationOccupancy is not valid");
            
            await _appDataContext.ReservationOccupancies.AddAsync(reservationOccupancy, cancellationToken);
                   
            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return reservationOccupancy;
        }

        public async ValueTask<ReservationOccupancy> UpdateAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsValidOccupancy(reservationOccupancy))
                throw new EntityValidationException<ReservationOccupancy> ("This ReservationOccupation not valid");
            
            var foundReservationOccupancy = await GetByIdAsync(reservationOccupancy.Id, cancellationToken);
            
            foundReservationOccupancy.Adults = reservationOccupancy.Adults;
            foundReservationOccupancy.Children = reservationOccupancy.Children;
            foundReservationOccupancy.Infants = reservationOccupancy.Infants;
            foundReservationOccupancy.Pets = reservationOccupancy.Pets;
            
            await _appDataContext.ReservationOccupancies.UpdateAsync(foundReservationOccupancy, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return foundReservationOccupancy;
        }

        public IQueryable<ReservationOccupancy> Get(Expression<Func<ReservationOccupancy, bool>> predicate)
            => GetUndelatedReservatinOccupancies().Where(predicate.Compile()).AsQueryable();

        public ValueTask<ICollection<ReservationOccupancy>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => new(GetUndelatedReservatinOccupancies()
                .Where(reservationOccupancy => ids
                .Contains(reservationOccupancy.Id)).ToList());

        public ValueTask<ReservationOccupancy> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => new (GetUndelatedReservatinOccupancies()
                .FirstOrDefault(reservationOccupancy =>  reservationOccupancy.Id.Equals(id))
                ?? throw new EntityNotFoundException<ReservationOccupancy> ("ReservationOccupancy not found."));

        public async ValueTask<ReservationOccupancy> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var removedReservationOccupancy = await GetByIdAsync(id, cancellationToken);

            await _appDataContext.ReservationOccupancies.RemoveAsync(removedReservationOccupancy, cancellationToken);

            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return removedReservationOccupancy;
        }

        public async ValueTask<ReservationOccupancy> DeleteAsync(ReservationOccupancy reservationOccupancy, bool saveChanges = true, CancellationToken cancellationToken = default)
            => await DeleteAsync(reservationOccupancy.Id, saveChanges, cancellationToken);

        private bool IsValidOccupancy(ReservationOccupancy reservationOccupancy)
            => (reservationOccupancy.Adults >= _occupancysettings.MinAdults && reservationOccupancy.Adults <= _occupancysettings.MaxAdults)
            || (reservationOccupancy.Children >= _occupancysettings.MinChildren && reservationOccupancy.Children <= _occupancysettings.MaxChildren)
            || (reservationOccupancy.Infants >= _occupancysettings.MinInfants && reservationOccupancy.Infants <= _occupancysettings.MaxInfants)
            || (reservationOccupancy.Pets >= _occupancysettings.MinPets && reservationOccupancy.Pets <= _occupancysettings.MaxPets);
        
        private IQueryable<ReservationOccupancy> GetUndelatedReservatinOccupancies() => _appDataContext.ReservationOccupancies
            .Where(rsO => !rsO.IsDeleted).AsQueryable();
    }
}