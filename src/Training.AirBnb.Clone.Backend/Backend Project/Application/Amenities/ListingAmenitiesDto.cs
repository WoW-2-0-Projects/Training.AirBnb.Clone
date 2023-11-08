namespace Backend_Project.Application.Amenities;

public class ListingAmenitiesDto
{
    public Guid Id { get; set; }

    public Guid ListingId { get; set; }

    public Guid AmenityId { get; set; }
}