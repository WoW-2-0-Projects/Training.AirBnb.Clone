using AirBnB.Domain.Entities.Identity;
using AirBnB.Domain.Entities.StorageFiles;
using AirBnB.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.ApplyEntityConfigurations<IdentityDbContext>();
    }
}