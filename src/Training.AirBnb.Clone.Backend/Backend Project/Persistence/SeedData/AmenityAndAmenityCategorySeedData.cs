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
            new AmenityCategory { CategoryName = "Bathroom"},
            new AmenityCategory { CategoryName = "Bedroom and laundry"},
            new AmenityCategory { CategoryName = "Entertainment"},
            new AmenityCategory { CategoryName = "Family"},
            new AmenityCategory { CategoryName = "Heating and cooling"},
            new AmenityCategory { CategoryName = "Home safety"},
            new AmenityCategory { CategoryName = "Internet and office"},
            new AmenityCategory { CategoryName = "Kitchen and dining"},
            new AmenityCategory { CategoryName = "Location features"},
            new AmenityCategory { CategoryName = "Outdoor"},
            new AmenityCategory { CategoryName = "Parking and facilities"},
            new AmenityCategory { CategoryName = "Services"}
        };

        await context.AmenityCategories.AddRangeAsync(amenitiesCategory);
        await context.SaveChangesAsync();
    }

    private static async ValueTask InitializeAmenitySeedData(this IDataContext context)
    {
        List<List<string>> amenities = new List<List<string>>
        {
            new List<string> { "Cleaning products", "Body soap", "Bidet", "Bathtub", "Conditioner", "Hair dryer", 
                "Hot water", "Outdoor shower", "Shampoo", "Shower gel" },

            new List<string> { "Essentials", "Bed linens", "Clothing storage", "Dryer", "Drying rack for clothing", 
                "Extra pillows and blankets", "Hangers", "Iron", "Mosquito net", "Room-darkening shades", "Safe", "Washer"},

            new List<string> { "Laser tag", "Life size games", "Mini golf", "Movie theater", "Piano", "Ping pong table",
                "Pool table", "Record player", "Skate ramp", "Sound system", "Theme room", "TV", "Game console",
                "Exercise equipment", "Ethernet connection", "Climbing wall", "Bowling alley", "Books and reading material",
                "Batting cage", "Arcade games"},

            new List<string> { "Children’s books and toys", "Changing table", "Board games", "Babysitter recommendations",
                "Baby safety gates", "Children's playroom", "Children’s bikes", "Baby monitor", "Baby bath", "Children’s dinnerware",
                "Crib", "Fireplace guards", "High chair", "Outdoor playground", "Outlet covers", "Pack ’n play/Travel crib", 
                "Table corner guards", "Window guards" },

            new List<string> { "Portable fans", "Indoor fireplace", "Heating", 
                "Ceiling fan", "Air conditioning" },

            new List<string> { "Smoke alarm", "First aid kit", "Fire extinguisher", 
                "Carbon monoxide alarm" },

            new List<string> {"Dedicated workspace", 
                "Wifi", "Pocket wifi"},

            new List<string> { "Hot water kettle", "Freezer", "Dishwasher", "Dishes and silverware", "Dining table", "Cooking basics",
                "Coffee maker", "Coffee", "Blender", "Bread maker", "Barbecue utensils", "Baking sheet", "Wine glasses",
                "Trash compactor", "Toaster", "Stove", "Rice maker", "Refrigerator", "Oven", "Mini fridge", "Microwave",
                "Kitchenette", "Kitchen"},

            new List<string> { "Waterfront", "Ski-in/Ski-out", "Resort access", "Private entrance", "Laundromat nearby",
                "Lake access", "Beach access"},

            new List<string> { "Sun loungers", "Patio or balcony", "Outdoor kitchen", "Backyard", "Outdoor furniture", "Outdoor dining area",
                "Kayak", "Hammock", "Fire pit", "Boat slip", "Bikes", "Beach essentials", "BBQ grill", },

            new List<string> { "Elevator", "EV charger", "Free parking on premises", "Hockey rink", "Free street parking",
                "Gym", "Hot tub", "Paid parking off premises", "Single level home", "Sauna", "Pool", "Paid parking on premises"},

            new List<string> {"Luggage dropoff allowed", "Breakfast", "Cleaning available during stay",
                "Long term stays allowed"}
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