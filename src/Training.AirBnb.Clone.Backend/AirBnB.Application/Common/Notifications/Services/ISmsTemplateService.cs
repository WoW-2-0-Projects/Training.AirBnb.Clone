using AirBnB.Application.Common.Models.Querying;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Notifications.Services;

public interface ISmsTemplateService
{
    /// <summary>
    /// it takes smsTemplate by filtering
    /// </summary>
    /// <param name="pagination"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Ilist SmsTemplate</returns>
    ValueTask<IList<SmsTemplate>> GetByFilterAsync(
        FilterPagination pagination,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// it takes smsTemplate By type
    /// </summary>
    /// <param name="templateType"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>default or smsTemplate</returns>
    ValueTask<SmsTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// it creates by emailTemplate
    /// </summary>
    /// <param name="smsTemplate"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>smsTemplate</returns>
    ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default);
}
