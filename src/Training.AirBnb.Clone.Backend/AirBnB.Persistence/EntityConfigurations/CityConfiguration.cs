using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(city => city.Name).HasMaxLength(256).IsRequired();

        builder.HasOne(city => city.Country)
            .WithMany(country=>country.Cities)
            .HasForeignKey(city => city.CountryId);
    }
}