using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}