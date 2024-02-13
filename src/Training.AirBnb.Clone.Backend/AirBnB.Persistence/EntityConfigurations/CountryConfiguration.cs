using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(country => country.Name).HasMaxLength(255).IsRequired();
        builder.Property(country => country.PhoneNumber).HasMaxLength(15).IsRequired();
        // builder.HasMany(country => country.Cities)
        //     .WithOne(city=>city.Country)
        //     .HasForeignKey(city=>city.CountryId);
        builder
            .HasOne(country => country.Currency)
            .WithMany()
            .HasForeignKey(country => country.CurrencyId);
        
        builder.HasOne(country => country.Language)
            .WithMany()
            .HasForeignKey(country => country.LanguageId);
    }
}