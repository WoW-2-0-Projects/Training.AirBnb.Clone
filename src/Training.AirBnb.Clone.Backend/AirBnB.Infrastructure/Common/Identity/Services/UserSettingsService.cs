using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Service for managing userSettings-related operations.
/// </summary>
public class UserSettingsService(
    IUserSettingsRepository userSettingsRepository,
    IValidator<UserSettings> validator) : IUserSettingsService
{
    public ValueTask<UserSettings> CreateAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(userSettings);
        return userSettingsRepository.CreateAsync(userSettings, saveChanges, cancellationToken);
    }

    public ValueTask<UserSettings?> DeleteAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default)
        => userSettingsRepository.DeleteAsync(userSettings, saveChanges, cancellationToken);

    public ValueTask<UserSettings?> DeleteByIdAsync(Guid userSettingsId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => userSettingsRepository.DeleteByIdAsync(userSettingsId, saveChanges, cancellationToken);

    public IQueryable<UserSettings> Get(Expression<Func<UserSettings, bool>>? predicate, bool asNoTracking = false)
        => userSettingsRepository.Get(predicate, asNoTracking);

    public ValueTask<IList<UserSettings>> GetAsync(QuerySpecification<UserSettings> querySpecification, CancellationToken cancellationToken = default)
        => userSettingsRepository.GetAsync(querySpecification, cancellationToken);

    public ValueTask<IList<UserSettings>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default)
        => userSettingsRepository.GetAsync(querySpecification, cancellationToken);

    public ValueTask<UserSettings?> GetByIdAsync(Guid userSettingsId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => userSettingsRepository.GetByIdAsync(userSettingsId, asNoTracking, cancellationToken);

    public ValueTask<IList<UserSettings>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => userSettingsRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<UserSettings> UpdateAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        Validate(userSettings);
        return userSettingsRepository.UpdateAsync(userSettings, saveChanges, cancellationToken);
    }

    private void Validate(UserSettings userSettings)
    {
        var validationResult = validator.Validate(userSettings);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
    }
}
