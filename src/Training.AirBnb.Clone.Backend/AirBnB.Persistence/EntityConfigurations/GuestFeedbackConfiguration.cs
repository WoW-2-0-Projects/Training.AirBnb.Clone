using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class GuestFeedbackConfiguration : IEntityTypeConfiguration<GuestFeedback>
{
    public void Configure(EntityTypeBuilder<GuestFeedback> builder)
    {
        // configure validations
        builder.Property(feedback => feedback.Accuracy).IsRequired();
        builder.Property(feedback => feedback.Cleanliness).IsRequired();
        builder.Property(feedback => feedback.Communication).IsRequired();
        builder.Property(feedback => feedback.Location).IsRequired();
        builder.Property(feedback => feedback.Value).IsRequired();
        builder.Property(feedback => feedback.CheckIn).IsRequired();
        builder.Property(feedback => feedback.OverallRating).IsRequired();
        builder.Property(feedback => feedback.Comment).IsRequired();
        
        // configure relations.
        builder.HasOne<User>(feedback => feedback.Guest).WithMany();
        builder.HasOne<Listing>(feedback => feedback.Listing)
            .WithMany(listing => listing.Feedbacks);
    }
}