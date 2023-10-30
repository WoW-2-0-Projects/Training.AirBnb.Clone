using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class LocationScenicViews : SoftDeletedEntity
{
    public Guid LocationId { get; set; }
    public Guid ScenicViewId { get; set; }
}
