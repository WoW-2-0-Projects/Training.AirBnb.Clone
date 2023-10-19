using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class ListingPropertyTypeSeedData
{
    public static async ValueTask InitializeListingProperyTypeSeedData(this IDataContext context)
    {
        if (!context.ListingPropertyTypes.Any())
            await context.InitializeListingProperyType();
    }

    private static async ValueTask InitializeListingProperyType(this IDataContext context)
    {
        var listingPropertyTypes = new List<ListingPropertyType>();

        var listingCategories = context.ListingCategories.ToList();
        var listingTypes = context.ListingTypes.ToList();

        var random = new Random();

        for(int index = 0; index < 1000; index++)
        {
            listingPropertyTypes.Add(new ListingPropertyType()
            {
               // CategoryId = listingCategories[random.Next(0, listingCategories.Count())],
               // TypeId = listingCategoryTypes[random.Next(0, context.ListingTypes.)]

            });
        }

        await context.ListingPropertyTypes.AddRangeAsync(listingPropertyTypes);
        await context.SaveChangesAsync();
    }
}
