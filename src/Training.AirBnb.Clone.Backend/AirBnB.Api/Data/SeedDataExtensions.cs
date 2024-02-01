using AirBnB.Domain.Brokers;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.DataContexts;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AirBnB.Api.Data;

/// <summary>
/// Extension methods for initializing seed data in the application.
/// </summary>
public static class SeedDataExtensions
{
    /// <summary>
    /// Initializes seed data in the AppDbContext by checking for existing users and seeding them if necessary.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
    /// <returns>An asynchronous task representing the initialization process.</returns>
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (!await appDbContext.Roles.AnyAsync())
            await appDbContext.SeedRolesAsync();

        if (!await appDbContext.Users.AnyAsync())
            await appDbContext.SeedUsersAsync();

        if (!await appDbContext.ListingCategories.AnyAsync())
            await appDbContext.SeedListingCategoriesAsync(webHostEnvironment);

        if (!await appDbContext.Listings.AnyAsync())
            await appDbContext.SeedListingsAsync(webHostEnvironment);

        // check change tracker and if changes exist, save changes to database
        if (appDbContext.ChangeTracker.HasChanges())
            await appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Seeds the database with initial roles.
    /// </summary>
    /// <param name="dbContext"></param>
    private static async ValueTask SeedRolesAsync(this AppDbContext dbContext)
    {
        var roles = new List<Role>
        {
            new()
            {
                Id = Guid.Parse("7700e8af-6e37-4448-9409-8d9d03911732"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.System
            },
            new()
            {
                Id = Guid.Parse("8346abd3-ec6e-4be4-9e17-784733a9e269"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.Admin
            },
            new()
            {
                Id = Guid.Parse("22acb325-9a85-4ccd-afde-bbfcdd4ae53c"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.Guest
            },
            new()
            {
                Id = Guid.Parse("a42302e1-ffa0-490b-8398-d4323bb3a9e4"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.Host
            }
        };

        await dbContext.Roles.AddRangeAsync(roles);
        await dbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Seeds user data into the AppDbContext using Bogus library.
    /// </summary>
    /// <param name="dbContext">The AppDbContext instance to seed data into.</param>
    /// <returns>An asynchronous task representing the seeding process.</returns>
    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext)
    {
        // Add system user.
        var systemRoleId = dbContext.Roles.First(role => role.Type == RoleType.System).Id;

        var systemUser = new User
        {
            Id = Guid.Parse("7dead347-e459-4c4a-85b0-8f1b373d3dec"),
            RoleId = systemRoleId,
            FirstName = "System",
            LastName = "System",
            EmailAddress = "example@gmail.com",
            PasswordHash = "A1rBnB.$com",
            PhoneNumber = ""
        };

        await dbContext.Users.AddAsync(systemUser);
        
        // Add Hosts
        var hostRoleId = dbContext.Roles.First(role => role.Type == RoleType.Host).Id;

        var hostFaker = new Faker<User>()
            .RuleFor(user => user.RoleId, () => hostRoleId)
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PasswordHash, data => data.Internet.Password(8))
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone);

        await dbContext.AddRangeAsync(hostFaker.Generate(100));
        
        // Add guests.
        var guestRoleId = dbContext.Roles.First(role => role.Type == RoleType.Guest).Id;

        var guestFaker = new Faker<User>()
            .RuleFor(user => user.RoleId, () => guestRoleId)
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PasswordHash, data => data.Internet.Password(8))
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone);

        await dbContext.AddRangeAsync(guestFaker.Generate(100));
       
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Seeds listing categories into the AppDbContext from a JSON file.
    /// </summary>
    /// <param name="appDbContext"></param>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    private static async ValueTask SeedListingCategoriesAsync(this AppDbContext appDbContext, IHostEnvironment webHostEnvironment)
    {
        var listingCategoriesFileName = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "SeedData", "ListingCategories.json");

        // Retrieve listing categories
        var listingCategories = JsonConvert.DeserializeObject<List<ListingCategory>>(await File.ReadAllTextAsync(listingCategoriesFileName))!;

        // Set category images
        listingCategories.ForEach(
            listingCategory => listingCategory.ImageStorageFile = new StorageFile
            {
                Id = listingCategory.StorageFileId,
                FileName = $"{listingCategory.StorageFileId}.jpg",
                Type = StorageFileType.ListingCategoryImage
            }
        );

        await appDbContext.ListingCategories.AddRangeAsync(listingCategories);
    }

    /// <summary>
    /// Seeds listings into the AppDbContext from a JSON file.
    /// </summary>
    /// <param name="appDbContext"></param>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    private static async ValueTask SeedListingsAsync(this AppDbContext appDbContext, IHostEnvironment webHostEnvironment)
    {
        var randomDateTimeProvider = new RandomDateTimeProvider();
        var listingsFileName = Path.Combine(webHostEnvironment.ContentRootPath, "Data", "SeedData", "Listings.json");
        var users = await appDbContext.Users.Include(user => user.Role).Where(user => user.Role!.Type == RoleType.Host).ToListAsync();
        var listingCategories = appDbContext.ListingCategories.Local.ToList();

        // Retrieve listings
        var listings = JsonConvert.DeserializeObject<List<Listing>>(await File.ReadAllTextAsync(listingsFileName))!;

        var hostCounter = 0;

        // Update listings
        listings.ForEach(
            listing =>
            {
                // Add random built date
                listing.BuiltDate = DateOnly.FromDateTime(randomDateTimeProvider.Generate(new DateTime(1950, 1, 1), DateTime.Now.AddYears(-3)));

                // Add host
                listing.HostId = users[hostCounter].Id;
                listing.CreatedByUserId = users[hostCounter].Id;

                // Fix listing category relationship
                listing.ListingCategories = listing.ListingCategories.Select(
                        selectedCategory => listingCategories.First(listingCategory => listingCategory.Id == selectedCategory.Id)
                    )
                    .ToList();

                hostCounter = (hostCounter + 1) % users.Count;
            }
        );

        await appDbContext.Listings.AddRangeAsync(listings);
        await appDbContext.SaveChangesAsync();
    }
}