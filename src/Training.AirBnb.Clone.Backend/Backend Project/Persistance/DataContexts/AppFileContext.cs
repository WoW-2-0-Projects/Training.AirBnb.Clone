using Backend_Project.Domain.Common;
using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileEntry;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Context.Models.Configurations;
using FileBaseContext.Context.Models.FileContext;

namespace Backend_Project.Persistance.DataContexts;

public class AppFileContext : FileContext, IDataContext
{
    public IFileSet<EmailTemplate, Guid> EmailTemplates => Set<EmailTemplate, Guid>(nameof(EmailTemplates));
    public IFileSet<EmailMessage, Guid> EmailMessages => Set<EmailMessage, Guid>(nameof(EmailMessages));
    public IFileSet<Email,Guid> Emails => Set<Email, Guid>(nameof(Emails));
    public IFileSet<Reservation, Guid> Reservations => Set<Reservation, Guid>(nameof(Reservations));
    public IFileSet<City, Guid> Cities => Set<City, Guid>(nameof(Cities));
    public IFileSet<Country, Guid> Countries => Set<Country, Guid>(nameof(Countries));
    public IFileSet<User, Guid> Users => Set<User, Guid>(nameof(Users));
    public IFileSet<Address, Guid> Addresses => Set<Address, Guid>(nameof(Addresses));
    public IFileSet<ReservationOccupancy,Guid> ReservationOccupancies => Set<ReservationOccupancy, Guid>(nameof(ReservationOccupancies));
    public IFileSet<Comment, Guid> Comments => Set<Comment, Guid>(nameof(Comments));
    public IFileSet<UserCredentials, Guid> UserCredentials => Set<UserCredentials, Guid>(nameof(UserCredentials));
    public IFileSet<AmenityCategory, Guid> AmenityCategories => Set<AmenityCategory, Guid>(nameof(AmenityCategories));
    public IFileSet<ListingCategory,Guid> ListingCategories => Set<ListingCategory, Guid>(nameof(ListingCategories));
    public IFileSet<ListingRating, Guid> ListingRatings => Set<ListingRating, Guid>(nameof(ListingRatings));
    public IFileSet<Amenity, Guid> Amenities => Set<Amenity, Guid>(nameof(Amenities));
    public IFileSet<ListingFeatureOption, Guid> ListingFeatureOptions => Set<ListingFeatureOption, Guid>(nameof(ListingFeatureOptions));
    public IFileSet<ListingCategoryFeatureOption, Guid> ListingCategoryFeatureOptions => Set<ListingCategoryFeatureOption, Guid>(nameof(ListingCategoryFeatureOptions));
    public IFileSet<ListingFeature, Guid> ListingFeatures => Set<ListingFeature, Guid>(nameof(ListingFeatures));
    public IFileSet<ListingProperty, Guid> ListingProperties => Set<ListingProperty, Guid>(nameof(ListingProperties));
    public IFileSet<ListingAmenities, Guid> ListingAmenities => Set<ListingAmenities, Guid>(nameof(ListingAmenities));
    public IFileSet<ListingOccupancy, Guid> ListingOccupancies => Set<ListingOccupancy, Guid>(nameof(ListingOccupancies));
    public IFileSet<Listing, Guid> Listings => Set<Listing, Guid>(nameof(Listings));

    public AppFileContext(IFileContextOptions<AppFileContext> fileContextOptions) : base(fileContextOptions)
    {
        OnSaveChanges += AddPrimaryKeys;
        OnSaveChanges += AddAuditableDetails;
        OnSaveChanges += AddSoftDeletionDetails;
    }

    public ValueTask AddPrimaryKeys(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSet in fileSets)
            foreach (var entry in fileSet.GetEntries())
            {
                if (entry is not IFileEntityEntry<IEntity> entityEntry) continue;

                if (entityEntry.State == FileEntityState.Added)
                    entityEntry.Entity.Id = Guid.NewGuid();

                if (entry is not IFileEntityEntry<IFileSetEntity<Guid>>) continue;
            }

        return new ValueTask(Task.CompletedTask);
    }

    public ValueTask AddAuditableDetails(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSet in fileSets)
            foreach (var entry in fileSet.GetEntries())
            {
                if (entry is not IFileEntityEntry<IAuditableEntity> entityEntry) continue;

                if (entityEntry.State == FileEntityState.Added)
                    entityEntry.Entity.CreatedDate = DateTimeOffset.Now;

                if (entityEntry.State == FileEntityState.Modified)
                    entityEntry.Entity.ModifiedDate = DateTimeOffset.Now;

                if (entry is not IFileEntityEntry<IFileSetEntity<Guid>>) continue;
            }

        return new ValueTask(Task.CompletedTask);
    }

    public ValueTask AddSoftDeletionDetails(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSet in fileSets)
            foreach (var entry in fileSet.GetEntries())
            {
                // Skip entities that are not soft deletable
                if (entry is not IFileEntityEntry<ISoftDeletedEntity> { State: FileEntityState.Deleted } entityEntry) continue;

                // Soft delete all entities except PostView
                entityEntry.Entity.IsDeleted = true;
                entityEntry.Entity.DeletedDate = DateTimeOffset.Now;
                entityEntry.State = FileEntityState.MarkedDeleted;
            }

        return new ValueTask(Task.CompletedTask);
    }
}