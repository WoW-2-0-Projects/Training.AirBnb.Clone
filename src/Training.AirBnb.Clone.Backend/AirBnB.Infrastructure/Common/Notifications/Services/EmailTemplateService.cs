using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Extensions;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

public class EmailTemplateService(IEmailTemplateRepository emailTemplateRepository, IValidator<EmailTemplate> emailTemplateValidator):IEmailTemplateService
{
    public ValueTask<IList<EmailTemplate>> GetAsync(
        QuerySpecification<EmailTemplate> querySpecification,
        CancellationToken cancellationToken = default) =>
        emailTemplateRepository.GetAsync(querySpecification, cancellationToken);

    public ValueTask<IList<EmailTemplate>> GetAsync(
        QuerySpecification querySpecification,
        CancellationToken cancellationToken = default) =>
        emailTemplateRepository.GetAsync(querySpecification, cancellationToken);
    

    public async ValueTask<EmailTemplate?> GetByTypeAsync(NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
            await emailTemplateRepository.Get(emailTemplate => emailTemplate.TemplateType == templateType, asNoTracking)
            .SingleOrDefaultAsync(cancellationToken); 

    public ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var validationResult = emailTemplateValidator.Validate(emailTemplate);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return emailTemplateRepository.CreateAsync(emailTemplate, saveChanges, cancellationToken);
    }
}