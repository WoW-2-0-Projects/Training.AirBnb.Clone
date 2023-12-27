using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.HasDiscriminator(code => code.Type)
            .HasValue<UserInfoVerificationCode>(VerificationType.UserInfoVerificationCode);

        builder.Property(code => code.Code).IsRequired().HasMaxLength(64);
        builder.Property(code => code.VerificationLink).IsRequired().HasMaxLength(256);
    }
}