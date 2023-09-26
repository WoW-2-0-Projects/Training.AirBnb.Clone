using System.Linq.Expressions;

namespace Backend_Project.Domain.Interfaces;

public interface IEntityBaseService<T> where T : class
{
    IQueryable<T> Get(Expression<Func<T, bool>> predicate);

    ValueTask<ICollection<T>> GetAsync(IEnumerable<Guid> ids);

    ValueTask<T> GetByIdAsync(Guid id);

    ValueTask<T> CreateAsync(T entity, bool saveChanges = true);

    ValueTask<T> UpdateAsync(T entity, bool saveChanges = true);

    ValueTask<T> DeleteAsync(Guid id, bool saveChanges = true);

    ValueTask<T> DeleteAsync(T entity, bool saveChanges = true);
}