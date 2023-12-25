using AirBnB.Domain.Entities.StorageFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class StorageFileConfigurations : IEntityTypeConfiguration<StorageFile>
{
    public void Configure(EntityTypeBuilder<StorageFile> builder)
    {
        builder.Property(e => e.FileName).HasMaxLength(255).IsRequired();
    }
}