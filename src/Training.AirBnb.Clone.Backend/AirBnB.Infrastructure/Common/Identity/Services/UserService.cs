using System.Linq.Expressions;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.Identity;
using AirBnB.Domain.Enums;
using AirBnB.Infrastructure.Common.Validators;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Service for managing user-related operations.
/// </summary>
public class UserService(IUserRepository userRepository, UserValidator userValidator) : IUserService
{
    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default,
        bool asNoTracking = false
    ) =>
        userRepository.Get(predicate, asNoTracking);

    public ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);

    public  ValueTask<IList<User>> GetAsync(
        QuerySpecification<User> querySpecification,
        CancellationToken cancellationToken = default
    ) => 
         userRepository.GetAsync(querySpecification, cancellationToken);

    public ValueTask<IList<User>> GetAsync(
        QuerySpecification querySpecification,
        CancellationToken cancellationToken = default
    ) => userRepository.GetAsync(querySpecification, cancellationToken); 
    

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        userRepository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<User> CreateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var validationResult = userValidator
            .Validate(user,
                options =>
                    options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> UpdateAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    ) =>
        userRepository.UpdateAsync(user, saveChanges, cancellationToken);

    public ValueTask<User?> DeleteByIdAsync(
        Guid userId,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
        =>
            userRepository.DeleteByIdAsync(userId, saveChanges, cancellationToken);

    public ValueTask<User?> DeleteAsync(
        User user,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    ) =>
        userRepository.DeleteAsync(user, saveChanges, cancellationToken);
}