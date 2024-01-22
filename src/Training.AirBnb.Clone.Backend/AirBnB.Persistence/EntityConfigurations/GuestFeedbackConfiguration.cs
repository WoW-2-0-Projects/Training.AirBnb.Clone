using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class GuestFeedbackConfiguration : IEntityTypeConfiguration<GuestFeedback>
{
    public void Configure(EntityTypeBuilder<GuestFeedback> builder)
    {
        // configure validations
        builder.OwnsOne<Rating>(feedback => feedback.Rating, rating =>
        {
            rating.Property(feedback => feedback.Accuracy).IsRequired().HasPrecision(2, 1);    
            rating.Property(feedback => feedback.Cleanliness).IsRequired().HasPrecision(2, 1);
            rating.Property(feedback => feedback.Communication).IsRequired().HasPrecision(2, 1);
            rating.Property(feedback => feedback.Location).IsRequired().HasPrecision(2, 1);    
            rating.Property(feedback => feedback.Value).IsRequired().HasPrecision(2, 1);       
            rating.Property(feedback => feedback.CheckIn).IsRequired().HasPrecision(2, 1);
            rating.Property(feedback => feedback.OverallRating).IsRequired().HasPrecision(2, 1);
        });
        
        // configure relations.
        builder.HasOne<User>(feedback => feedback.Guest).WithMany();
        builder.HasOne<Listing>(feedback => feedback.Listing)
            .WithMany(listing => listing.Feedbacks);
    }
}