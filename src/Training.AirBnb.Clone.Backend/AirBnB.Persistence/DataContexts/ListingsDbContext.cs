using AirBnB.Domain.Entities.Listings;
using AirBnB.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

/// <summary>
/// Represents the Entity Framework DbContext for listings.
/// </summary>
/// <param name="options"></param>
public class ListingsDbContext(DbContextOptions<ListingsDbContext> options) : DbContext(options)
{
    public DbSet<ListingCategory> ListingCategories => Set<ListingCategory>();

    /// <summary>
    /// Customize model creation by setting the default schema and applying configurations.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("listings");
        modelBuilder.ApplyEntityConfigurations<ListingsDbContext>();
    }
}