﻿using System.Reflection;
using AirBnB.Api.Data;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Application.Common.Settings;
using AirBnB.Application.Common.StorageFiles;
using AirBnB.Application.Listings.Services;
using AirBnB.Infrastructure.Common.Caching;
using AirBnB.Infrastructure.Common.Identity.Services;
using AirBnB.Infrastructure.Common.Notifications.Services;
using AirBnB.Infrastructure.Common.Settings;
using AirBnB.Infrastructure.Common.StorageFiles;
using AirBnB.Infrastructure.Listings.Services;
using AirBnB.Infrastructure.StorageFiles.Settings;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

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
    /// Registers NotificationDbContext in DI 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("NotificationConnection"),
                o => o.MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: "notification")));

        builder.Services
            .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
            .AddScoped<ISmsTemplateRepository, SmsTemplateRepository>();
            
        builder.Services
            .AddScoped<IEmailTemplateService, EmailTemplateService>()
            .AddScoped<ISmsTemplateService, SmsTemplateService>();
        
        return builder;
    }

    /// <summary>
    /// Configures IdentityInfrastructure including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"),
                o => o.MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: "identity")));

        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserService, UserService>();
        
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        return builder;
    }

    /// <summary>
    /// Configures Listings Infrastructure, including services, repositories, dbContexts.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddListingsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ListingsDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ListingsConnection"),
            o => o.MigrationsHistoryTable(
                tableName: HistoryRepository.DefaultTableName,
                schema: "listings")));

        // register repositories
        builder.Services.AddScoped<IListingCategoryRepository, ListingCategoryRepository>();

        // register foundation services
        builder.Services.AddScoped<IListingCategoryService, ListingCategoryService>();

        return builder;
    }
    
    /// <summary>
    ///  Extension method to add storage file infrastructure services
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddStorageFileInfrastructure(this WebApplicationBuilder builder)
    {
        // configure storage file settings
        builder.Services.Configure<StorageFileSettings>(builder.Configuration.GetSection(nameof(StorageFileSettings)))
           .Configure<ApiSettings>(builder.Configuration.GetSection(nameof(ApiSettings)));

        // register repositories
        builder.Services
            .AddScoped<IStorageFileRepository, StorageFileRepository>();

        // register foundation services
        builder.Services
            .AddScoped<IStorageFileService, StorageFileService>();

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