using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository for managing email templates in the data store.
/// </summary>
public interface IEmailTemplateRepository
{
    /// <summary>
    /// Gets a queryable collection of email templates based on the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate for filtering email templates (optional).</param>
    /// <param name="asNoTracking">Flag indicating whether to use query tracking or not.</param>
    /// <returns>An <see cref="IQueryable"/> of email templates.</returns>
    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default, bool asNoTracking = false);

    /// <summary>
    /// Asynchronously creates a new email template.
    /// </summary>
    /// <param name="emailTemplate">The email template to create.</param>
    /// <param name="saveChanges">Flag indicating whether to save changes to the data store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the created email template.</returns>
    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
}