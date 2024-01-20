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
            await appDbContext.SeedListingsAsync();

        if (!await appDbContext.GuestFeedbacks.AnyAsync())
            await appDbContext.SeedGuestFeedbacksAsync();
    }

    /// <summary>
    /// Seeds the database with initial roles.
    /// </summary>
    /// <param name="dbContext"></param>
    private static async ValueTask SeedRolesAsync(this AppDbContext dbContext)
    {
        var roles = new List<Role>()
        {
            new Role
            {
                Id = Guid.Parse("7700e8af-6e37-4448-9409-8d9d03911732"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.System
            },
            new Role
            {
                Id = Guid.Parse("8346abd3-ec6e-4be4-9e17-784733a9e269"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.Admin
            },
            new Role
            {
                Id = Guid.Parse("22acb325-9a85-4ccd-afde-bbfcdd4ae53c"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.Guest
            },
            new Role
            {
                Id = Guid.Parse("a42302e1-ffa0-490b-8398-d4323bb3a9e4"),
                CreatedTime = DateTimeOffset.UtcNow,
                Type = RoleType.Host
            }
        };

        await dbContext.Roles.AddRangeAsync(roles);
        dbContext.SaveChanges();
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
        dbContext.SaveChanges();
    }

    /// <summary>
    /// Seeds listing categories into the AppDbContext from a JSON file.
    /// </summary>
    /// <param name="appDbContext"></param>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    private static async ValueTask SeedListingCategoriesAsync(
        this AppDbContext appDbContext,
        IHostEnvironment webHostEnvironment)
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
        appDbContext.SaveChanges();
    }

    /// <summary>
    /// Seeds the listings data into the AppDbContext using Bogus library.
    /// </summary>
    /// <param name="appDbContext"></param>
    private static async ValueTask SeedListingsAsync(this AppDbContext appDbContext)
    {
        // get existing hosts
        var hosts = appDbContext.Users
            .Where(user => user.Role.Type == RoleType.Host)
            .Select(host => host.Id);

        // generate fake addresses
        var addressFaker = new Faker<Address>()
            .RuleFor(address => address.City, data => data.Address.City())
            .RuleFor(address => address.CityId, Guid.NewGuid())
            .RuleFor(address => address.Latitude, data => data.Address.Latitude())
            .RuleFor(address => address.Longitude, data => data.Address.Longitude());

        var addresses = addressFaker.Generate(100).ToHashSet();

        // generate fake money
        var moneyFaker = new Faker<Money>()
            .RuleFor(money => money.Amount, data => data.Random.Decimal(10, 10_000))
            .RuleFor(money => money.Currency, Currency.USD);

        var money = moneyFaker.Generate(100).ToHashSet();

        // generate fake listings
        var listingsFaker = new Faker<Listing>()
            .RuleFor(listing => listing.Name, data => data.Lorem.Word())
            .RuleFor(listing => listing.BuiltDate, data => data.Date.PastDateOnly(100))
            .RuleFor(listing => listing.Address, data =>
            {
                var chosenAddress = data.PickRandom<Address>(addresses);
                addresses.Remove(chosenAddress);
                return chosenAddress;
            })
            .RuleFor(listing => listing.PricePerNight, data =>
            {
                var chosenMoney = data.PickRandom<Money>(money);
                money.Remove(chosenMoney);
                return chosenMoney;
            })
            .RuleFor(listing => listing.HostId, data => data.PickRandom<Guid>(hosts))
            .RuleFor(listing => listing.CreatedByUserId, (data, listing) => listing.HostId)
            .RuleFor(listing => listing.DeletedByUserId, Guid.Empty)
            .RuleFor(listing => listing.CreatedTime, data => data.Date.PastOffset(5, DateTimeOffset.UtcNow));

        var listings = listingsFaker.Generate(100);

        await appDbContext.Listings.AddRangeAsync(listings);
        appDbContext.SaveChanges();
    }
    
    /// <summary>
    /// Seeds Guest Feedbacks data into the AppDbContext using Bogus library.
    /// </summary>
    /// <param name="dbContext"></param>
    private static async ValueTask SeedGuestFeedbacksAsync(this AppDbContext dbContext)
    {
        var listings = dbContext.Listings.AsQueryable();

        var feedbackFaker = new Faker<GuestFeedback>()
            .RuleFor(feedback => feedback.Communication, data => data.Random.Byte(1, 5))
            .RuleFor(feedback => feedback.Cleanliness, data => data.Random.Byte(1, 5))
            .RuleFor(feedback => feedback.Accuracy, data => data.Random.Byte(1, 5))
            .RuleFor(feedback => feedback.Location, data => data.Random.Byte(1, 5))
            .RuleFor(feedback => feedback.Value, data => data.Random.Byte(1, 5))
            .RuleFor(feedback => feedback.CheckIn, data => data.Random.Byte(1, 5))
            .RuleFor(feedback => feedback.Comment, data => data.Lorem.Paragraph())
            .RuleFor(feedback => feedback.CreatedTime, data => data.Date.PastOffset(5, DateTimeOffset.UtcNow))
            .RuleFor(feedback => feedback.Listing, data => data.PickRandom<Listing>(listings))
            .RuleFor(feedback => feedback.GuestId, (data, feedback) =>
            {
                var listingOwnerId = feedback.Listing.HostId;
                
                return data.PickRandom(dbContext.Users.Select(user => user.Id)
                    .Where(userId => userId != listingOwnerId).ToList());
            })
            .RuleFor(feedback => feedback.OverallRating, (data, feedback) => 
                (feedback.Accuracy + feedback.Value + feedback.Cleanliness + 
                 feedback.Communication + feedback.Location + feedback.CheckIn) / 6.0);

        var guestFeedbacks = feedbackFaker.Generate(100);
        
        await dbContext.GuestFeedbacks.AddRangeAsync(guestFeedbacks);
        dbContext.SaveChanges();
    }
}