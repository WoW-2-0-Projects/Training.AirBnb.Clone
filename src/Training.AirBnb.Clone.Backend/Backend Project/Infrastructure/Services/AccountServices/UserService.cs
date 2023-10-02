using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.User;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.AccountServices;

public class UserService : IEntityBaseService<User>
{
    private readonly IDataContext _appDataContext;
    private readonly IValidationService _validationService;
    public UserService(IDataContext appDataContext, IValidationService validationService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
    }

    public async ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!_validationService.IsValidNameAsync(user.FirstName))
            throw new UserFormatException("Invalid first name");
        if (!_validationService.IsValidNameAsync(user.LastName))
            throw new UserFormatException("Invalid last name");
        if (!_validationService.IsValidEmailAddress(user.EmailAddress))
            throw new UserFormatException("Invalid email address");
        if (!await IsUnique(user.EmailAddress))
            throw new UserAlreadyExistsException("This email address already exists");
        await _appDataContext.Users.AddAsync(user, cancellationToken);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return user;
    }

    public async ValueTask<User> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var deletedUser = await GetByIdAsync(id);
        if (deletedUser is null)
            throw new UserNotFoundException("User not found");
        deletedUser.DeletedDate = DateTimeOffset.UtcNow;
        deletedUser.IsDeleted = true;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return deletedUser;

    }

    public async ValueTask<User> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {

        var deletedUser = await GetByIdAsync(user.Id);
        if (deletedUser is null)
            throw new UserNotFoundException("User not found");
        deletedUser.DeletedDate = DateTimeOffset.UtcNow;
        deletedUser.IsDeleted = true;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return deletedUser;
    }

    public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
    {
        return GetUndeletedUsers().Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<ICollection<User>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var users = GetUndeletedUsers()
            .Where(user => ids.Contains(user.Id));
        return new ValueTask<ICollection<User>>(users.ToList());
    }

    public ValueTask<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return new ValueTask<User>(GetUndeletedUsers()
            .FirstOrDefault(user => user.Id == id) ??
            throw new UserNotFoundException("User not found"));
    }

    public async ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var updatedUser = await GetByIdAsync(user.Id);
        if (updatedUser is null)
            throw new UserNotFoundException("User not found");
        if (!_validationService.IsValidNameAsync(user.FirstName))
            throw new UserFormatException("Invalid first name");
        if (!_validationService.IsValidNameAsync(user.LastName))
            throw new UserFormatException("Invalid last name");
        if (user.PhoneNumberId == default)
            throw new UserFormatException("Invalid phone number id");

        updatedUser.FirstName = user.FirstName;
        updatedUser.LastName = user.LastName;
        updatedUser.ModifiedDate = DateTimeOffset.UtcNow;
        updatedUser.PhoneNumberId = user.PhoneNumberId;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return updatedUser;
    }
    
    private ValueTask<bool> IsUnique(string email) =>
         new ValueTask<bool>(!GetUndeletedUsers()
             .Any(user => user.EmailAddress.Equals(email)));
    private IQueryable<User> GetUndeletedUsers() =>
        _appDataContext.Users.Where(user => !user.IsDeleted).AsQueryable();
}