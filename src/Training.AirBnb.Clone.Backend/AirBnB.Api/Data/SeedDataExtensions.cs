using AirBnB.Application.Ratings.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Constants;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        var cacheBroker = serviceProvider.GetRequiredService<ICacheBroker>();
        var ratingProcessingService = serviceProvider.GetRequiredService<IRatingProcessingService>();

        var passwordHasherService = serviceProvider.GetRequiredService<IPasswordHasherService>();
        
        if (!await appDbContext.Roles.AnyAsync())
            await appDbContext.SeedRolesAsync();

        if (!await appDbContext.Users.AnyAsync())
            await appDbContext.SeedUsersAsync(passwordHasherService);

        if (!await appDbContext.ListingCategories.AnyAsync())
            await appDbContext.SeedListingCategoriesAsync(webHostEnvironment);

        if (!await appDbContext.Listings.AnyAsync())
            await appDbContext.SeedListingsAsync(webHostEnvironment);

        if (!await appDbContext.GuestFeedbacks.AnyAsync())
            await appDbContext.SeedGuestFeedbacksAsync(cacheBroker, ratingProcessingService);

        if (!await appDbContext.NotificationTemplates.AnyAsync())
            await appDbContext.SeedNotificationTemplatesAsync(webHostEnvironment);

        // check change tracker and if changes exist, save changes to database
        if (appDbContext.ChangeTracker.HasChanges())
            appDbContext.SaveChanges();
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
        dbContext.SaveChanges();
    }
    
    /// <summary>
    /// Seeds user data into the AppDbContext using Bogus library.
    /// </summary>
    /// <param name="dbContext">The AppDbContext instance to seed data into.</param>
    /// <returns>An asynchronous task representing the seeding process.</returns>
    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext, IPasswordHasherService passwordHasherService)
    {
        var roles = dbContext.Roles.ToList();
        
        // Add system user.
        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("7dead347-e459-4c4a-85b0-8f1b373d3dec"),
                FirstName = "System",
                LastName = "System",
                EmailAddress = "example@gmail.com",
                UserCredentials = new UserCredentials("$2a$11$b1aYo5P/Yqgs8voe1WHw3eJBuEGq0eVt15WQxFhNv3al5xBFh7ELu"), // System_1
                PhoneNumber = "",
                Roles = new List<Role>
                {
                    roles.First(role => role.Type == RoleType.System)
                }
            },
            new()
            {
                FirstName = "Admin",
                LastName = "Admin",
                EmailAddress = "admin@gmail.com",
                UserCredentials = new UserCredentials("$2a$11$uUKbkiC9nzwqpAaGN5tV9uLIa8XWynAgX4fYoue4sYj9M4NmSPFW6"), //@dmin
                PhoneNumber = "+99891223435",
                Roles = new List<Role>
                {
                    roles.First(role => role.Type == RoleType.Admin)
                }
            }
        };  
        
         dbContext.Users.AddRange(users);
        
         // Add Hosts
        var hostRole = roles.First(role => role.Type == RoleType.Host);

        var hostFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone)
            .RuleFor(user => user.Roles, () => new List<Role> { hostRole })
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone)
            .RuleFor(user => user.UserCredentials, data => new UserCredentials
            {
                PasswordHash = passwordHasherService.HashPassword(data.Internet.Password(8))
            });

        await dbContext.AddRangeAsync(hostFaker.Generate(1000));
        
        // Add guests.
        var guestRole = roles.First(role => role.Type == RoleType.Guest);

        var guestFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone)
            .RuleFor(user => user.Roles, () => new List<Role>() { guestRole })
            .RuleFor(user => user.UserCredentials, data => new UserCredentials
            {
                PasswordHash = passwordHasherService.HashPassword(data.Internet.Password(8))
            });
        
        await dbContext.AddRangeAsync(guestFaker.Generate(100));
        
        var hostGuestFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone)
            .RuleFor(user => user.Roles, () => new List<Role> { hostRole, guestRole })
            .RuleFor(user => user.UserCredentials, data => new UserCredentials
            {
                PasswordHash = passwordHasherService.HashPassword(data.Internet.Password(8))
            });

        await dbContext.AddRangeAsync(hostGuestFaker.Generate(100));
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
        appDbContext.SaveChanges();
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
       
        var users = appDbContext.Users
            .Where(user => user.Roles.Any(role => role.Type == RoleType.Host)).ToList();
        
        var listingCategories = appDbContext.ListingCategories.ToList();

        // Retrieve listings
        var listings = JsonConvert.DeserializeObject<List<Listing>>(await File.ReadAllTextAsync(listingsFileName))!;

        var hostCounter = 0;

        // Update listings
        listings.ForEach(
            listing =>
            {
                listing.Id = Guid.NewGuid();
                // Add random built date
                listing.BuiltDate = DateOnly.FromDateTime(randomDateTimeProvider.Generate(new DateTime(1950, 1, 1), DateTime.Now.AddYears(-3)));

                // Add host
                listing.HostId = users[hostCounter].Id;
                listing.CreatedByUserId = users[hostCounter].Id;
                listing.CreatedTime = users[hostCounter].CreatedTime;

                // Fix listing category relationship
                listing.ListingCategories = listing.ListingCategories.Select(
                        selectedCategory => listingCategories.FirstOrDefault(listingCategory => listingCategory.Id == selectedCategory.Id)
                    )
                    .ToList()!;

                // Add listing media files
                byte order = 0;
                listing.ImagesStorageFile.ForEach(image =>
                {
                    image.StorageFile = new StorageFile()
                    {
                        Id = image.Id,
                        FileName = $"{image.Id}.jpg",
                        Type = StorageFileType.ListingImage
                    };
                    
                    image.Id = Guid.NewGuid();
                    image.OrderNumber = order++;
                });

                hostCounter = (hostCounter + 1) % users.Count;
            }
        );

        await appDbContext.Listings.AddRangeAsync(listings);
        appDbContext.SaveChanges();
    }
    
    /// <summary>
    /// Seeds Guest Feedbacks data into the AppDbContext using Bogus library.
    /// </summary>
    /// <param name="dbContext"></param>
    private static async ValueTask SeedGuestFeedbacksAsync(this AppDbContext dbContext, ICacheBroker cacheBroker, IRatingProcessingService ratingProcessingService)
    {
        var listings = await dbContext.Listings.ToListAsync();
        var users = await dbContext.Users.Select(user => user.Id).ToListAsync();

        var feedbackFaker = new Faker<GuestFeedback>()
            .RuleFor(feedback => feedback.Rating, GenerateFakeRatings)
            .RuleFor(feedback => feedback.Comment, data => data.Lorem.Paragraph())
            .RuleFor(feedback => feedback.CreatedTime, data => data.Date.PastOffset(5, DateTimeOffset.UtcNow))
            .RuleFor(feedback => feedback.Listing, data => data.PickRandom<Listing>(listings))
            .RuleFor(feedback => feedback.GuestId, (data, feedback) =>
            {
                var listingOwnerId = feedback.Listing.HostId;

                return data.PickRandom(users
                    .Where(userId => userId != listingOwnerId));
            });

        var feedbacks = feedbackFaker.Generate(2000);
        
        await dbContext.GuestFeedbacks.AddRangeAsync(feedbacks);
        dbContext.SaveChanges();

        await cacheBroker.SetAsync(CacheKeyConstants.AddedGuestFeedbacks, feedbacks);
        await ratingProcessingService.ProcessListingsRatings();
    }

    /// <summary>
    /// Generates Faker rating.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    private static Rating GenerateFakeRatings()
    {
        var ratingsFaker = new Faker<Rating>()
            .RuleFor(rating => rating.Accuracy, data => data.Random.Byte(1, 5))
            .RuleFor(rating => rating.Cleanliness, data => data.Random.Byte(1, 5))
            .RuleFor(rating => rating.Communication, data => data.Random.Byte(1, 5))
            .RuleFor(rating => rating.CheckIn, data => data.Random.Byte(1, 5))
            .RuleFor(rating => rating.Location, data => data.Random.Byte(1, 5))
            .RuleFor(rating => rating.Value, data => data.Random.Byte(1, 5))
            .RuleFor(rating => rating.OverallRating, (data, rating) => 
                (rating.Accuracy + rating.Value + rating.Cleanliness + 
                 rating.Communication + rating.Location + rating.CheckIn) / 6);

        return ratingsFaker.Generate();
    }

    /// <summary>
    /// Seeds Notification Templates
    /// </summary>
    /// <param name="appDbContext"></param>
    /// <param name="webHostEnvironment"></param>
    /// <exception cref="NotSupportedException"></exception>
    private static async ValueTask SeedNotificationTemplatesAsync(this AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
    {
        var emailTemplateTypes = new List<NotificationTemplateType>
        {
            NotificationTemplateType.SystemWelcomeNotification,
            NotificationTemplateType.EmailVerificationNotification,
            NotificationTemplateType.ReferralNotification
        };

        var emailTemplateContents = await Task.WhenAll(
            emailTemplateTypes.Select(
                async templateType =>
                {
                    var filePath = Path.Combine(
                        webHostEnvironment.ContentRootPath,
                        "Data",
                        "SeedData",
                        "EmailTemplates",
                        Path.ChangeExtension(templateType.ToString(), "html")
                    );
                    return (TemplateType: templateType, TemplateContent: await File.ReadAllTextAsync(filePath));
                }
            )
        );

        var emailTemplates = emailTemplateContents.Select(
            templateContent => templateContent.TemplateType switch
            {
                NotificationTemplateType.SystemWelcomeNotification => new EmailTemplate
                {
                    TemplateType = templateContent.TemplateType,
                    Subject = "Welcome to our service!",
                    Content = templateContent.TemplateContent
                },
                NotificationTemplateType.EmailVerificationNotification => new EmailTemplate
                {
                    TemplateType = templateContent.TemplateType,
                    Subject = "Confirm your email address",
                    Content = templateContent.TemplateContent
                },
                NotificationTemplateType.ReferralNotification => new EmailTemplate
                {
                    TemplateType = templateContent.TemplateType,
                    Subject = "You have been referred!",
                    Content = templateContent.TemplateContent
                },
                _ => throw new NotSupportedException("Template type not supported.")
            }
        );

        await appDbContext.EmailTemplates.AddRangeAsync(emailTemplates);
        appDbContext.SaveChanges();
    }
}