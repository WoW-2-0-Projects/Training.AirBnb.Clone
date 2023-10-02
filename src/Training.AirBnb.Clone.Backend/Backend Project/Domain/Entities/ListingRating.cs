using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class ListingRating : SoftDeletedEntity
    {
        public double Rating { get; set; }
        public Guid ListingId { get; set; }
        public ListingRating(double rating, Guid listingId)
        {
            Id = Guid.NewGuid();
            Rating = rating;
            ListingId = listingId;
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
