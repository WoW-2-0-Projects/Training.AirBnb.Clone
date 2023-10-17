#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingAmenities : SoftDeletedEntity
{
    public Guid ListingId { get; set; }
    public Guid AmenityId { get; set; }
}