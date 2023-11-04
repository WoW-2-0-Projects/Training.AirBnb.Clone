using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ReviewServices;

public interface ICommentService
{
    IQueryable<Comment> Get(Expression<Func<Comment, bool>> predicate);

    ValueTask<ICollection<Comment>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Comment> CreateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> UpdateAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Comment> DeleteAsync(Comment comment, bool saveChanges = true, CancellationToken cancellationToken = default);
}