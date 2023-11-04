using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.NotificationServices;

public interface IEmailService
{
    IQueryable<Email> Get(Expression<Func<Email, bool>> predicate);

    ValueTask<ICollection<Email>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Email> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Email> CreateAsync(Email email, bool saveChanges = true, CancellationToken cancellationToken = default);
}