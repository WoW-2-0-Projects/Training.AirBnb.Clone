
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.User;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services;

public class UserService : IEntityBaseService<User>
{
    private readonly IDataContext _appDataContext;
    private readonly IValidationService _validationService;
    public UserService(IDataContext appDataContext, IValidationService validationService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
    }

    public async ValueTask<User> CreateAsync(User user, bool saveChanges = true)
    {
        if (!(await _validationService.IsValidName(user.FirstName)))
            throw new UserFormatException("Invalid first name");
        if (!(await _validationService.IsValidName(user.LastName)))
            throw new UserFormatException("Invalid last name");
        if (!_validationService.IsValidEmailAddress(user.EmailAddress))
            throw new UserFormatException("Invalid email address");
        if (!(await IsUnique(user.EmailAddress)))
            throw new UserAlreadyExistsException("This email address already exists");
        await _appDataContext.Users.AddAsync(user);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return user;
    }

    public async ValueTask<User> DeleteAsync(Guid id, bool saveChanges = true)
    {
        var deletedUser = await GetById(id);
        if (deletedUser is null)
            throw new UserNotFoundException("User not found");
        deletedUser.DeletedDate = DateTimeOffset.UtcNow;
        deletedUser.IsDeleted = true;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return deletedUser;

    }

    public async ValueTask<User> DeleteAsync(User user, bool saveChanges = true)
    {

        var deletedUser = await GetById(user.Id);
        if (deletedUser is null)
            throw new UserNotFoundException("User not found");
        deletedUser.DeletedDate = DateTimeOffset.UtcNow;
        deletedUser.IsDeleted = true;
        if(saveChanges)
            await _appDataContext.SaveChangesAsync();
        return deletedUser;
    }

    public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
    {
        return _appDataContext.Users.Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<ICollection<User>> Get(IEnumerable<Guid> ids)
    {
        var users = _appDataContext.Users
            .Where(user => ids.Contains(user.Id));
        return new ValueTask<ICollection<User>>(users.ToList());
    }

    public ValueTask<User> GetById(Guid id)
    {
        return new ValueTask<User>(_appDataContext.Users.
            FirstOrDefault(user => user.Id == id && !user.IsDeleted) ?? 
            throw new UserNotFoundException("User not found"));
    }

    public async ValueTask<User> UpdateAsync(User user, bool saveChanges = true)
    {
        var updatedUser = await GetById(user.Id);

        if (updatedUser is null)
            throw new UserNotFoundException("User not found");
        if (!(await _validationService.IsValidName(user.FirstName)))
            throw new UserFormatException("Invalid first name");
        if (!(await _validationService.IsValidName(user.LastName)))
            throw new UserFormatException("Invalid last name");

        updatedUser.FirstName = user.FirstName;
        updatedUser.LastName = user.LastName;
        updatedUser.ModifiedDate = DateTimeOffset.UtcNow;
        updatedUser.PhoneNumberId = user.PhoneNumberId;
        updatedUser.IsActive = false;
        if(saveChanges)
            await _appDataContext.SaveChangesAsync();
        return updatedUser;
    }
    private ValueTask<bool> IsUnique(string email) =>
         new ValueTask<bool>(!_appDataContext.Users
             .Any(user => !user.IsDeleted && user.EmailAddress.Equals(email)));
}
