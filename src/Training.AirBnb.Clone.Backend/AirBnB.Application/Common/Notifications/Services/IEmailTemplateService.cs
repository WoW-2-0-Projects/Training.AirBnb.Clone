using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AirBnB.Application.Common.Notifications.Services;

/// <summary>
/// Represents a service for managing email templates.
/// </summary>
public interface IEmailTemplateService
{
    /// <summary>
    /// Retrieves a queryable collection of EmailTemplate entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the EmailTemplate entities (optional).</param>
    /// <param name="asNoTracking">Indicates whether to disable change tracking for the entities (default: false).</param>
    /// <returns>A queryable collection of EmailTemplate entities.</returns>
    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTracking = false);
    
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