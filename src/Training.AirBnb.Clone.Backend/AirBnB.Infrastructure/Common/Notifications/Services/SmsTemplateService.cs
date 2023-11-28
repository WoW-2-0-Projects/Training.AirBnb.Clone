using AirBnB.Application.Common.Models.Querying;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Application.Common.Querying.Extension;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

/// <summary>
/// created crud methods for smsTemplate
/// </summary>
public class SmsTemplateService(ISmsTemplateRepository smsTemplateRepository,
        IValidator<SmsTemplate> smsTemplateValidator) : ISmsTemplateService
{
    public async ValueTask<IList<SmsTemplate>> GetByFilterAsync(
        FilterPagination pagination, 
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        await smsTemplateRepository.Get(asNoTracking: asNoTracking)
            .ApplyPagination(pagination)
            .ToListAsync(cancellationToken);

    public async ValueTask<SmsTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
            await smsTemplateRepository.Get(template => template.TemplateType == templateType, asNoTracking)
                .SingleOrDefaultAsync(cancellationToken);

    public ValueTask<SmsTemplate> CreateAsync(SmsTemplate smsTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = smsTemplateValidator.Validate(smsTemplate);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return smsTemplateRepository.CreateAsync(smsTemplate, saveChanges, cancellationToken);
    }
}