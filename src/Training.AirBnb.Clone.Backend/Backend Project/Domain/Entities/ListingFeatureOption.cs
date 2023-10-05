#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingFeatureOption : SoftDeletedEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}