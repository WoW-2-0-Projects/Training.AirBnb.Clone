using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class ListingCategoryAssociationConfiguration : IEntityTypeConfiguration<ListingCategoryAssociation>
{
    public void Configure(EntityTypeBuilder<ListingCategoryAssociation> builder)
    {
        builder.HasKey(relation => new { relation.ListingId, relation.ListingCategoryId });

        builder.HasOne<Listing>(relation => relation.Listing)
            .WithMany(listing => listing.ListingCategoryAssociations)
            .HasForeignKey(relation => relation.ListingId);

        builder.HasOne<ListingCategory>(relation => relation.ListingCategory)
            .WithMany(category => category.ListingCategoryAssociations)
            .HasForeignKey(relation => relation.ListingCategoryId);
    }
}