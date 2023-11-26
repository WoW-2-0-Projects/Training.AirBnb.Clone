using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

public class NotificationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notification");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}