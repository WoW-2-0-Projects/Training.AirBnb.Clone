using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.DataContexts;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Data;

/// <summary>
/// Extension methods for initializing seed data in the application.
/// </summary>
public static class SeedDataExtensions
{
    /// <summary>
    /// Initializes seed data in the IdentityDbContext by checking for existing users and seeding them if necessary.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
    /// <returns>An asynchronous task representing the initialization process.</returns>
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var identityDbContext = serviceProvider.GetRequiredService<IdentityDbContext>();

        if (!await identityDbContext.Users.AnyAsync())
            await identityDbContext.SeedUsersAsync();
        
    }

    /// <summary>
    /// Seeds user data into the IdentityDbContext using Bogus library.
    /// </summary>
    /// <param name="dbContext">The IdentityDbContext instance to seed data into.</param>
    /// <returns>An asynchronous task representing the seeding process.</returns>
    private static async ValueTask SeedUsersAsync(this IdentityDbContext dbContext)
    {
        var userFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.Password, data => data.Internet.Password(8))
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone);

        await dbContext.AddRangeAsync(userFaker.Generate(100));
        await dbContext.SaveChangesAsync();
    }
}