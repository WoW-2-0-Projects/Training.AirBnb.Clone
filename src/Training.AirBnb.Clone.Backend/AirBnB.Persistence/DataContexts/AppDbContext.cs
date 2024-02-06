using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

/// <summary>
/// Represents Database Context for the entire web application
/// </summary>
/// <param name="options"></param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    #region Identity

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<UserSettings> UserSettings => Set<UserSettings>();

    #endregion

    #region Notification

    public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    #endregion

    #region Verification

    public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();

    public DbSet<UserInfoVerificationCode> UserInfoVerificationCodes => Set<UserInfoVerificationCode>();

    #endregion

    #region Media

    public DbSet<StorageFile> StorageFiles => Set<StorageFile>();

    #endregion

    #region Listings

    public DbSet<Listing> Listings => Set<Listing>();

    public DbSet<ListingCategory> ListingCategories => Set<ListingCategory>();

    public DbSet<ListingCategoryAssociation> ListingCategoryAssociations => Set<ListingCategoryAssociation>();

    #endregion
    public DbSet<Country> Countries => Set<Country>();

    #region Ratings

    public DbSet<GuestFeedback> GuestFeedbacks => Set<GuestFeedback>();

    #endregion

    #region Finance

    public DbSet<Currency> Currencies => Set<Currency>();
    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}