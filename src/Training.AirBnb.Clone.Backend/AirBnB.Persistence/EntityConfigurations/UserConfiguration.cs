using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(user => user.Role).WithMany().HasForeignKey(user => user.RoleId);

        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(user => user.LastName).IsRequired().HasMaxLength(128);
        builder.Property(user => user.EmailAddress).IsRequired().HasMaxLength(128);

        builder.OwnsOne(user => user.UserCredentials, userCredentialsConfiguration =>
        {
            userCredentialsConfiguration.Property(userCredentials => userCredentials.PasswordHash).IsRequired()
                .HasMaxLength(128);
        });

        builder.HasIndex(user => user.EmailAddress).IsUnique();
    }
}