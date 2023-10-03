using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingFeatureOption : SoftDeletedEntity
{
    public string Name { get; set; }

    public ListingFeatureOption(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedDate = DateTime.UtcNow;
    }
}