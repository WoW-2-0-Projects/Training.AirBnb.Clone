using Backend_Project.Domain.Common;
namespace Backend_Project.Domain.Entities
{
    public class ListingComment : SoftDeletedEntity
    {
        public Guid WrittenBy { get; set; }
        public string Comment { get; set; }
        public Guid ListingId { get; set; }
        public ListingComment(Guid writtenBy, string comment, Guid listingId)
        {
            Id = Guid.NewGuid();
            WrittenBy = writtenBy;
            Comment = comment;
            ListingId = listingId;
            CreatedDate = DateTimeOffset.UtcNow;
        }

    }
}
