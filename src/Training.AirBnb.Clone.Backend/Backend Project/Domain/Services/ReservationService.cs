using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using System.Linq.Expressions;
using Backend_Project.Persistance.DataContexts;

namespace Backend_Project.Domain.Services
{
    public class ReservationService : IEntityBaseService<Reservation>
    {
        private readonly IDataContext _appDataContext;

        public ReservationService(IDataContext appDateContext)
        {
            _appDataContext = appDateContext;
        }
        
        public async ValueTask<Reservation> CreateAsync(Reservation entity, bool saveChanges = true)
        {
            if(IsValidEntity(entity))
                await _appDataContext.Reservations.AddAsync(entity); 
            if (saveChanges)
               await _appDataContext.Reservations.SaveChangesAsync(); 
            return entity;
        }

        public async ValueTask<Reservation> DeleteAsync(Guid id, bool saveChanges = true)
        {
            var removedReservation = await GetById(id);
            if (removedReservation is null)
                throw new ArgumentNullException();
            removedReservation.IsDeleted = true;
            removedReservation.DeletedDate = DateTimeOffset.UtcNow;
            await _appDataContext.Reservations.SaveChangesAsync();
            return  removedReservation;
        }

        public async ValueTask<Reservation> DeleteAsync(Reservation entity, bool saveChanges = true)
        {
            var removedReservation = await GetById(entity.Id);
            if (removedReservation is null)
                throw new ArgumentNullException();
            removedReservation.IsDeleted = true;
            removedReservation.DeletedDate = DateTimeOffset.UtcNow;
            await _appDataContext.Reservations.SaveChangesAsync();
            return removedReservation;
        }

        public IQueryable<Reservation> Get(Expression<Func<Reservation, bool>> predicate)
        {
            return _appDataContext.Reservations.Where(predicate.Compile()).AsQueryable();
        }

        public ValueTask<ICollection<Reservation>> Get(IEnumerable<Guid> ids)
        {
            var reservation = _appDataContext.Reservations
                .Where(reservation => ids.Contains(reservation.Id));
            return new ValueTask<ICollection<Reservation>>(reservation.ToList());
        }

        public ValueTask<Reservation> GetById(Guid id)
        {
            var reservation =_appDataContext.Reservations.FirstOrDefault(reservation => reservation.Id.Equals(id));
            if (reservation is null)
                throw new ArgumentNullException("Reservation not found");
            return new ValueTask<Reservation?>(reservation);
        }

        public ValueTask<Reservation> UpdateAsync(Reservation entity, bool saveChanges = true)
        {
            var foundReseervation = _appDataContext.Reservations.FirstOrDefault(reservation =>
                reservation.Id.Equals(entity.Id));
            if (foundReseervation is null)
                throw new ArgumentNullException();
            foundReseervation.ListingId = entity.ListingId;
            foundReseervation.BookedBy = entity.BookedBy;
            foundReseervation.OccupancyId = entity.OccupancyId;
            foundReseervation.StartDate = entity.StartDate;
            foundReseervation.EndDate = entity.EndDate;
            foundReseervation.TotalPrice = entity.TotalPrice;
            
            _appDataContext.Reservations.SaveChangesAsync();
            return new ValueTask<Reservation>(foundReseervation);
        }

        private bool IsValidEntity(Reservation entity)
        {
            if (entity.ListingId.Equals(default))
                return false;
            if(entity.BookedBy.Equals(default))
                return false;
            if (entity.OccupancyId.Equals(default))
                return false;
            if (entity.StartDate.Equals(default))
                return false;
            if(entity.EndDate.Equals(default))
                return false;
            if(!(entity.TotalPrice is decimal))
                return false;
            return true;
                
        }
    }
}
