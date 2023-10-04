#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingProperty : SoftDeletedEntity
{
    public string PropertyName { get; set; }
    public int PropertyCount { get; set; }
    public Guid ListingId { get; set; }
}