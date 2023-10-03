using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingFeature : SoftDeletedEntity
{
    public string Name { get; set; }
    public Guid FeatureOptionsId { get; set; }

    public ListingFeature(string name, Guid featureOptionsId)
    {
        Id = Guid.NewGuid();
        Name = name;
        FeatureOptionsId = featureOptionsId;
        CreatedDate = DateTime.UtcNow;
    }
}