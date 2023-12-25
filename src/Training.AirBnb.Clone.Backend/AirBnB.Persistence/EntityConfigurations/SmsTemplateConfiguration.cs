using AirBnB.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class SmsTemplateConfiguration : IEntityTypeConfiguration<SmsTemplate>
{
    public void Configure(EntityTypeBuilder<SmsTemplate> builder)
    {
    }
}