using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingRating : SoftDeletedEntity
{
    public double Rating { get; set; }
    public Guid ListingId { get; set; }
}