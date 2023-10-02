using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingProperty : SoftDeletedEntity
{
    public string PropertyName { get; set; }
    public int PropertyCount { get; set; }
    public Guid ListingId { get; set; }

    public ListingProperty(string propertyName, int propertyCount, Guid listingId)
    {
        Id = Guid.NewGuid();
        PropertyName = propertyName;
        PropertyCount = propertyCount;
        ListingId = listingId;
        CreatedDate = DateTime.UtcNow;
    }
}