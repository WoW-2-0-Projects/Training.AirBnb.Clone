using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Listing> Listings => Set<Listing>();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);

        // Seeds initial data for the Listing entity.
        modelBuilder.Entity<Listing>().HasData(
            new Listing
            {
                Id = Guid.NewGuid(),
                Title = "FirstSeedData",
                DescriptionId = Guid.NewGuid(),
                Status = ListingStatus.InProgress,
                PropertyTypeId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                RulesId = Guid.NewGuid(),
                AvailabilityId = Guid.NewGuid(),
                HostId = Guid.NewGuid(),
                Price = 99.333m,
                InstantBook = true
            }
        );
    }
}