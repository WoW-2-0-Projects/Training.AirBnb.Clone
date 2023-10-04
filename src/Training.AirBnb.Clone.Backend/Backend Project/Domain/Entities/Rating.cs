
using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Rating:SoftDeletedEntity
{
    public Guid GivenBy { get; set; }
    public Guid ListingId { get; set; }
    public double Mark { get; set; }
    public Rating() { }

    public Rating(double mark,Guid givenBy, Guid listingId)
    {
        Mark = mark;
        Id = Guid.NewGuid();
        GivenBy = givenBy;
        ListingId = listingId;
    }
}
