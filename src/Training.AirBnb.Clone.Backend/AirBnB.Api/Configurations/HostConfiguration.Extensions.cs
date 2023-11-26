using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirBnB.Api.Configurations;

public static partial class HostConfiguration
{
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

    // todo: AddNotificationsInfrastructure add service
    // todo: register NotificationsDb 
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: "notification")));
        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: "identity")));

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