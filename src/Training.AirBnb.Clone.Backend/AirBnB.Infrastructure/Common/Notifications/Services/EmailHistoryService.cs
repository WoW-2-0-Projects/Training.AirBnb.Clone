using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

public class EmailHistoryService(
    IEmailHistoryRepository emailHistoryRepository,
    IValidator<EmailHistory> emailHistoryValidator) : IEmailHistoryService
{
    public IQueryable<EmailHistory> Get(Expression<Func<EmailHistory, bool>>? predicate = null, bool asNoTracking = false)
        => emailHistoryRepository.Get(predicate, asNoTracking);

    public ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = emailHistoryValidator.Validate(emailHistory,
            options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return emailHistoryRepository.CreateAsync(emailHistory, saveChanges, cancellationToken);
    }
}
