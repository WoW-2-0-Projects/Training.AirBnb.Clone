using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class NotificationHistoryConfiguration : IEntityTypeConfiguration<NotificationHistory>
{
    public void Configure(EntityTypeBuilder<NotificationHistory> builder)
    {
        builder
            .HasDiscriminator(history => history.Type)
            .HasValue<EmailHistory>(NotificationType.Email)
            .HasValue<SmsHistory>(NotificationType.Sms);
    }
}
