using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class CategoryDetailsSeedData
{
    public static async ValueTask InitializeCategoryDetailsSeedData(this IDataContext context)
    {
        if (!context.ListingTypes.Any())
            await context.InitializeListingTypes();

        if (!context.ListingFeatures.Any())
            await context.InitializeListingFeatures();

        if (!context.ListingCategories.Any())
            await context.InitializeCategories();

        if (!context.ListingCategoryTypes.Any())
            await context.InitializeCategoryTypes();
    }

    private static async ValueTask InitializeListingTypes(this IDataContext context)
    {
        var types = new List<ListingType>
        {
            new ListingType {Name = "An entire place", Description = "Guests have the whole place to themselves."},
            new ListingType {Name = "A room", Description = "Guests have their own room in a home, plus access to shared places."},
            new ListingType {Name = "A shared room", Description = "Guests sleep in a room or common area that may be shared with you or others."}
        };

        await context.ListingTypes.AddRangeAsync(types);
        await context.SaveChangesAsync();
    }

    private static async ValueTask InitializeListingFeatures(this IDataContext context)
    {
        var featuresData = new List<string>
        {
            "Bedroom", "Bed", "Full bathroom", "WC", "Full kitchen", "Kitchenette", "Living area", "Dining area", "Office", "Back garden", "Patio",
            "Pool", "Hot tub", "Laundry area", "Garage", "Gym"
        };

        var entirePlaceId = context.ListingTypes.First(type => type.Name == "An entire place").Id;
        var roomId = context.ListingTypes.First(type => type.Name == "A room").Id;
        var sharedRoomId = context.ListingTypes.First(type => type.Name == "A shared room").Id;

        await context.InitializeFeaturesByType(roomId, featuresData, new List<string> { "Bed", "Bedroom" });
        await context.InitializeFeaturesByType(sharedRoomId, featuresData, new List<string> { "Bed", "WC", "Full bathroom" });
        await context.InitializeFeaturesByType(entirePlaceId, featuresData, new List<string> { "Bedroom", "Bed", "WC", "Full bathroom" });
    }

    private static async ValueTask InitializeFeaturesByType(this IDataContext context, Guid typeId, List<string> listingFeatures, List<string> requiredFeatures)
    {
        foreach (var featureName in listingFeatures)
        {
            var feature = new ListingFeature { Name = featureName, MinValue = 0, MaxValue = 50, ListingTypeId = typeId };

            if (requiredFeatures.Contains(featureName))
                feature.MinValue = 1;

            await context.ListingFeatures.AddAsync(feature);
        }

        await context.SaveChangesAsync();
    }

    private static async ValueTask InitializeCategories(this IDataContext context)
    {
        List<string> categoryNames = new List<string>
        {
            "House", "Flat/apartment", "Barn", "Bed & breakfast", "Boat", "Cabin", "Campervan/motorhome", "Casa particular", "Castle", "Cave", "Container",
            "Cycladic home", "Dammuso", "Dome", "Earth home", "Farm", "Guest house", "Hotel", "Houseboat", "Kezhan", "Minsu", "Riad", "Ryokan",
            "Shepherd's hut", "Tent", "Tiny home", "Tower", "Tree house", "Trullo", "Windmill", "Yurt"
        };

        foreach (var name in categoryNames)
        {
            var category = new ListingCategory { Name = name };
            await context.ListingCategories.AddAsync(category);
        }

        await context.SaveChangesAsync();
    }

    private static async ValueTask InitializeCategoryTypes(this IDataContext context)
    {
        var entirePlaceId = context.ListingTypes.First(type => type.Name == "An entire place").Id;
        var roomId = context.ListingTypes.First(type => type.Name == "A room").Id;
        var sharedRoomId = context.ListingTypes.First(type => type.Name == "A shared room").Id;

        var nonEntireCategories = new List<string> { "Bed & breakfast", "Hotel", "Kezhan", "Ryokan" };
        var relations = new List<ListingCategoryType>();

        foreach (var category in context.ListingCategories)
        {
            if (nonEntireCategories.Contains(category.Name))
            {
                relations.Add(new ListingCategoryType { ListingCategoryId = category.Id, ListingTypeId = roomId });
                relations.Add(new ListingCategoryType { ListingCategoryId = category.Id, ListingTypeId = sharedRoomId });
            }
            else
            {
                relations.Add(new ListingCategoryType { ListingCategoryId = category.Id, ListingTypeId = roomId });
                relations.Add(new ListingCategoryType { ListingCategoryId = category.Id, ListingTypeId = sharedRoomId });
                relations.Add(new ListingCategoryType { ListingCategoryId = category.Id, ListingTypeId = entirePlaceId });
            }
        }

        await context.ListingCategoryTypes.AddRangeAsync(relations);
        await context.SaveChangesAsync();
    }
}