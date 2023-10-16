namespace AirBnb.Api.Configs;

public static partial class HostConfiguration
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder
            .AddExposers()
            .AddDevTools()
            .AddDataContext()
            .AddServices();
            
        return builder;
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app.UseDevTools();

        await app.SeedDataAsync();

        return app;
    }
}