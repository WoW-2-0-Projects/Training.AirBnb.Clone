namespace Backend_Project.Domain.Entities;

public class ListingAmenities
{
    public Guid ListingId { get; set; }
    public Guid AmenityId { get; set; }

    public ListingAmenities(Guid listingId, Guid amenityId)
    {
        ListingId = listingId;
        AmenityId = amenityId;
    }
}