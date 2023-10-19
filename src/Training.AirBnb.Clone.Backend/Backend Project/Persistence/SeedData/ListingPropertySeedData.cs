using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Enums;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class ListingPropertySeedData
{
    public static async ValueTask InitializeListingPropertySeedData(this IDataContext context)
    {
        if (!context.ListingPropertyTypes.Any())
            await context.InitializeListingProperty();
    }

    private static async ValueTask InitializeListingProperty(this IDataContext context)
    {
        var listingPropertyTypes = new List<ListingPropertyType>();

        var random = new Random();

        for (int index = 0; index < 1000; index++)
        {
            var floorsCount = random.Next(1, 180);

            listingPropertyTypes.Add(new ListingPropertyType()
            {
                CategoryId = context.ListingCategoryTypes.ToList()[random.Next(0, context.ListingCategoryTypes
                    .ToList().Count())].ListingCategoryId,
                TypeId = context.ListingCategoryTypes.ToList()[random.Next(0, context.ListingCategoryTypes
                    .ToList().Count())].ListingTypeId,
                FloorsCount = floorsCount,
                ListingFloor = random.Next(1, floorsCount),
                YearBuilt = random.Next(1900, DateTime.UtcNow.Year),
                PropertySize = random.Next(1, 1_000_000_000),
                UnitOfSize = floorsCount % 2 == 0 ? UnitsOfSize.SquareMetres : UnitsOfSize.SquareFeet
            });
        }
        await context.ListingPropertyTypes.AddRangeAsync(listingPropertyTypes);
        await context.SaveChangesAsync();
    }
}