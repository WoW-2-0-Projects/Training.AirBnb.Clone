using Backend_Project.Application.Entity;
using Backend_Project.Application.Foundations.AccountServices;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Foundations.LocationServices;
using Backend_Project.Application.Listings;
using Backend_Project.Application.Notifications;
using Backend_Project.Application.Validation;
using Backend_Project.Domain.Entities;
using Backend_Project.Infrastructure.CompositionServices;
using Backend_Project.Infrastructure.Services;
using Backend_Project.Infrastructure.Services.AccountServices;
using Backend_Project.Infrastructure.Services.ListingServices;
using Backend_Project.Infrastructure.Services.LocationServices;
using Backend_Project.Infrastructure.Services.NotificationsServices;
using Backend_Project.Infrastructure.Services.ReservationServices;
using Backend_Project.Infrastructure.Services.ReviewServices;
using Backend_Project.Persistence.DataContexts;
using Backend_Project.Persistence.SeedData;
using FileBaseContext.Context.Models.Configurations;

namespace AirBnb.Api.Configs;

public static partial class HostConfiguration
{
    public static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder;
    }

    public static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplicationBuilder AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDataContext, AppFileContext>(_ =>
        {
            var contextOptions = new FileContextOptions<AppFileContext>
            {
                StorageRootPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "DataStorage")
            };

            var context = new AppFileContext(contextOptions);
            context.FetchAsync().AsTask().Wait();

            return context;
        });

        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder
            .AddValidationServices()
            .AddAccountServices()
            .AddListingServices()
            .AddListingCategoryServices()
            .AddListingAmenityServices()
            .AddLocationServices()
            .AddReservationServices()
            .AddReviewServices()
            .AddNotificationServices();

        return builder;
    }

    public static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    public static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {

        var context = app.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IDataContext>();

        await context.InitializeUsersSeedDataAsync();
        await context.InitializeCategoryDetailsSeedData();
        await context.InitializeAmenityAndAmenityCategorySeedData();
        await context.InitializeListingPropertySeedData();
        await context.InitializeLocationSeedData();
        await context.InitializeEmailTemplateSeedDate();
        await context.InitializeAvailabilitySeedData();

        return app;
    }

    #region Registration of services divided into categories.

    private static WebApplicationBuilder AddAccountServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserCredentialsService, UserCredentialsService>()
            .AddScoped<IPhoneNumberService, PhoneNumberService>();

        return builder;
    }

    private static WebApplicationBuilder AddListingServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IListingService, ListingService>()
            .AddScoped<IListingPropertyService, ListingPropertyService>()
            .AddScoped<IListingPropertyTypeService, ListingPropertyTypeService>()
            .AddScoped<IListingRatingService, ListingRatingService>()
            .AddScoped<IListingRulesService, ListingRulesService>()
            .AddScoped<IDescriptionService, DescriptionService>()
            .AddScoped<IBlockedNightService, BlockedNightService>();

        return builder;
    }

    private static WebApplicationBuilder AddListingCategoryServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IListingCategoryService, ListingCategoryService>()
            .AddScoped<IListingFeatureService, ListingFeatureService>()
            .AddScoped<IListingTypeService, ListingTypeService>()
            .AddScoped<IListingCategoryTypeService, ListingCategoryTypeService>()
            .AddScoped<IListingCategoryDetailsService, ListingCategoryDetailsService>();

        return builder;
    }

    private static WebApplicationBuilder AddListingAmenityServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IAmenityService, AmenityService>()
            .AddScoped<IAmenityCategoryService, AmenityCategoryService>()
            .AddScoped<IListingAmenitiesService, ListingAmenitiesService>()
            .AddScoped<IAmenitiesManagementService, AmenitiesManagementService>();

        return builder;
    }

    private static WebApplicationBuilder AddLocationServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IAddressService, AddressService>()
            .AddScoped<ICountryService, CountryService>()
            .AddScoped<ICityService, CityService>()
            .AddScoped<ILocationScenicViewsService, LocationScenicViewsService>()
            .AddScoped<ILocationService, LocationService>()
            .AddScoped<IScenicViewService, ScenicViewService>();

        return builder;
    }

    private static WebApplicationBuilder AddReservationServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IEntityBaseService<Reservation>, ReservationService>()
            .AddScoped<IEntityBaseService<ReservationOccupancy>, ReservationOccupancyService>()
            .AddScoped<IAvailabilityService, AvailabilityService>();

        return builder;
    }

    private static WebApplicationBuilder AddReviewServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IEntityBaseService<Comment>, CommentService>()
            .AddScoped<IEntityBaseService<Rating>, RatingService>();

        return builder;
    }

    private static WebApplicationBuilder AddNotificationServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IEntityBaseService<Email>, EmailService>()
            .AddScoped<IEntityBaseService<EmailTemplate>, EmailTemplateService>()
            .AddScoped<IEmailPlaceholderService, EmailPlaceholderService>()
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<IEmailMessageService, EmailMessageSevice>()
            .AddScoped<IEmailManagementService, EmailManagementService>();

        return builder;
    }

    private static WebApplicationBuilder AddValidationServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IValidationService, ValidationService>();

        return builder;
    }

    #endregion
}