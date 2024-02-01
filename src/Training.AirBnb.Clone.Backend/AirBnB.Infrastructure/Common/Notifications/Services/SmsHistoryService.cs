using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

public class SmsHistoryService(
    ISmsHistoryRepository smsHistoryRepository,
    IValidator<SmsHistory> smsHistoryValidator
    ) : ISmsHistoryService
{
    public IQueryable<SmsHistory> Get(Expression<Func<SmsHistory, bool>>? predicate = null, bool asNoTracking = false)
        => smsHistoryRepository.Get(predicate, asNoTracking);

    public ValueTask<SmsHistory> CreateAsync(SmsHistory smsHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = smsHistoryValidator.Validate(smsHistory,
            options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return smsHistoryRepository.CreateAsync(smsHistory, saveChanges, cancellationToken);
    }
}
