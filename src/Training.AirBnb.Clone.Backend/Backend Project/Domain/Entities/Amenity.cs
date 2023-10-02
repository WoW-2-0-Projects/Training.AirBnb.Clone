using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Amenity : SoftDeletedEntity
{
    public string AmenityName { get; set; }
    public Guid CategoryId { get; set; }

    public Amenity(string amenityName, Guid categoryId)
    {
        Id = Guid.NewGuid();
        AmenityName = amenityName;
        CategoryId = categoryId;
        CreatedDate = DateTime.UtcNow;
    }
}