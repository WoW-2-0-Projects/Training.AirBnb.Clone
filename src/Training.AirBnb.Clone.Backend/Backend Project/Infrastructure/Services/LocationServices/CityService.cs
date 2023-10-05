using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.LocationServices
{
    public class CityService : IEntityBaseService<City>
    {
        private readonly IDataContext _appDataContext;
        public CityService(IDataContext dataContext)
        {
            _appDataContext = dataContext;
        }

        public async ValueTask<City> CreateAsync(City city, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            if (!IsUnique(city))
                throw new DuplicateEntityException<City> ("This City already Exists");

            if (!IsValidCityName(city))
                throw new EntityValidationException<City> ("The city is in the wrong format");

            await _appDataContext.Cities.AddAsync(city, cancellationToken);
            
            if (saveChanges) await _appDataContext.SaveChangesAsync();

            return city;
        }

        public async ValueTask<City> UpdateAsync(City city, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var foundCity = await GetByIdAsync(city.Id);
            
            if (!IsValidCityName(city))
                throw new EntityValidationException<City> ("The city is in the wrong format");
            
            foundCity.Name = city.Name;
            
            await _appDataContext.Cities.UpdateAsync(foundCity, cancellationToken);

            if (saveChanges) await _appDataContext.Cities.SaveChangesAsync(cancellationToken);
            
            return foundCity;
        }

        public IQueryable<City> Get(Expression<Func<City, bool>> predicate) 
            => GetUndeletedCities().Where(predicate.Compile()).AsQueryable();

        public ValueTask<ICollection<City>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
            => new ValueTask<ICollection<City>>(GetUndeletedCities()
                .Where(city => ids
                .Contains(city.Id)).ToList());
    
        public ValueTask<City> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => new ValueTask<City>(GetUndeletedCities()
            .FirstOrDefault(amenity => amenity.Id == id)
            ?? throw new EntityNotFoundException<Amenity>());

        public async ValueTask<City> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var deletedCity = await GetByIdAsync(id);
            
            await _appDataContext.Cities.RemoveAsync(deletedCity, cancellationToken);
            
            if (saveChanges)
                await _appDataContext.Cities.SaveChangesAsync(cancellationToken);
            
            return deletedCity;
        }

        public async ValueTask<City> DeleteAsync(City city, bool saveChanges = true, CancellationToken cancellationToken = default)
            => await DeleteAsync(city.Id, saveChanges, cancellationToken);

        private IQueryable<City> GetUndeletedCities() => _appDataContext.Cities
            .Where(city => !city.IsDeleted).AsQueryable();

        private bool IsValidCityName(City city)
        {
            if (string.IsNullOrWhiteSpace(city.Name)
                || city.Name.Length <= 4
                || city.Name.Length > 185
                || city.CountryId.Equals(Guid.Empty))
                return false;
            
            return true;
        }

        private bool IsUnique(City city) => !GetUndeletedCities().Any(c => c.Name.Equals(city.Name));
    }
}