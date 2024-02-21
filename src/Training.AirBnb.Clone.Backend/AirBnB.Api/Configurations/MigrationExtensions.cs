using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Configurations;

public static class MigrationExtensions
{
    /// <summary>
    /// Migrates existing database schema to data sources
    /// </summary>
    /// <param name="scopeFactory">Service scope factory</param>
    /// <typeparam name="TContext">Data access context</typeparam>
    public static async ValueTask MigrateAsync<TContext>(this IServiceScopeFactory scopeFactory) where TContext : DbContext
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
            await context.Database.MigrateAsync();
    }
}