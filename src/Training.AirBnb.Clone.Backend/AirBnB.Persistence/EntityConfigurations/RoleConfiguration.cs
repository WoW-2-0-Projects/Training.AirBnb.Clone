using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations
{
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
            // Ensure that the 'Type' property has a unique index
            builder.HasIndex(role => role.Type).IsUnique();

            // Seed initial data for roles in the database
            builder.HasData(
                new Role
                {
                    Id = Guid.Parse("29E62346-1BB7-4FD4-833F-8EBD85734570"),
                    Type = RoleType.System,
                    CreatedTime = DateTime.UtcNow
                },
                new Role
                {
                    Id = Guid.Parse("EEC07FC2-2A0D-4E63-B084-1975E836793C"),
                    Type = RoleType.Admin,
                    CreatedTime = DateTime.UtcNow
                },
                new Role
                {
                    Id = Guid.Parse("C93760C5-03ED-4845-B3C9-01C125EF326A"),
                    Type = RoleType.User,
                    CreatedTime = DateTime.UtcNow
                }
            );
        }
    }
}
