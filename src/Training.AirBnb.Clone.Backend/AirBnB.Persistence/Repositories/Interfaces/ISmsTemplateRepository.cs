using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Represents a repository for managing SMS templates in the data store.
/// </summary>
public interface ISmsTemplateRepository
{
    /// <summary>
    /// Gets a queryable collection of SMS templates based on the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate for filtering SMS templates (optional).</param>
    /// <param name="asNoTracking">Flag indicating whether to use query tracking or not.</param>
    /// <returns>An <see cref="IQueryable"/> of SMS templates.</returns>
    IQueryable<SmsTemplate> Get(Expression<Func<SmsTemplate, bool>>? predicate = default, bool asNoTracking = false);
    
    /// <summary>
    /// Asynchronously creates a new SMS template.
    /// </summary>
    /// <param name="smsTemplate">The SMS template to create.</param>
    /// <param name="saveChanges">Flag indicating whether to save changes to the data store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the created SMS template.</returns>
    ValueTask<SmsTemplate> CreateAsync(SmsTemplate smsTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
}