using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });

        builder.HasOne(userRole => userRole.Role)
            .WithMany(user => user.Users)
            .HasForeignKey(userRole => userRole.RoleId);
        
        builder.HasOne(userRole => userRole.User)
            .WithMany(role => role.Roles)
            .HasForeignKey(userRoles => userRoles.UserId);
    }
}