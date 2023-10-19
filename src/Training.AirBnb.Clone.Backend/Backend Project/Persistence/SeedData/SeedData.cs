using Backend_Project.Domain.Common;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Enums;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class SeedData
{
    public static async ValueTask InitializeSeedDataAsync(this IDataContext context)
    {
        if (!context.ListingPropertyTypes.Any())
            await context.AddSeedDataAsync<ListingPropertyType>(1000);
    }

    private static async ValueTask AddSeedDataAsync<TEntity>(this IDataContext context, int count = 0) where TEntity : IEntity
    {
        var task = typeof(TEntity) switch
        {
            { } type when type == typeof(ListingPropertyType) => context.AddListingProperyType(count),
            _ => new ValueTask(Task.CompletedTask)
        };

        await task;
        await context.SaveChangesAsync();
    }

    private static async ValueTask AddListingProperyType(this IDataContext context, int count)
    {
        var listingPropertyTypes = new List<ListingPropertyType>();

        var random = new Random();

        for (int index = 0; index < count; index++)
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
    }
}