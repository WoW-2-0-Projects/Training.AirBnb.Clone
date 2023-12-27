using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.Extensions;

/// <summary>
/// A static class providing extension methods for configuring entity models in Entity Framework.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// // Extension method for applying entity configurations to the provided ModelBuilder instance. Assumes TDataContext is a DbContext.
    /// </summary>
    /// <typeparam name="TDataContext"></typeparam>
    /// <param name="modelBuilder"></param>
    public static void ApplyEntityConfigurations<TDataContext>(this ModelBuilder modelBuilder) where TDataContext : DbContext
    {
        // Get type information for the provided DbContext type.
        var dbContextType = typeof(TDataContext);

        // Retrieve properties of type DbSet<> from the DbContext.
        var dbSetProperties = dbContextType
            .GetProperties()
            .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        // Extract entity types from DbSet<> properties.
        var dbSetTypes = dbSetProperties
            .Select(p => p.PropertyType.GetGenericArguments()[0])
            .ToList();

        // Create a list of IEntityTypeConfiguration<> types for each entity type.
        var possibleEntityConfigurationTypes = dbSetTypes
            .Select(dbSetType => typeof(IEntityTypeConfiguration<>)
            .MakeGenericType(dbSetType))
            .ToList();

        // Discover and filter classes implementing entity configurations.
        var matchingConfigurationTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract &&
                           possibleEntityConfigurationTypes.Exists(configType => configType.IsAssignableFrom(type)))
            .ToList();

        // Apply each discovered configuration to the ModelBuilder.
        matchingConfigurationTypes.ForEach(type => modelBuilder.ApplyConfiguration((dynamic)Activator.CreateInstance(type)!));
    }
}