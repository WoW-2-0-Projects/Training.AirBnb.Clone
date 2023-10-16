using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;
using FileBaseContext.Abstractions.Models.FileSet;

namespace Backend_Project.Persistence.SeedData;

public static class AmenityAndAmenityCategorySeedData
{
    public static async ValueTask InitializeAmenityAndAmenityCategorySeedData(this IDataContext context)
    {
        if (!context.AmenityCategories.Any())
            await context.InitializeAmenityCategory();

        if (!context.Amenities.Any())
            await context.InitializeAmenitySeedData();
    }

    private static async ValueTask InitializeAmenityCategory(this IDataContext context)
    {
        var amenitiesCategory = new List<AmenityCategory>
        {
            new AmenityCategory { CategoryName = "Bedroom and laundry"},
            new AmenityCategory { CategoryName = "Entertainment"},
            new AmenityCategory { CategoryName = "Family"},
            new AmenityCategory { CategoryName = "Heating and cooling"},
            new AmenityCategory { CategoryName = "Home safety"},
            new AmenityCategory { CategoryName = "Internet and office"},
            new AmenityCategory { CategoryName = "Kitchen and dining"},
            new AmenityCategory { CategoryName = "Location features"},
            new AmenityCategory { CategoryName = "Outdoor"},
            new AmenityCategory { CategoryName = "Services"},
            new AmenityCategory { CategoryName = "Parking and facilities"},
            new AmenityCategory { CategoryName = "Bathroom"}
        };

        await context.AmenityCategories.AddRangeAsync(amenitiesCategory);
        await context.SaveChangesAsync();
    }

    private static async ValueTask InitializeAmenitySeedData(this IDataContext context)
    {
        List<List<string>> amenities = new List<List<string>>
        {
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""},
            new List<string> {""}
        };

        var amenityCategoryId = GetCategoriesIds(context.AmenityCategories.ToList());

        for (int i = 0; i < amenities.Count; i++)
        {
            var addAmenities = await GetAmenityByCategoryId(amenityCategoryId[i], amenities[i]);
            
            await context.Amenities.AddRangeAsync(addAmenities);
            await context.SaveChangesAsync();
        }
    }

    private static ValueTask<List<Amenity>> GetAmenityByCategoryId(Guid categoryId, List<string> amenities)
    {
        List<Amenity> addAmenity = new List<Amenity>();

        foreach (var amenity in amenities)
            addAmenity.Add(new Amenity { AmenityName = amenity, CategoryId = categoryId });

        return new ValueTask<List<Amenity>>(addAmenity);

    }

    private static List<Guid> GetCategoriesIds(List<AmenityCategory> amenityCategories)
        => amenityCategories.Select(category => category.Id).ToList();
}