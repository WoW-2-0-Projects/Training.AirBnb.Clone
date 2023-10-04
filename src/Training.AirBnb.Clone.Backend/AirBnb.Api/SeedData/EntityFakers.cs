using Backend_Project.Domain.Entities;
using Backend_Project.Persistance.DataContexts;
using Bogus;

namespace Backend_Project.SeedDate;

public static class EntityFakers
{
    internal static Faker<User> GetUserFaker(IDataContext context)
    {
        return new Faker<User>()
            .RuleFor(keySelector => keySelector.Id, Guid.NewGuid)
            .RuleFor(keySelector => keySelector.FirstName, source => source.Person.FirstName)
            .RuleFor(keySelector => keySelector.LastName, source => source.Person.LastName)
            .RuleFor(keySelector => keySelector.EmailAddress, source => source.Person.Email);
    }
}
