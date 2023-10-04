#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Reservation : SoftDeletedEntity
{
    public Guid ListingId { get; set; }
    public Guid BookedBy { get; set; }
    public Guid OccupancyId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
}