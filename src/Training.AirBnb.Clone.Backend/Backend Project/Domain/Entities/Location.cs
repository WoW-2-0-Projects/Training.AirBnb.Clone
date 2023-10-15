using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Location : SoftDeletedEntity
{
    public Guid AddressId { get; set; }
    public string? NeighborhoodDescription { get; set; }
    public string? GettingAround { get; set; }
}
