using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder
            .HasOne(userSettings => userSettings.User)
            .WithOne(user => user.UserSettings)
            .HasForeignKey(typeof(UserSettings), "UserId");
    }
}
