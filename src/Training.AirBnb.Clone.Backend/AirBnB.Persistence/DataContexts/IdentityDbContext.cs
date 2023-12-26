using AirBnB.Domain.Entities.Identity;
using AirBnB.Domain.Entities.StorageFiles;
using AirBnB.Persistence.Extensions;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<UserInfoVerificationCode> UserInfoVerificationCodes => Set<UserInfoVerificationCode>();
    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<UserSettings> UserSettings => Set<UserSettings>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.ApplyEntityConfigurations<IdentityDbContext>();
    }
}