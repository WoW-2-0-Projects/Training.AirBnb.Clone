using AirBnB.Domain.Entities.Notification;
using AirBnB.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
{
    DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notification");
        modelBuilder.ApplyEntityConfigurations<NotificationDbContext>();
    }
}