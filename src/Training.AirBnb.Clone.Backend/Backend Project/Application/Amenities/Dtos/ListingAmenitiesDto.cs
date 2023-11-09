namespace Backend_Project.Application.Amenities.Dtos;

public class ListingAmenitiesDto
{
    public Guid Id { get; set; }

    public Guid ListingId { get; set; }

    public Guid AmenityId { get; set; }
}