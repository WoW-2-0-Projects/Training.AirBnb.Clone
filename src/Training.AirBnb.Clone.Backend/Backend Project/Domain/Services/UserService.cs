
using Backend_Project.Domain.Entities;
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

    public async ValueTask<User> CreateAsync(User entity, bool saveChanges = true)
    {
        if (!(await _validationService.IsValidName(entity.FirstName)))
            throw new FormatException("Invalid first name");
        if (!(await _validationService.IsValidName(entity.FirstName)))
            throw new FormatException("Invalid last name");
        if (!_validationService.IsValidEmailAddress(entity.EmailAddress))
            throw new FormatException("Invalid email address");
        if (!(await IsUnique(entity.EmailAddress)))
            throw new ArgumentException("This email address already exists");
        await _appDataContext.Users.AddAsync(entity);

        if (saveChanges)
            await _appDataContext.SaveChangesAsync();

        return entity;
    }

    public async ValueTask<User> DeleteAsync(Guid id, bool saveChanges = true)
    {
        var deletedUser = await GetById(id);
        if (deletedUser is null)
            throw new InvalidOperationException("User not found");
        await _appDataContext.Users.RemoveAsync(deletedUser);
        deletedUser.DeletedDate = DateTimeOffset.UtcNow;
        deletedUser.IsDeleted = true;
        if (saveChanges)
            await _appDataContext.SaveChangesAsync();
        return deletedUser;

    }

    public async ValueTask<User> DeleteAsync(User entity, bool saveChanges = true)
    {

        var deletedUser = await GetById(entity.Id);
        if (deletedUser is null)
            throw new InvalidOperationException("User not found");
        await _appDataContext.Users.RemoveAsync(deletedUser);
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
            throw new ArgumentNullException("User not found"));
    }

    public async ValueTask<User> UpdateAsync(User entity, bool saveChanges = true)
    {
        var updatedUser = await GetById(entity.Id);

        if (updatedUser is null)
            throw new InvalidOperationException("User not found");

        updatedUser.FirstName = entity.FirstName;
        updatedUser.LastName = entity.LastName;
        updatedUser.EmailAddress = entity.EmailAddress;
        updatedUser.ModifiedDate = DateTimeOffset.UtcNow;
        if(saveChanges)
            await _appDataContext.SaveChangesAsync();
        return updatedUser;
    }
    private ValueTask<bool> IsUnique(string email) =>
         new ValueTask<bool>(!_appDataContext.Users
             .Any(user => !user.IsDeleted && user.EmailAddress.Equals(email)));
}
