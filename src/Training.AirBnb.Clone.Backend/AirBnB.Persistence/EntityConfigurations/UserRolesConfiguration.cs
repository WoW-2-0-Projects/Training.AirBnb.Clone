using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });

        builder.HasOne<Role>(userRole => userRole.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(userRole => userRole.RoleId);
        
        builder.HasOne<User>(userRole => userRole.User)
            .WithMany(user => user.Roles)
            .HasForeignKey(userRoles => userRoles.UserId);
    }
}