using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Rating : SoftDeletedEntity
{
    public Guid GivenBy { get; set; }
    public Guid ListingId { get; set; }
    public double Mark { get; set; }
}