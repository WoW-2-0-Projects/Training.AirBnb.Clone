using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

/// <summary>
/// Configuration for the 'Role' entity within the AirBnB database context.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <summary>
    /// Configures the 'Role' entity using the EntityTypeBuilder.
    /// </summary>
    /// <param name="builder">The EntityTypeBuilder for the 'Role' entity.</param>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        //Ensure that the 'Type' property has a unique index
        builder.HasIndex(role => role.Type).IsUnique();
    }
}
