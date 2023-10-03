using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingAmenities : SoftDeletedEntity
{
    public Guid ListingId { get; set; }
    public Guid AmenityId { get; set; }

    public ListingAmenities(Guid listingId, Guid amenityId)
    {
        Id = Guid.NewGuid();
        ListingId = listingId;
        AmenityId = amenityId;
        CreatedDate = DateTime.UtcNow;
    }
}