using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingRating : SoftDeletedEntity
{
    public Guid ListingId { get; set; }
    public double Rating { get; set; }
}