using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class GuestFeedbackConfiguration : IEntityTypeConfiguration<GuestFeedback>
{
    public void Configure(EntityTypeBuilder<GuestFeedback> builder)
    {
        builder.OwnsOne<Rating>(feedback => feedback.Rating);

        builder.Navigation(feedback => feedback.Rating);
        
        builder.HasOne<User>(feedback => feedback.Guest).WithMany();
        builder.HasOne<Listing>(feedback => feedback.Listing)
            .WithMany(listing => listing.Feedbacks);
    }
}