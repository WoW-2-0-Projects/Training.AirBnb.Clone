using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.Notification;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for managing SMS templates.
/// </summary>
public interface ISmsTemplateService
{
    /// <summary>
    /// Asynchronously retrieves a list of SMS templates based on the provided query specification.
    /// </summary>
    /// <param name="querySpecification">The query specification for filtering SMS templates.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the list of SMS templates.</returns>
    ValueTask<IList<SmsTemplate>> GetAsync(QuerySpecification<SmsTemplate> querySpecification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously retrieves a list of SMS templates based on a generic query specification.
    /// </summary>
    /// <param name="querySpecification">The generic query specification for filtering SMS templates.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the list of SMS templates.</returns>
    ValueTask<IList<SmsTemplate>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default);

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