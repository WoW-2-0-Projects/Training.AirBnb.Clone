using AirBnB.Domain.Entities;
using AirBnB.Persistence.DataContexts;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var identityDbContext = serviceProvider.GetRequiredService<IdentityDbContext>();

        if (!await identityDbContext.Users.AnyAsync())
            await identityDbContext.SeedUsersAsync();
    }

    private static async ValueTask SeedUsersAsync(this IdentityDbContext dbContext)
    {
        var userFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.Password, data => data.Random.Chars('0', 'z', 8).ToString())
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone);

        await dbContext.AddRangeAsync(userFaker.Generate(100));
        await dbContext.SaveChangesAsync();
    }
}