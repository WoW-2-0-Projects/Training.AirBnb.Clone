#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Amenity : SoftDeletedEntity
{
    public string AmenityName { get; set; }
    public Guid CategoryId { get; set; }
}