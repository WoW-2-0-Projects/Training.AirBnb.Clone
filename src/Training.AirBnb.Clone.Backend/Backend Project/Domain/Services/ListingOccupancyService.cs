using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Project.Domain.Services
{
    public class ListingOccupancyService : IEntityBaseService<ListingOccupancy>
    {
        public ValueTask<ListingOccupancy> CreateAsync(ListingOccupancy entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingOccupancy> DeleteAsync(Guid id, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingOccupancy> DeleteAsync(ListingOccupancy entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ListingOccupancy> Get(Expression<Func<ListingOccupancy, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ICollection<ListingOccupancy>> GetAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingOccupancy> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<ListingOccupancy> UpdateAsync(ListingOccupancy entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
