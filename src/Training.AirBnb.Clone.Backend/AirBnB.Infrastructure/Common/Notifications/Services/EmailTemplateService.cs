using System.Linq.Expressions;
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
    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTracking = false)
        => emailTemplateRepository.Get(predicate, asNoTracking);

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