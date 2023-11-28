using AirBnB.Application.Common.Models.Querying;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Application.Common.Querying.Extension;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Infrastructure.Common.Validators;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

/// <summary>
/// created crud methods for emailTemplate
/// </summary>
public class EmailTemplateService(IEmailTemplateRepository emailTemplateRepository, 
    IValidator<EmailTemplate> emailTemplateValidator) : IEmailTemplateService
{
    public async ValueTask<IList<EmailTemplate>> GetByFilterAsync(
        FilterPagination pagination, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) => 
            await emailTemplateRepository.Get(asNoTracking:  asNoTracking)
            .ApplyPagination(pagination)
            .ToListAsync(cancellationToken);

    public async ValueTask<EmailTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default) => 
            await emailTemplateRepository.Get(template => template.TemplateType == templateType, asNoTracking)
            .SingleOrDefaultAsync(cancellationToken);

    public ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate template, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = emailTemplateValidator.Validate(template);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return emailTemplateRepository.CreateAsync(template, saveChanges, cancellationToken);
    }
}