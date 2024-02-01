using AirBnB.Application.Common.Identity.Services;
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
        var passwordHasherService = serviceProvider.GetRequiredService<IPasswordHasherService>();
        
        if (!await appDbContext.Roles.AnyAsync())
            await appDbContext.SeedRolesAsync();
        
        if (!await appDbContext.Users.AnyAsync())
            await appDbContext.SeedUsersAsync(passwordHasherService);

        if (!await appDbContext.ListingCategories.AnyAsync())
            await appDbContext.SeedListingCategoriesAsync(webHostEnvironment);
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
        await dbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Seeds user data into the AppDbContext using Bogus library.
    /// </summary>
    /// <param name="dbContext">The AppDbContext instance to seed data into.</param>
    /// <returns>An asynchronous task representing the seeding process.</returns>
    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext, IPasswordHasherService passwordHasherService)
    {
        // Add system user.
       // var systemRoleId = dbContext.Roles.First(role => role.Type == RoleType.System).Id;
        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("7dead347-e459-4c4a-85b0-8f1b373d3dec"),
                FirstName = "System",
                LastName = "System",
                EmailAddress = "example@gmail.com",
                PasswordHash = "$2a$11$7CblN46.ijAjU98BhD5wf.ZageDRq8uabpL5rmwdcC/VBC6hifiIa",//A1rBnB.$com,
                PhoneNumber = ""
            },
            new()
            {
                FirstName = "Admin",
                LastName = "Admin",
                EmailAddress = "admin@gmail.com",
                PasswordHash = "$2a$11$uUKbkiC9nzwqpAaGN5tV9uLIa8XWynAgX4fYoue4sYj9M4NmSPFW6", //@dmin
                PhoneNumber = "+99891223435",
            }
        };  
        
         dbContext.Users.AddRange(users);
         var getAllRoles = dbContext.Roles.ToList();
        
         // Add Hosts
        var hostRole = getAllRoles.First(role => role.Type == RoleType.Host);

        var hostFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PasswordHash, data => passwordHasherService.HashPassword(data.Internet.Password(8)))
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone)
            .RuleFor(user => user.Roles, () => new List<Role>() { hostRole });

        await dbContext.AddRangeAsync(hostFaker.Generate(100));
        
        // Add guests.
        var guestRole = getAllRoles.First(role => role.Type == RoleType.Guest);

        var guestFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PasswordHash, data => passwordHasherService.HashPassword(data.Internet.Password(8)))
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone)
            .RuleFor(user => user.Roles, () => new List<Role>() { guestRole });
        
        await dbContext.AddRangeAsync(guestFaker.Generate(100));
        
        var hostGuestFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.PasswordHash, data => passwordHasherService.HashPassword(data.Internet.Password(8)))
            .RuleFor(user => user.PhoneNumber, data => data.Person.Phone);
        
        await dbContext.AddRangeAsync(hostGuestFaker.Generate(100));
        
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
        await appDbContext.SaveChangesAsync();
    }
}