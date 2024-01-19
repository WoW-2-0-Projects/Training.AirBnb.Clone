using System.Text;
using System.Reflection;
using AirBnB.Api.Data;
using AirBnB.Application.Common.Identity.Services;
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
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using AirBnB.Application.Listings.Services;
using AirBnB.Infrastructure.Common.Serializers;
using AirBnB.Infrastructure.Listings.Services;
using AirBnB.Infrastructure.StorageFiles.Settings;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        
        return builder;
    }

    /// <summary>
    /// Configures IdentityInfrastructure including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserSettingsRepository, UserSettingsRepository>()
            .AddScoped<IUserSettingsService, UserSettingsService>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>()
            .AddScoped<IAccessTokenGeneratorService, AccessTokenGeneratorService>()
            .AddScoped<IAccessTokenService, AccessTokenService>()
            .AddScoped<IPasswordHasherService, PasswordHasherService>();
            

        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

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

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString")));
        
        return builder;
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