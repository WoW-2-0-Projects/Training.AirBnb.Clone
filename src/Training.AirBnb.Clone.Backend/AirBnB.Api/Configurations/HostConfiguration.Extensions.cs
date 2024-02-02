using System.Text;
using System.Reflection;
using AirBnb.Api.Configurations;
using AirBnB.Api.Data;
using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Brokers;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Application.Common.Serializers;
using AirBnB.Application.Common.Settings;
using AirBnB.Application.Common.Verifications.Services;
using AirBnB.Infrastructure.Common.Caching;
using AirBnB.Infrastructure.Common.Identity.Services;
using AirBnB.Infrastructure.Common.Notifications.Services;
using AirBnB.Infrastructure.Common.Settings;
using AirBnB.Infrastructure.Common.Verifications.Services;
using AirBnB.Application.Common.StorageFiles;
using AirBnB.Infrastructure.Common.StorageFiles;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AirBnB.Application.Listings.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Infrastructure.Common.Notifications.Brokers;
using AirBnB.Infrastructure.Common.EventBus.Services;
using AirBnB.Infrastructure.Common.RequestContexts.Brokers;
using AirBnB.Application.Ratings.Services;
using AirBnB.Application.Ratings.Settings;
using AirBnB.Domain.Settings;
using AirBnB.Infrastructure.Common.EventBus.Brokers;
using AirBnB.Infrastructure.Common.Serializers;
using AirBnB.Infrastructure.Listings.Services;
using AirBnB.Infrastructure.Ratings.Services;
using AirBnB.Infrastructure.StorageFiles.Settings;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AirBnB.Persistence.Interceptors;

namespace AirBnB.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

   /// <summary>
   /// Adds MediatR services to the application with custom service registrations.
   /// </summary>
   /// <param name="builder"></param>
   /// <returns></returns>
    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(Assemblies.ToArray()); });

        return builder;
    }
    /// <summary>
    /// Adds caching services to the web application builder.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        // Configure CacheSettings from the app settings.
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        // Configure Redis caching with options from the app settings.
        builder.Services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "AirBnb.CacheMemory";
            });

        // Register the RedisDistributedCacheBroker as a singleton.
        builder.Services.AddSingleton<ICacheBroker, RedisDistributedCacheBroker>();
        
        // register authentication handlers
        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ??
                          throw new InvalidOperationException("JwtSettings is not configured.");

        // add authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidateLifetime = jwtSettings.ValidateLifetime,
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                }
            );

        return builder;
    }

    private static WebApplicationBuilder AddEventBus(this WebApplicationBuilder builder)
    {
        //register settings
        builder.Services.Configure<RabbitMqConnectionSettings>(builder.Configuration.GetSection(nameof(RabbitMqConnectionSettings)));
        
        //register brokers
        builder.Services.AddSingleton<IRabbitMqConnectionProvider, RabbitMqConnectionProvider>()
            .AddSingleton<IEvenBusBroker, RabbitMqEventBusBroker>();
        
        //register general background service
        builder.Services.AddHostedService<EventBusBackgroundService>();
        
        return builder;
    }
    
    /// <summary>
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);
        return builder;
    }

    /// <summary>
    /// Configures AutoMapper for object-to-object mapping using the specified profile.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }

    /// <summary>
    /// Configures and adds Serializers to web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddSerializers(this WebApplicationBuilder builder)
    {
        // register json serialization settings
        builder.Services.AddSingleton<IJsonSerializationSettingsProvider, JsonSerializationSettingsProvider>();
        
        return builder;
    }
    
    /// <summary>
    /// Registers NotificationDbContext in DI 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
            .AddScoped<ISmsTemplateRepository, SmsTemplateRepository>();
            
        builder.Services
            .AddScoped<IEmailTemplateService, EmailTemplateService>()
            .AddScoped<ISmsTemplateService, SmsTemplateService>()
            .AddScoped<IEmailRenderingService, EmailRenderingService>()
            .AddScoped<ISmsRenderingService, SmsRenderingService>();

        builder.Services
            .AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>()
            .AddScoped<IEmailSenderBroker, SmtpEmailSenderBroker>();

        builder.Services
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<ISmsSenderService, SmsSenderService>()
            .AddScoped<IEmailRenderingService, EmailRenderingService>()
            .AddScoped<ISmsRenderingService, SmsRenderingService>();
        
        return builder;
    }

    /// <summary>
    /// Configures IdentityInfrastructure including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        //configuration settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        builder.Services.Configure<PasswordValidationSettings>(builder.Configuration.GetSection(nameof(PasswordValidationSettings)));
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        //register repository
        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserSettingsRepository, UserSettingsRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>();
        
        //register services
        builder.Services    
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserSettingsService, UserSettingsService>()
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAccessTokenGeneratorService, AccessTokenGeneratorService>()
            .AddScoped<IAccessTokenService, AccessTokenService>()
            .AddScoped<IPasswordGeneratorService, PasswordGeneratorService>()
            .AddScoped<IPasswordHasherService, PasswordHasherService>()
            .AddScoped<IRoleProcessingService, RoleProcessingService>();
        
        return builder;
    }
    
    /// <summary>
    ///  Extension method to add storage file infrastructure services
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddStorageFileInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<StorageFileSettings>(builder.Configuration.GetSection(nameof(StorageFileSettings)));

        builder.Services
            .AddScoped<IStorageFileRepository, StorageFileRepository>()
            .AddScoped<IStorageFileService, StorageFileService>();

        return builder;
    }

    /// <summary>
    /// Configures Listings Infrastructure, including services, repositories, dbContexts.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddListingsInfrastructure(this WebApplicationBuilder builder)
    {
        // register repositories
        builder.Services
            .AddScoped<IListingRepository, ListingRepository>()
            .AddScoped<IListingCategoryRepository, ListingCategoryRepository>();

        // register foundation services
        builder.Services
            .AddScoped<IListingService, ListingService>()
            .AddScoped<IListingCategoryService, ListingCategoryService>();

        return builder;
    }

    /// <summary>
    /// Extension method to configure and add verification infrastructure to the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddVerificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<VerificationCodeSettings>(
            builder.Configuration.GetSection(nameof(VerificationCodeSettings)));

        builder.Services.AddScoped<IUserInfoVerificationCodeRepository, UserInfoVerificationCodeRepository>();

        builder.Services.AddScoped<IUserInfoVerificationCodeService, UserInfoVerificationCodeService>();

        return builder;
    }

    /// <summary>
    /// Configures Listing Ratings and Feedbacks infrastructure including repositories and services
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddRatingsInfrastructure(this WebApplicationBuilder builder)
    {
        // configure ratings related settings.
        builder.Services.Configure<RatingProcessingSchedulerSettings>(
            builder.Configuration.GetSection(nameof(RatingProcessingSchedulerSettings)));
        
        builder.Services.Configure<GuestFeedbacksCacheSettings>(
            builder.Configuration.GetSection(nameof(GuestFeedbacksCacheSettings)));
        
        // register services
        builder.Services.AddScoped<IGuestFeedbackRepository, GuestFeedbackRepository>();
        builder.Services.AddScoped<IGuestFeedbackService, GuestFeedbackService>();
        builder.Services.AddScoped<IRatingProcessingService, RatingProcessingService>();
        
        builder.Services.AddHostedService<RatingProcessingScheduler>();
        
        return builder;
    }

    /// <summary>
    /// Configures Request Context tool for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddRequestContextTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        
        builder.Services.AddScoped<IRequestUserContextProvider, RequestUserContextProvider>();

        builder.Services.Configure<RequestUserContextSettings>(
            builder.Configuration.GetSection(nameof(RequestUserContextSettings)));
        
        return builder;
    }
    
    /// <summary>
    /// Configures DbContext and ef-core interceptors for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // register ef core interceptors
        builder.Services
            .AddScoped<UpdatePrimaryKeyInterceptor>()
            .AddScoped<UpdateAuditableInterceptor>()
            .AddScoped<UpdateSoftDeletionInterceptor>();
        
        // register db context
        builder.Services.AddDbContext<AppDbContext>((provider, options) =>
        {
            options
                .UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString"))
                .AddInterceptors(provider.GetRequiredService<UpdatePrimaryKeyInterceptor>(),
                    provider.GetRequiredService<UpdateAuditableInterceptor>(),
                    provider.GetRequiredService<UpdateSoftDeletionInterceptor>());
        });
        
        return builder;
    }
    
    /// <summary>
    /// Migrates existing database schema to data sources
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static async ValueTask<WebApplication> MigrateDatabaseSchemasAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);
        
        await serviceScopeFactory.MigrateAsync<AppDbContext>();
        
        return app;
    }
    
    /// <summary>
    /// Seeds data into the application's database by creating a service scope and initializing the seed operation.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();
        
        return app;
    }
    
    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns></returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection(nameof(ApiSettings)));

        return builder;
    }
    
    /// <summary>
    /// Configures CORS for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin", 
            policy => policy
                .WithOrigins(builder.Configuration["ApiClientSettings:WebClientAddress"]!)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()));
        
        return builder;
    }

    /// <summary>
    /// Configures devTools including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
    
    /// <summary>
    /// Enables CORS middleware in the web application pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static WebApplication UseCors(this WebApplication app)
    {
        app.UseCors("AllowSpecificOrigin");
        
        return app;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}