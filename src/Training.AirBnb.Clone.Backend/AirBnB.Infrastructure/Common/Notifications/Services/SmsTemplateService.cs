using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.Notification;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

public class SmsTemplateService(ISmsTemplateRepository smsTemplateRepository, IValidator<SmsTemplate> validator) : ISmsTemplateService
{
    public ValueTask<IList<SmsTemplate>> GetAsync(
        QuerySpecification<SmsTemplate> querySpecification,
        CancellationToken cancellationToken = default) =>
        smsTemplateRepository.GetAsync(querySpecification, cancellationToken);

    public ValueTask<IList<SmsTemplate>> GetAsync(
        QuerySpecification querySpecification,
        CancellationToken cancellationToken = default) =>
        smsTemplateRepository.GetAsync(querySpecification, cancellationToken);

    public async ValueTask<SmsTemplate?> GetByTypeAsync(NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        await smsTemplateRepository.Get(template => template.TemplateType == templateType, asNoTracking)
            .SingleOrDefaultAsync(cancellationToken);

    public ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(smsTemplate);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return smsTemplateRepository.CreateAsync(smsTemplate, saveChanges, cancellationToken);
    }
}