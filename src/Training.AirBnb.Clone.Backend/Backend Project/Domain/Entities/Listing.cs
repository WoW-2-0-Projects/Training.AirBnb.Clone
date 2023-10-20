#pragma warning disable CS8618

using Backend_Project.Domain.Common;
using Backend_Project.Domain.Enums;

namespace Backend_Project.Domain.Entities;

public class Listing : SoftDeletedEntity
{
    public string Title { get; set; }

    public Guid DescriptionId { get; set; }

    public ListingStatus Status { get; set; } = ListingStatus.InProgress;

    public Guid PropertyTypeId { get; set; }

    public Guid LocationId { get; set; }

    public Guid RulesId { get; set; }

    public Guid AvailabilityId { get; set; }

    public Guid HostId { get; set; }

    public decimal Price { get; set; }

    public bool InstantBook { get; set; }
}