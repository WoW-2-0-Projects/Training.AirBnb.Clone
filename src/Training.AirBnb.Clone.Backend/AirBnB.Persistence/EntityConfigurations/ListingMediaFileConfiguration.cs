using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class ListingMediaFileConfiguration : IEntityTypeConfiguration<ListingMediaFile>
{
    public void Configure(EntityTypeBuilder<ListingMediaFile> builder)
    {
        builder
            .HasOne<StorageFile>(media => media.StorageFile)
            .WithOne()
            .HasForeignKey<ListingMediaFile>(media => media.StorageFileId);

        builder.HasOne<Listing>(media => media.Listing)
            .WithMany(listing => listing.ImagesStorageFile)
            .HasForeignKey(media => media.ListingId);

        builder.HasQueryFilter(media => !media.IsDeleted);
    }
}