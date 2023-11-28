using AirBnB.Application.Common.Models.Querying;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Notifications.Services;

public interface IEmailTemplateService
{
    /// <summary>
    /// it takes emailTemplate by filtering
    /// </summary>
    /// <param name="pagination"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Ilist EmailTemplate</returns>
    ValueTask<IList<EmailTemplate>> GetByFilterAsync(
        FilterPagination pagination,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// it takes emailTemplate By type
    /// </summary>
    /// <param name="templateType"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>default or emailTemplate</returns>
    ValueTask<EmailTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// it creates by emailTemplate
    /// </summary>
    /// <param name="template"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>emailTemplate</returns>
    ValueTask<EmailTemplate> CreateAsync(
       EmailTemplate template,
       bool saveChanges = true,
       CancellationToken cancellationToken = default);
}