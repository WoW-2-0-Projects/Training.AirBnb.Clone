#pragma warning disable CS8618

using Backend_Project.Domain.Common;
using Backend_Project.Domain.Enums;

namespace Backend_Project.Domain.Entities;

public class Listing : SoftDeletedEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ListingStatus Status { get; set; }
    public Guid CategoryId { get; set; }
    public Guid TypeId { get; set; }
    public Guid AddressId { get; set; }
    public Guid OccupancyId { get; set; }
    public Guid HostId { get; set; }
    public decimal Price { get; set; }
}