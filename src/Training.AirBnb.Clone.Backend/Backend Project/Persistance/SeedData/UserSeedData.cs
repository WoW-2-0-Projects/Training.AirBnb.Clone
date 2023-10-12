using Backend_Project.Domain.Common;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Enums;
using Backend_Project.Persistance.DataContexts;
using Bogus;

namespace Backend_Project.Persistance.SeedData;

public static class UserSeedData
{
    public static async ValueTask InitializeSeedDataAsync(this IDataContext fileContext)
    {
        if (!fileContext.Users.Any())
        {
            var admin = fileContext.GetUserSystem();
            await fileContext.Users.AddAsync(admin);
            var userCredential = new UserCredentials { CreatedDate = DateTime.Now, UserId = admin.Id, Password = "Aa1234!@a" };
            await fileContext.UserCredentials.AddAsync(userCredential);
            await fileContext.SaveChangesAsync();
        }


        if (!fileContext.Users.Any())
        {
            await fileContext.AddAsync<User>(100);   
        }

        if (!fileContext.UserCredentials.Any())
        {
            await fileContext.AddAsync<UserCredentials>(100);
        }
    }
    public static User GetUserSystem(this IDataContext fileContext)
    {
        if (!fileContext.Users.Any())
        {
            return new User { Id = Guid.NewGuid(), FirstName = "System", LastName = "Project", UserRole = UserRole.Admin, EmailAddress = "sultonbek.rakhimov.recovery@gmail.com" };
        }
        return fileContext.Users.First(user => user.EmailAddress.Equals("sultonbek.rakhimov.recovery@gmail.com"));
    }

     public static async ValueTask AddAsync<TEntity>(this IDataContext context, int count) where TEntity : IEntity
    {
        var task = typeof(TEntity) switch
        {
            { } t when t == typeof(User) => context.AddUsersAsync(count),
            { } t when t == typeof(UserCredentials) => context.AddUserCredentials(count),
            _ => new ValueTask(Task.CompletedTask)
        };
    }
    public static async ValueTask AddUsersAsync(this IDataContext context, int count)
    {
        var faker = GetUserFaker(context);
        var uniqueUsers = new HashSet<User>(faker.Generate(100_000));
        var test = uniqueUsers.Take(count);
        await context.Users.AddRangeAsync(uniqueUsers.Take(count).ToList());
    }
    public static async ValueTask AddUserCredentials(this IDataContext context, int count)
    {
        var faker = GetUserCredentialsFaker(context);
        var userCredentials = faker.Generate(context.Users.Count());
        await context.UserCredentials.AddRangeAsync(userCredentials.Take(count).ToList());
    }
    public static Faker<User> GetUserFaker(IDataContext context)
    {
        return new Faker<User>()
            .RuleFor(keySelector => keySelector.Id, Guid.NewGuid)
            .RuleFor(keySelector => keySelector.FirstName, source => source.Person.FirstName)
            .RuleFor(keySelector => keySelector.LastName, source => source.Person.LastName)
            .RuleFor(keySelector => keySelector.EmailAddress, source => source.Person.Email);
    }

    public static Faker<UserCredentials> GetUserCredentialsFaker(IDataContext context)
    {
        var usersId = new Stack<Guid>(context.Users.Select(user => user.Id));
        return new Faker<UserCredentials>()
            .RuleFor(keySelector => keySelector.Id, Guid.NewGuid)
            .RuleFor(keySelector => keySelector.UserId, () => usersId.Pop())
            .RuleFor(keySelector => keySelector.Password, source => source.Internet.Password(8, false, "", "1234567890"));
    }
}