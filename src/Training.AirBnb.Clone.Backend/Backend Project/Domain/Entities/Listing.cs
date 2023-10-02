using Backend_Project.Domain.Common;
using Backend_Project.Domain.Enums;

namespace Backend_Project.Domain.Entities;

public class Listing : SoftDeletedEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ListingStatus Status { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AddressId { get; set; }
    public Guid OccupancyId { get; set; }
    public Guid HostId { get; set; }
    public decimal Price { get; set; }

    public Listing(string title, string description, ListingStatus status, Guid categoryId, Guid addressId, Guid occupancyId, Guid hostId, decimal price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Status = status;
        CategoryId = categoryId;
        AddressId = addressId;
        OccupancyId = occupancyId;
        HostId = hostId;
        Price = price;
        CreatedDate = DateTime.UtcNow;
    }
}