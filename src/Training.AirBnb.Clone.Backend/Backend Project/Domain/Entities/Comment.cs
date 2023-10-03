using Backend_Project.Domain.Common;
namespace Backend_Project.Domain.Entities
{
    public class Comment : SoftDeletedEntity
    {
        public Guid WrittenBy { get; set; }
        public string CommentMessage { get; set; }
        public Guid ListingId { get; set; }
        
        public Comment(Guid writtenBy, string commentMessage, Guid listingId)
        {
            Id = Guid.NewGuid();
            WrittenBy = writtenBy;
            CommentMessage = commentMessage;
            ListingId = listingId;
            CreatedDate = DateTimeOffset.UtcNow;
        }

    }
}