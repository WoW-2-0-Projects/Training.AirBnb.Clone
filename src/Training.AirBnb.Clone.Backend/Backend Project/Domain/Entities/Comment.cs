#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Comment : SoftDeletedEntity
{
    public Guid WrittenBy { get; set; }
    public string CommentMessage { get; set; }
    public Guid ListingId { get; set; }
}