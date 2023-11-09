using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;
using Bogus;

namespace Backend_Project.Persistence.SeedData;

public static class ListingDescriptionSeedData
{
    public static async ValueTask InitializeListsingDescriptionSeedData(this IDataContext context)
    {
        if (!context.Descriptions.Any())
            await context.AddListingDescriptionSeedData(1000);
    }

    private static async ValueTask AddListingDescriptionSeedData(this IDataContext context, int count)
    {
        var faker = new Faker();

        for(var i = 0; i < count; i++)
        {
            var description = new Description
            {
                ListingDescription = faker.Lorem.Sentence(),
                TheSpace = faker.Random.Bool() ? faker.Lorem.Paragraph() : null,
                GuestAccess = faker.Random.Bool() ? faker.Lorem.Paragraph() : null,
                OtherDetails = faker.Random.Bool() ? faker.Lorem.Paragraph() : null,
                InteractionWithGuests = faker.Random.Bool() ? faker.Lorem.Paragraph() : null
            };

            await context.Descriptions.AddAsync(description);
        }

        await context.SaveChangesAsync();
    }
}
