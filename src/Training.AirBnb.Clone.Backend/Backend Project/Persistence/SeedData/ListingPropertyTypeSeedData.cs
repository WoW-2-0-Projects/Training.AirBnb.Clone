using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Enums;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class ListingPropertyTypeSeedData
{
    public static async ValueTask InitializeListingPropertySeedData(this IDataContext context)
    {
        if (!context.ListingPropertyTypes.Any())
            await context.InitializeListingProperty(1000);
    }

    private static async ValueTask InitializeListingProperty(this IDataContext context, int count)
    {
        var listingPropertyTypes = new List<ListingPropertyType>();

        var random = new Random();

        for (int index = 0; index < count; index++)
        {
            var floorsCount = random.Next(1, 180);
            var categoryTypes = context.ListingCategoryTypes.ToList()[random.Next(context.ListingCategoryTypes.Count())];

            listingPropertyTypes.Add(new ListingPropertyType()
            {
                CategoryId = categoryTypes.ListingCategoryId,
                TypeId = categoryTypes.ListingTypeId,
                FloorsCount = floorsCount,
                ListingFloor = random.Next(1, floorsCount),
                YearBuilt = random.Next(1900, DateTime.UtcNow.Year),
                PropertySize = random.Next(1, 10_000),
                UnitOfSize = floorsCount % 2 == 0 ? UnitsOfSize.SquareMetres : UnitsOfSize.SquareFeet
            });
        }

        await context.ListingPropertyTypes.AddRangeAsync(listingPropertyTypes);
        await context.SaveChangesAsync();
    }
}