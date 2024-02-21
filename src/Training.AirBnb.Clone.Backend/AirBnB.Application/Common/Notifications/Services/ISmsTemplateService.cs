using System.Linq.Expressions;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for managing SMS templates.
/// </summary>
public interface ISmsTemplateService
{
    /// <summary>
    /// Retrieves a queryable collection of SmsTemplate entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the SmsTemplate entities (optional).</param>
    /// <param name="asNoTracking">Indicates whether to disable change tracking for the entities (default: false).</param>
    /// <returns>A queryable collection of SmsTemplate entities.</returns>
    IQueryable<SmsTemplate> Get(Expression<Func<SmsTemplate, bool>>? predicate = default, bool asNoTracking = false);
 
    /// <summary>
    /// Asynchronously retrieves an SMS template by its notification template type.
    /// </summary>
    /// <param name="templateType">The notification template type.</param>
    /// <param name="asNoTracking">Flag indicating whether to use query tracking or not.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the SMS template or null if not found.</returns>
    ValueTask<SmsTemplate?> GetByTypeAsync(NotificationTemplateType templateType, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new SMS template.
    /// </summary>
    /// <param name="smsTemplate">The SMS template to create.</param>
    /// <param name="saveChanges">Flag indicating whether to save changes to the data store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the created SMS template.</returns>
    ValueTask<SmsTemplate> CreateAsync(SmsTemplate smsTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
}