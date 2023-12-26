using AirBnB.Domain.Entities.StorageFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class StorageFileConfigurations : IEntityTypeConfiguration<StorageFile>
{
    public void Configure(EntityTypeBuilder<StorageFile> builder)
    {
        builder.HasIndex(file => file.Id);
        builder.Property(file => file.FileName).HasMaxLength(255).IsRequired();
    }
}