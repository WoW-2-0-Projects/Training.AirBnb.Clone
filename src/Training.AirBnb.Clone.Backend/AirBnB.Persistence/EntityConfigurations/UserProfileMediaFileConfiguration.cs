using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class UserProfileMediaFileConfiguration : IEntityTypeConfiguration<UserProfileMediaFile>
{
    public void Configure(EntityTypeBuilder<UserProfileMediaFile> builder)
    {
        builder.HasOne<StorageFile>(media => media.StorageFile)
            .WithOne()
            .HasForeignKey<UserProfileMediaFile>(media => media.StorageFileId);

        builder.HasOne<User>(media => media.User)
            .WithOne(user => user.ProfilePicture)
            .HasForeignKey<UserProfileMediaFile>(media => media.UserId);
    }
}