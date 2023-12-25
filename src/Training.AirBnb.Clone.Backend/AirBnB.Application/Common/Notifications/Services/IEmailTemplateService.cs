using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.Notification;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for managing email templates.
/// </summary>
public interface IEmailTemplateService
{
    /// <summary>
    /// Asynchronously retrieves a list of email templates based on the provided query specification.
    /// </summary>
    /// <param name="querySpecification">The query specification for filtering email templates.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the list of email templates.</returns>
    ValueTask<IList<EmailTemplate>> GetAsync(QuerySpecification<EmailTemplate> querySpecification, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously retrieves a list of email templates based on a generic query specification.
    /// </summary>
    /// <param name="querySpecification">The generic query specification for filtering email templates.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the list of email templates.</returns>
    ValueTask<IList<EmailTemplate>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves an email template by its notification template type.
    /// </summary>
    /// <param name="templateType">The notification template type.</param>
    /// <param name="asNoTracking">Flag indicating whether to use query tracking or not.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the email template or null if not found.</returns>
    ValueTask<EmailTemplate?> GetByTypeAsync(NotificationTemplateType templateType, bool asNoTracking = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a new email template.
    /// </summary>
    /// <param name="emailTemplate">The email template to create.</param>
    /// <param name="saveChanges">Flag indicating whether to save changes to the data store.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation, containing the created email template.</returns>
    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
}