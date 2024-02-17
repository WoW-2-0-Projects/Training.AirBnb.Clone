using System.Runtime.InteropServices;
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

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<UserSettings> UserSettings => Set<UserSettings>();

    #endregion
    
    #region Notification

    public DbSet<NotificationTemplate> NotificationTemplates => Set<NotificationTemplate>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    public DbSet<NotificationHistory> NotificationHistories => Set<NotificationHistory>();

    public DbSet<EmailHistory> EmailHistories => Set<EmailHistory>();

    public DbSet<SmsHistory> SmsHistories => Set<SmsHistory>();

    #endregion
    
    #region Verification

    public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();

    public DbSet<UserInfoVerificationCode> UserInfoVerificationCodes => Set<UserInfoVerificationCode>();

    #endregion
    
    #region Media

    public DbSet<StorageFile> StorageFiles => Set<StorageFile>();

    public DbSet<ListingMediaFile> ListingMediaFiles => Set<ListingMediaFile>();

    #endregion
    
    #region Listings

    public DbSet<Listing> Listings => Set<Listing>();

    public DbSet<ListingCategory> ListingCategories => Set<ListingCategory>();

    public DbSet<ListingCategoryAssociation> ListingCategoryAssociations => Set<ListingCategoryAssociation>();

    #endregion

    #region Ratings

    public DbSet<GuestFeedback> GuestFeedbacks => Set<GuestFeedback>();

    #endregion

    #region Globalization
    public DbSet<Country> Countries => Set<Country>();

    public DbSet<Language> Languages => Set<Language>();
    
    public DbSet<City> Cities => Set<City>();

    public DbSet<Currency> Currencies => Set<Currency>();
    
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}