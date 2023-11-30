using AirBnB.Domain.Entities;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories.Interfaces;

public interface ISmsTemplateRepository
{
    /// <summary>
    /// Returns queryable of email templates
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns>IQueryable<EmailTemplate> Get </returns>
    IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Create SmsTemplate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="saveChanges"></param>
    /// <returns>ValueTask<SmsTemplate</returns>
    ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
}