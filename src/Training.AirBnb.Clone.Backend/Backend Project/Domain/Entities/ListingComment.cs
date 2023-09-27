using Backend_Project.Domain.Common;
namespace Backend_Project.Domain.Entities
{
    public class ListingReview : SoftDeletedEntity
    {
        public Guid WrittenBy { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
        public Guid ListingId { get; set; }
        public ListingReview(Guid writtenBy, string comment, double rating, Guid listingId)
        {
            Id = Guid.NewGuid();
            WrittenBy = writtenBy;
            Comment = comment;
            Rating = rating;
            ListingId = listingId;
            CreatedDate = DateTimeOffset.Now;
        }

    }
}
