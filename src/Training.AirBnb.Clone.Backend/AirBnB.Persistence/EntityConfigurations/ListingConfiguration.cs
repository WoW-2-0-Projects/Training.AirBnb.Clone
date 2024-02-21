using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

/// <summary>
/// Entity type configuration for the Listing class.
/// </summary>
public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.Property(listing => listing.Name).IsRequired().HasMaxLength(256);
        builder.Property(listing => listing.CreatedByUserId).IsRequired();

        builder
            .OwnsOne(listing => listing.Address)
            .Property(address => address.City)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.OwnsOne(listing => listing.PricePerNight, moneyConfiguration =>
        {
            moneyConfiguration.HasOne(money => money.Currency)
                .WithMany().HasForeignKey(money => money.CurrencyId);
        });

        builder.OwnsOne(listing => listing.Rating);

        builder.HasOne<User>(listing => listing.Host)
            .WithMany(user => user.Listings);
    }
}